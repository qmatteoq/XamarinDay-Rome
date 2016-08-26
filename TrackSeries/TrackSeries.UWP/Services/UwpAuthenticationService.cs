using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TrackSeries.Services;
using TrackSeries.UWP.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpAuthenticationService))]
namespace TrackSeries.UWP.Services
{
    public class UwpAuthenticationService: IAuthenticationService
    {
        public async Task<MobileServiceUser> Authenticate(string mobileServiceUrl)
        {
            MobileServiceClient client = new MobileServiceClient(mobileServiceUrl);
            var user = await client.LoginAsync(MobileServiceAuthenticationProvider.Twitter);
            return user;
        }
    }
}
