using Prism.Mvvm;
using Prism.Navigation;
using Schoolager.Prism.Services;

namespace Schoolager.Prism.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        private readonly INavigationService _navigationService;
        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string Title { get; set; }
        public INavigationService NavigationService { get; set; }

    }
}