using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Schoolager.Prism.Views;
using Schoolager.Prism.Models;
using Schoolager.Prism.Services;
using Xamarin.Essentials;
using Schoolager.Prism.Helpers;

namespace Schoolager.Prism.ViewModels
{
	public class FavoritesPageViewModel : ViewModelBase
    {
        private DelegateCommand _searchCommand;

        private bool _isRunning;
        private string _searchItem;
        private string _search;
        private readonly IApiServices _apiService;
        private bool _enabled;

        private WeatherResponse _weatherDetail;

        public FavoritesPageViewModel(INavigationService navigationService, IApiServices apiServices) : base(navigationService)
        {
            Title = Languages.SearchAll;
            _apiService = apiServices;
            _enabled = false;
            _isRunning = false;
        }

        public string SearchItem
        { 
            get => _searchItem;
            set => SetProperty(ref (_searchItem), value); }

        public WeatherResponse WeatherDetail
        {
            get => _weatherDetail;
            set => SetProperty(ref (_weatherDetail), value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool Enabled
        {
            get => _enabled;
            set => SetProperty(ref (_enabled), value);

        }
        public string Search {
            get => _search;
            set => SetProperty(ref (_search), value);
        }
    
        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ButtonClicked));

        public async void ButtonClicked()
        {
            IsRunning = true;
            Enabled = false;
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error,
                        Languages.ConnectionError,
                        Languages.Accept);

                });
                IsRunning= false;
                return;
            }

            string urlBase = App.Current.Resources["UrlAPIWeather"].ToString();
            string key = App.Current.Resources["KEYWeather"].ToString();
            string servicePrefix = "weather?q=" + Search;
            string controller = "&appid=" + key + "&units=metric";
            Response response = await _apiService.GetWeather(urlBase, servicePrefix, controller);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error,response.Message, Languages.Accept);
                
            }
            else
            {
                IsRunning = false;
                SearchItem = Search;
                WeatherResponse weather = (WeatherResponse)response.Result;
                string icon = weather.Weather[0].Icon;
                weather.Weather[0].Icon = "http://openweathermap.org/img/wn/" + icon + "@4x.png";
                WeatherDetail = weather;
                Enabled = true;
            }
            
            Search= null;
           
            
        }

    }
}
