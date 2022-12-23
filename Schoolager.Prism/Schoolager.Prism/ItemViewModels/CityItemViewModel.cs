using Prism.Commands;
using Prism.Navigation;
using Schoolager.Prism.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolager.Prism.ItemViewModels
{
    public class CityItemViewModel : WeatherResponse
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

        private void SelectCountryAsync()
        {
            throw new NotImplementedException();
        }
    }
}
