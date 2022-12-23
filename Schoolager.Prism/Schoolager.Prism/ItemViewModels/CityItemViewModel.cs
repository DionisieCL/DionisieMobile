using Prism.Commands;
using Prism.Navigation;
using Schoolager.Prism.Models;
using Schoolager.Prism.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolager.Prism.ItemViewModels
{
    public class CityItemViewModel : CityResponse
    {
        private readonly INavigationService _navigationService;

        private DelegateCommand _selectCountryCommand;
        public CityItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCountryCommand => 
            _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));

        private async void SelectCountryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                {"Country",this }
            };
            await _navigationService.NavigateAsync(nameof(CityDetailPageViewModel),parameters);
        }
    }
}
