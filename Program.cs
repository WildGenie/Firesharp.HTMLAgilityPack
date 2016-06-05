using System;
using System.Collections.Generic;
using System.Linq;
using FireSharp.Config;
using FireSharp.Interfaces;
using HtmlAgilityPack;

namespace FirebaseClient
{
   class Program
   {
      static void Main()
      {
         IFirebaseConfig config = new FirebaseConfig
         {
            AuthSecret = Configuration.FirebaseBaseSecret,
            BasePath = Configuration.FirebaseBasePath
         };

         var firebaseClient = new FireSharp.FirebaseClient(config);

         int count = 0;
         var products = ScrapeWebshop();
         foreach (var product in products)
         {
            firebaseClient.Push(Configuration.FirebaseBaseSubpath, product);
            count++;
         }
         Console.WriteLine($"Uploaded {count} Products");
      }

      static IEnumerable<Product> ScrapeWebshop()
      {
         IList<Product> products = new List<Product>();
         var client = new CookieAwareWebClient();
         HtmlDocument doc = client.GetPage(Configuration.WebshopBasePath);

         var dy = doc.DocumentNode.Descendants().Where(x => x.Name.Equals("a") && x.Attributes.Contains("class") && x.Attributes["class"].Value.Equals("l2"));
         foreach (HtmlNode du in dy)
         {
            Console.WriteLine(HtmlEntity.DeEntitize(du.InnerText));
            foreach (HtmlNode dt in du.ParentNode.Descendants().Where(x => x.Name.Equals("a") && x.Attributes.Contains("class") && x.Attributes["class"].Value.Equals("l3")))
            {
               HtmlDocument docDetail = client.GetPage($@"{Configuration.WebshopSecondaryPath}{dt.Attributes["href"].Value}?ajaxWorkarea=true");

               var pa = docDetail.DocumentNode.Descendants().Where(x => x.Name.Equals("div") && x.Attributes.Contains("class") && x.Attributes["class"].Value.Equals("product"));
               foreach (HtmlNode pb in pa)
               {
                  string name = "";
                  string price = "";
                  string quantity = "";
                  string url = "";
                  HtmlNode pd = pb.Descendants().FirstOrDefault(x => x.Name.Equals("p") && x.Attributes.Contains("class") && x.Attributes["class"].Value.Equals("product-grid-price"));
                  if (pd != null)
                  {
                     price = HtmlEntity.DeEntitize(pd.InnerText);
                  }
                  HtmlNode pe = pb.Descendants().FirstOrDefault(x => x.Name.Equals("p") && x.Attributes.Contains("class") && x.Attributes["class"].Value.Equals("product-inhalt-amount"));
                  if (pe != null)
                  {
                     quantity = HtmlEntity.DeEntitize(pe.InnerText);
                  }
                  HtmlNode pc = pb.Descendants().FirstOrDefault(x => x.Name.Equals("img"));
                  if (pc != null)
                  {
                     url = pc.Attributes["src"].Value;
                     name = pc.Attributes["title"].Value;
                  }
                  decimal d;
                  if (!decimal.TryParse(price, out d))
                  {
                     d = 0;
                  }
                  var product = new Product
                  {
                     Name = name,
                     Price = d,
                     Quantity = quantity,
                     Url = url
                  };
                  products.Add(product);
               }
            }
         }
         return products;
      }
   }

   class Product
   {
      public string Name { get; set; }
      public decimal Price { get; set; }
      public string Quantity { get; set; }
      public string Url { get; set; }
   }
}
