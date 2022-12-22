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
        private bool _isRunning;
        private DelegateCommand _searchCommand;

        public WeatherPageViewModel(INavigationService navigationService, IApiServices apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Weather";
            IsRunning = false;
        }
        public string City{ get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(Search));

        public WeatherResponse weather { get; set; }
        public string Visibility {
            get => _visibility;
            set => SetProperty(ref _visibility, value);
        }

        private async void Search()
        {
            IsRunning = true;
            string urlBase = App.Current.Resources["UrlAPIWeather"].ToString();
            string key = App.Current.Resources["KEYWeather"].ToString();
            string servicePrefix = "weather?q="+City;
            string controller = "&appid="+key;
            Response response = await _apiService.GetWeather(urlBase, servicePrefix, controller);

            weather = (WeatherResponse)response.Result;

            //            _visibility = weather.Visibility.ToString();
            Visibility = weather.Visibility.ToString();
            await App.Current.MainPage.DisplayAlert("OK", Visibility, "Accept");
            IsRunning = false;
        }
    }
}
