using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using TrackSeries.Droid.Services;
using TrackSeries.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidAuthenticationService))]
namespace TrackSeries.Droid.Services
{
    public class DroidAuthenticationService : IAuthenticationService
    {
        public async Task<MobileServiceUser> Authenticate(string mobileServiceUrl)
        {
            MobileServiceClient client = new MobileServiceClient(mobileServiceUrl);
            var user = await client.LoginAsync(Forms.Context, MobileServiceAuthenticationProvider.Twitter);
            return user;
        }
    }
}