using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Events;
using TrackSeries.Core.Models;
using TrackSeries.Core.Services;
using TrackSeries.Events;
using TrackSeries.Models;
using TrackSeries.Services;
using Xamarin.Forms;

namespace TrackSeries.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly TsApiService _apiService;
        private readonly INavigationService _navigationService;
        private readonly IOfflineSyncService _offlineSyncService;
        private readonly IAuthenticationService _authenticationService;
        private ObservableCollection<SerieFollowersVM> _topSeries;

        public ObservableCollection<SerieFollowersVM> TopSeries
        {
            get { return _topSeries; }
            set { SetProperty(ref _topSeries, value); }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public MainPageViewModel(TsApiService apiService, INavigationService navigationService,
            IOfflineSyncService offlineSyncService,
            IEventAggregator eventAggregator,
            IAuthenticationService authenticationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            _offlineSyncService = offlineSyncService;
            _authenticationService = authenticationService;

            eventAggregator.GetEvent<FavoriteChangedEvent>().Subscribe(trackSeriesId =>
            {
                var serie = TopSeries.FirstOrDefault(x => x.Id == trackSeriesId);
                serie.IsFavorite = !serie.IsFavorite;
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            IsLoading = true;
            var user = await _authenticationService.Authenticate(Constants.MobileAppUrl);
            bool isAuthenticated = _offlineSyncService.Authenticate(user);
            if (isAuthenticated)
            {
                await _offlineSyncService.InitLocalStoreAsync();
                await _offlineSyncService.SyncAsync();
                await RefreshData();
            }
            IsLoading = false;
        }

        private async Task RefreshData()
        {
            IsLoading = true;
            var result = await _apiService.GetStatsTopSeries();
            foreach (var serie in result)
            {
                serie.IsFavorite = await _offlineSyncService.IsShowFavorite(serie.Id);
            }

            TopSeries = new ObservableCollection<SerieFollowersVM>();
            TopSeries = new ObservableCollection<SerieFollowersVM>(result);
            IsLoading = false;
        }

        private DelegateCommand<ItemTappedEventArgs> _goToDetailPageCommand;

        public DelegateCommand<ItemTappedEventArgs> GoToDetailPageCommand
        {
            get
            {
                if (_goToDetailPageCommand == null)
                {
                    _goToDetailPageCommand = new DelegateCommand<ItemTappedEventArgs>(async selected =>
                    {
                        NavigationParameters param = new NavigationParameters();
                        var serie = selected.Item as SerieFollowersVM;
                        param.Add("id", serie.Id);
                        await _navigationService.NavigateAsync("DetailPage", param);
                    });
                }

                return _goToDetailPageCommand;
            }
        }

        private DelegateCommand _refreshCommand;

        public DelegateCommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new DelegateCommand(async () =>
                    {
                        await _offlineSyncService.SyncAsync();
                        await RefreshData();
                    });
                }

                return _refreshCommand;
            }
        }
    }
}
