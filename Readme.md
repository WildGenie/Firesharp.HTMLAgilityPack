# Firesharp.HTMLAgilityPack
## Firesharp and HTML Agilitypack to scrape a webshop   

This is a small demo project to demonstrate the use of Firesharp, the Firebase client for .NET.

The HTML Agility Pack is used to retrieve products from an arbitrary web shop and to store the items into firebase.


### Prerequisites

* Firebase account
* Visual Studio
* Create a C# console project and add Firesharp and HTML Agility Pack via Nuget


### Points of interest

* Using HTML Agility Pack to parse a website
* Requests to the webshop are made using the *CookieAwareWebClient.c*. Refer to this article for more information: http://stackoverflow.com/questions/15206644/how-to-pass-cookies-to-htmlagilitypack-or-webclient
* Push data to Firebase with Firesharp



### Configuration:

* Provide a *sitesettings.config* file in folder *app_data* to configure your specific parameters as shown below:






```xml

<?xml version="1.0" encoding="utf-8" ?>
<appSettings>
  <add key="FirebaseBasePath" value="https://--myapp--.firebaseio.com/" />
  <add key="FirebaseSecret" value="o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1o1" />
  <add key="FirebaseSubpath" value="Products/WebshopXy"/>
  <add key="WebshopBasePath" value="https://www.awebshop.xxx/supermarket/nav"/>
  <add key="WebshopSecondaryPath" value="https://www.awebshop.xxx/supermarket/items/"/>
</appSettings>

```






