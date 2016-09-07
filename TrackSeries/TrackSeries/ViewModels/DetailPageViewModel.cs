using System;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using TrackSeries.Core.Models;
using TrackSeries.Core.Services;
using TrackSeries.Events;
using TrackSeries.Models;
using TrackSeries.Services;

namespace TrackSeries.ViewModels
{
    public class DetailPageViewModel : BindableBase, INavigationAware
    {
        private readonly ITsApiService _tsApiService;
        private readonly IOfflineSyncService _offlineSyncService;
        private readonly IEventAggregator _eventAggregator;
        private SerieInfoVM _selectedShow;

        public SerieInfoVM SelectedShow
        {
            get { return _selectedShow; }
            set { SetProperty(ref _selectedShow, value); }
        }

        private bool _isFavoriteShow;

        public bool IsFavoriteShow
        {
            get { return _isFavoriteShow; }
            set { SetProperty(ref _isFavoriteShow, value); }
        }

        public DetailPageViewModel(ITsApiService tsApiService, IOfflineSyncService offlineSyncService,
            IEventAggregator eventAggregator)
        {
            _tsApiService = tsApiService;
            _offlineSyncService = offlineSyncService;
            _eventAggregator = eventAggregator;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            int id = Convert.ToInt32(parameters["id"]);
            SelectedShow = await _tsApiService.GetSerieById(id);

            IsFavoriteShow = await _offlineSyncService.IsShowFavorite(id);

        }

        private DelegateCommand _handleFavoriteCommand;

        public DelegateCommand HandleFavoriteCommand
        {
            get
            {
                if (_handleFavoriteCommand == null)
                {
                    _handleFavoriteCommand = new DelegateCommand(async () =>
                    {
                        if (IsFavoriteShow)
                        {
                            await _offlineSyncService.RemoveFavorite(SelectedShow.Id);
                            IsFavoriteShow = false;
                        }
                        else
                        {
                            await _offlineSyncService.AddFavorite(
                                SelectedShow.Id);
                            IsFavoriteShow = true;
                        }
                        _eventAggregator.GetEvent<FavoriteChangedEvent>().Publish(SelectedShow.Id);

                    });
                }

                return _handleFavoriteCommand;
            }
        }
    }
}
