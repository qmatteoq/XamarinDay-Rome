using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace TrackSeries.Services
{
    public interface IAuthenticationService
    {
        Task<MobileServiceUser> Authenticate(string mobileServiceUrl);
    }
}
