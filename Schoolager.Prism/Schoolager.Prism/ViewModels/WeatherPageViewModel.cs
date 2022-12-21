using Example;
using ImTools;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Schoolager.Prism.Models;
using Schoolager.Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace Schoolager.Prism.ViewModels
{
	public class WeatherPageViewModel : ViewModelBase
	{
        private readonly IApiServices _apiService;
        private string _visibility;
        public WeatherPageViewModel(INavigationService navigationService, IApiServices apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Weather";
          
             GetData();
         
        }
        public WeatherResponse weather { get; set; }
        public string Visibility {
            get => _visibility;
            set => SetProperty(ref _visibility, value);
        }

        private async void GetData()
        {
            string urlBase = "";
            string controller = "";
            string servicePrefix = "";
            Response response = await _apiService.GetWeather(urlBase, servicePrefix, controller);

            weather = (WeatherResponse)response.Result;

            _visibility = weather.Visibility.ToString();
            
        }


    }
}
