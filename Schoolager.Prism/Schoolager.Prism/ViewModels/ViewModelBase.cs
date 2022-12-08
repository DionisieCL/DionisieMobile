using Prism.Mvvm;
using Prism.Navigation;

namespace Schoolager.Prism.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public string Title { get; set; }
        public INavigationService NavigationService { get; }
    }
}