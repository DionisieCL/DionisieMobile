using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Schoolager.Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Schoolager.Prism.Views;

namespace Schoolager.Prism.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {

        public MenuPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Menu";

        }       
    }
}
