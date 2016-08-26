using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using TrackSeries.Models;

namespace TrackSeries.Services
{
    public class OfflineSyncService : IOfflineSyncService
    {
        private MobileServiceClient client;
        
        private readonly string MobileServiceUrl = Constants.MobileAppUrl;
        private IMobileServiceSyncTable<FavoriteShow> favoritesTable;

        public OfflineSyncService()
        {
            client = new MobileServiceClient(MobileServiceUrl);
        }

        public async Task InitLocalStoreAsync()
        {
            var store = new MobileServiceSQLiteStore("favorites.db");
            store.DefineTable<FavoriteShow>();

            // Uses the default conflict handler, which fails on conflict
            // To use a different conflict handler, pass a parameter to InitializeAsync. For more details, see http://go.microsoft.com/fwlink/?LinkId=521416
            await client.SyncContext.InitializeAsync(store);
            favoritesTable = client.GetSyncTable<FavoriteShow>();
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await client.SyncContext.PushAsync();

                await this.favoritesTable.PullAsync(
                    "allFavoritesShows",
                    this.favoritesTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. 
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.",
                        error.TableName, error.Item["id"]);
                }
            }
        }

        public async Task AddShow(int trackSeriesId)
        {
            var list = await favoritesTable.Where(x => x.TrackSeriesId == trackSeriesId).ToEnumerableAsync();
            var favorite = list.FirstOrDefault();
            if (favorite == null)
            {
                FavoriteShow show = new FavoriteShow(trackSeriesId) {IsFavorite = true};
                await favoritesTable.InsertAsync(show);
            }
            else
            {
                favorite.IsFavorite = true;
                await favoritesTable.UpdateAsync(favorite);
            }
            
            await SyncAsync();
        }

        public async Task RemoveShow(int trackSeriesId)
        {
            var favorites = await favoritesTable.Where(x => x.TrackSeriesId == trackSeriesId).ToEnumerableAsync();
            var favorite = favorites.FirstOrDefault();
            favorite.IsFavorite = false;
            await favoritesTable.UpdateAsync(favorite);
            await SyncAsync();
        }

        public async Task<bool> IsShowFavorite(int id)
        {
            var favoriteShows = await favoritesTable.Where(x => x.TrackSeriesId == id).ToListAsync();
            return favoriteShows.Any() && favoriteShows.FirstOrDefault().IsFavorite;
        }

        public bool Authenticate(MobileServiceUser user)
        {
            if (user != null)
            {
                client.CurrentUser = user;
                return true;
            }
            return false;
        }
    }
}
