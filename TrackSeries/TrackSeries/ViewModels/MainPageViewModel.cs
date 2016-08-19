using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TrackSeries.Core.Models;
using TrackSeries.Core.Services;

namespace TrackSeries.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly TsApiService _apiService;
        private ObservableCollection<SerieFollowersVM> _topSeries;

        public ObservableCollection<SerieFollowersVM> TopSeries
        {
            get { return _topSeries; }
            set { SetProperty(ref _topSeries, value); }
        } 
          
        public MainPageViewModel(TsApiService apiService)
        {
            _apiService = apiService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            var result = await _apiService.GetStatsTopSeries();
            TopSeries = new ObservableCollection<SerieFollowersVM>(result);
        }
    }
}
