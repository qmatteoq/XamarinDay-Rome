using System.Threading.Tasks;
using TrackSeries.Models;

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