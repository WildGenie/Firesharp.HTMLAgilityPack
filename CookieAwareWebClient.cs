using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace FirebaseClient
{
   /// <summary>
   /// Handle cookies when making subsequent web requests
   /// <see cref="http://stackoverflow.com/questions/15206644/how-to-pass-cookies-to-htmlagilitypack-or-webclient"/>
   /// </summary>
   public class CookieAwareWebClient
   {
      //The cookies will be here.
      private CookieContainer _cookies = new CookieContainer();

      //In case you need to clear the cookies
      public void ClearCookies()
      {
         _cookies = new CookieContainer();
      }

      public HtmlDocument GetPage(string url)
      {
         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
         request.Method = "GET";

         //Set more parameters here...
         //...

         //This is the important part.
         request.CookieContainer = _cookies;

         HttpWebResponse response = (HttpWebResponse)request.GetResponse();
         var stream = response.GetResponseStream();

         //When you get the response from the website, the cookies will be stored
         //automatically in "_cookies".

         using (var reader = new StreamReader(stream))
         {
            string html = reader.ReadToEnd();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
         }
      }
   }
}
