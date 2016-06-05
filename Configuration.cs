using System.Configuration;
using System.Linq;

namespace FirebaseClient
{
   public class Configuration
   {
      public static string FirebaseBasePath => ReadStringValue(nameof(FirebaseBasePath), "https://--myapp--.firebaseio.com/");
      public static string FirebaseBaseSecret => ReadStringValue(nameof(FirebaseBaseSecret), "o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1");
      public static string FirebaseBaseSubpath => ReadStringValue(nameof(FirebaseBaseSubpath), "Products/WebshopXy");
      public static string WebshopBasePath => ReadStringValue(nameof(WebshopBasePath), "https://www.awebshop.xxx/supermarket/nav");
      public static string WebshopSecondaryPath => ReadStringValue(nameof(WebshopSecondaryPath), "https://www.awebshop.xxx/supermarket/items/");

      private static string ReadStringValue(string key, string defaultVal)
      {
         var hasKey = ConfigurationManager.AppSettings.AllKeys.Any(k => k == key);
         return hasKey ? ConfigurationManager.AppSettings[key] : defaultVal;
      }
   }
}
