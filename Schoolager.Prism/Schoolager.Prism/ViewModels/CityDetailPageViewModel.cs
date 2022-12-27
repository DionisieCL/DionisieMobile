using Example;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RESTCountries.Models;
using Schoolager.Prism.Models;
using Schoolager.Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schoolager.Prism.ViewModels
{
    public class CityDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private CountryWeather _weather;

        public CityDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }
        public CountryWeather WeatherDetail
        {
            get => _weather;
            set => SetProperty(ref _weather, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("country"))
            {
                WeatherDetail = parameters.GetValue<CountryWeather>("country");

                Title = WeatherDetail.Name;

            }
           
        }
    }
}
