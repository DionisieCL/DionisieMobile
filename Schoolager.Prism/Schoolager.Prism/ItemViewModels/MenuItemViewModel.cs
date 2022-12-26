using Prism.Commands;
using Prism.Navigation;
using Schoolager.Prism.Models;
using Schoolager.Prism.ViewModels;
using Schoolager.Prism.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml.Internals;

namespace Schoolager.Prism.ItemViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommnad;
        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommnad => _selectMenuCommnad ?? (_selectMenuCommnad = new DelegateCommand(SelectMenuAsync));
        private async void SelectMenuAsync()
        {
            await _navigationService.NavigateAsync
                ($"/{nameof(WeatherMasterDetailPage)}/NavigationPage/{PageName}");
        }
    }
}
