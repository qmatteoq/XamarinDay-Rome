using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace TrackSeries.Services
{
    public interface IOfflineSyncService
    {
        Task InitLocalStoreAsync();
        Task SyncAsync();
        Task AddShow(int trackSeriesId);
        Task RemoveShow(int trackSeriesId);
        Task<bool> IsShowFavorite(int id);
    }
}