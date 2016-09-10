# Offline sync sample in a Xamarin Forms app
Offline sync sample in a Xamarin Forms app for Xamarin Day in Rome

Instructions to use it:
1) Create an App Service on your Azure subscriptions
2) Insert the URL of your App Service in the property <b>MobileAppUrl</b> in the class <b>TrackSeries.Models.Contants</b> in the portable <b>TrackSeries</b> project.

There are two branches of the project:

1) Easytable leverages the Node.js backend and the Easy Tables option you will find in the App Service's configuration. Make sure to create a table called <b>FavoriteShow</b> with the following fields:
- IsFavorite
- TrackSeriesId
- UserId

2) The master branch leverages an ASP.NET backend (which is included in the branch). You just need to setup a data connection to a SQL Server database in the <b>Data connections</b> section of the App Service's configuration. The needed tables will be created at the first execution. The master branch implements authentication using Twitter. If you want to enable it, you need to access to the <b>Authentication / Authorization</b> section of the App Service and, in the Twitter section, configure the proper keys. You can find the instructions on how to get them at https://azure.microsoft.com/en-us/documentation/articles/app-service-mobile-how-to-configure-twitter-authentication/

