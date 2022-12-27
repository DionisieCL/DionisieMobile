using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Schoolager.Prism.Helpers;
using Schoolager.Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schoolager.Prism.ViewModels
{
    public class LogoutViewModel : ViewModelBase
    {
        public LogoutViewModel(INavigationService navigationService, IApiServices apiService) : base(navigationService)
        {
            Logout();
        }

        public async void Logout()
        {
            await NavigationService.NavigateAsync("NavigationPage/LogInPage");
        }
    }
}
