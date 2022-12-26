using Prism.Commands;
using Prism.Navigation;
using RESTCountries.Models;
using Schoolager.Prism.Models;
using Schoolager.Prism.Services;
using Schoolager.Prism.ViewModels;
using Schoolager.Prism.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolager.Prism.ItemViewModels
{
    public class CityItemViewModel : CityResponse
    {
        private readonly INavigationService _navigationService;

        private DelegateCommand _selectCountryCommand;
        private readonly IApiServices _apiService;

        public CityItemViewModel(INavigationService navigationService, IApiServices apiService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
        }


        public DelegateCommand SelectCountryCommand => 
            _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));


        private async void SelectCountryAsync()
        {
            string urlBase = App.Current.Resources["UrlAPIWeather"].ToString();
            string key = App.Current.Resources["KEYWeather"].ToString();
            string servicePrefix = "weather?lat=" + this.Latlng[0] + "&lon=" + this.Latlng[1];
            string controller = "&appid=" + key+"&units=metric";
            
            Response response = await _apiService.GetWeather(urlBase, servicePrefix, controller);

            WeatherResponse weather  = (WeatherResponse)response.Result;

            CountryWeather countryWeather = new CountryWeather
            {
                Name = this.Name,
                Flag = this.Flag,
                Description = weather.Weather[0].Description,
                Temp_max = weather.Main.Temp_max,
                Temp_min = weather.Main.Temp_min,
                Feels_like = weather.Main.Feels_like,
                Humidity = weather.Main.Humidity,
                Icon = "http://openweathermap.org/img/wn/"+weather.Weather[0].Icon+"@4x.png"
            };

            NavigationParameters parameters = new NavigationParameters
            {
                {"country", countryWeather }
            };
            await _navigationService.NavigateAsync(nameof(CityDetailPage),parameters);
        }
    }
}
