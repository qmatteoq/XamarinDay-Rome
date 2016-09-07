using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace TrackSeries.Services
{
    public interface IOfflineSyncService
    {
        Task InitLocalStoreAsync();
        Task SyncAsync();
        Task AddFavorite(int trackSeriesId);
        Task RemoveFavorite(int trackSeriesId);
        Task<bool> IsShowFavorite(int id);
        bool Authenticate(MobileServiceUser user);
    }
}