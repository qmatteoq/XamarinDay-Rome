using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Unity;
using TrackSeries.Core.Services;
using TrackSeries.Services;
using TrackSeries.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TrackSeries
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<DetailPage>();
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterType<ITsApiService, TsApiService>();
            Container.RegisterType<IEventAggregator, EventAggregator>();
            Container.RegisterType<IOfflineSyncService, OfflineSyncService>(new ContainerControlledLifetimeManager());
        }
    }
}
