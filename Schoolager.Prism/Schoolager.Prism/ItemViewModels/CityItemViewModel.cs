using Prism.Commands;
using Prism.Navigation;
using RESTCountries.Models;
using Schoolager.Prism.Models;
using Schoolager.Prism.Services;
using Schoolager.Prism.ViewModels;
using Schoolager.Prism.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public double CalculateProgress(int sunrise, int sunset)
        {
            int total = sunset - sunrise;

            int now = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

            double difference = (double)(now - sunrise) / total;

            return difference*100;
        }

        public string CalculateLocalTime(int timezone)
        {
            int now = (int)DateTimeOffset.Now.ToUnixTimeSeconds() +timezone;
            DateTimeOffset localTime = DateTimeOffset.FromUnixTimeSeconds(now);

            return localTime.TimeOfDay.ToString();

        }

        private async void SelectCountryAsync()
        {
            string urlBase = App.Current.Resources["UrlAPIWeather"].ToString();
            string key = App.Current.Resources["KEYWeather"].ToString();
            string servicePrefix = "weather?lat=" + this.Latlng[0] + "&lon=" + this.Latlng[1];
            string controller = "&appid=" + key+"&units=metric";
            
            Response response = await _apiService.GetWeather(urlBase, servicePrefix, controller);

            WeatherResponse weather  = (WeatherResponse)response.Result;
            
            DateTimeOffset timeSunset = DateTimeOffset.FromUnixTimeSeconds(weather.Sys.Sunset + weather.Timezone);
            DateTimeOffset timeSunrise = DateTimeOffset.FromUnixTimeSeconds(weather.Sys.Sunrise + weather.Timezone);
            double progressTime = CalculateProgress(weather.Sys.Sunrise, weather.Sys.Sunset);
            string localTime = CalculateLocalTime(weather.Timezone);
            CountryWeather countryWeather = new CountryWeather
            {
                Name = this.Name,
                Flag = this.Flag,
                Description = weather.Weather[0].Description,
                Temp = weather.Main.Temp,
                Temp_max = weather.Main.Temp_max,
                Temp_min = weather.Main.Temp_min,
                Feels_like = weather.Main.Feels_like,
                Humidity = weather.Main.Humidity,
                Visibility= weather.Visibility,
                Pressure= weather.Main.Pressure,
                Speed = weather.Wind.Speed,
                Sunrise = timeSunrise.TimeOfDay.ToString(), 
                Sunset = timeSunset.TimeOfDay.ToString(),
                Icon = "http://openweathermap.org/img/wn/"+weather.Weather[0].Icon+"@4x.png",
                ProgressTime= progressTime,
                LocalTime = localTime
            };

            NavigationParameters parameters = new NavigationParameters
            {
                {"country", countryWeather }
            };
            await _navigationService.NavigateAsync(nameof(CityDetailPage),parameters);
        }
    }
}
