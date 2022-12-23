using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RESTCountries.Models;
using Schoolager.Prism.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schoolager.Prism.ViewModels
{
    public class CityDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private WeatherResponse _weather;
        public CityDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }
        public WeatherResponse Weather
        {
            get => _weather;
            set => SetProperty(ref _weather, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Country"))
            {
                string Country = parameters.GetValue<CityResponse>("Country").Name;
                //TODO CallAPI Weather

                Title = Country;
            }
           
        }
    }
}
