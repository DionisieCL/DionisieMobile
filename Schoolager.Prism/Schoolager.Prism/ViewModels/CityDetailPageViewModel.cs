using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schoolager.Prism.ViewModels
{
    public class CityDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public CityDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
