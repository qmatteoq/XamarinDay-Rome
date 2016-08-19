using Microsoft.Practices.Unity;
using Prism.Unity;
using TrackSeries.Core.Services;
using TrackSeries.Views;

namespace TrackSeries
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterType<ITsApiService, TsApiService>();
        }
    }
}
