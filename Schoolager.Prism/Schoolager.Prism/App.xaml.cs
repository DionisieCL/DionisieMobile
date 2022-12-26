using Prism;
using Prism.Ioc;
using Schoolager.Prism.Services;
using Schoolager.Prism.ViewModels;
using Schoolager.Prism.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Syncfusion.SfSchedule;
using Prism.Navigation;

namespace Schoolager.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
           
            InitializeComponent();
          // await NavigationService.NavigateAsync("NavigationPage/AboutPage");
          // await NavigationService.NavigateAsync($"/{nameof(WeatherMasterDetailPage)}/NavigationPage/{nameof(FavoritesPage)}"); 
          // await NavigationService.NavigateAsync("/WeatherMasterDetailPage/NavigationPage/FavoritesPage"); 
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiServices, ApiServicescs>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LogInPage, LogInPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
            containerRegistry.RegisterForNavigation<SchedulePage, SchedulePageViewModel>();

            containerRegistry.RegisterForNavigation<MessagePage, MessagePageViewModel>();
            containerRegistry.RegisterForNavigation<WeatherPage, WeatherPageViewModel>();
            containerRegistry.RegisterForNavigation<CityDetailPage, CityDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<WeatherMasterDetailPage, WeatherMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<FavoritesPage, FavoritesPageViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutPageViewModel>();
        }
    } 
}
