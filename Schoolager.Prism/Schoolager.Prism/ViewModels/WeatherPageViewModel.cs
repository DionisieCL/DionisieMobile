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
using RESTCountries.Services;
using RESTCountries.Models;
using RestSharp;
using System.Collections.ObjectModel;
using Schoolager.Prism.ItemViewModels;

namespace Schoolager.Prism.ViewModels
{
	public class WeatherPageViewModel : ViewModelBase,INotifyPropertyChanged
	{
        private readonly IApiServices _apiService;
        private bool _isRunning;
        private ObservableCollection<CityItemViewModel> _countries;
        private string _search;
        private List<CityResponse> _city;
        private DelegateCommand _searchCommand;
        private readonly INavigationService _navigationService;
        public WeatherPageViewModel(INavigationService navigationService, IApiServices apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Weather";

            LoadCountries();
        }
        public ObservableCollection<CityItemViewModel> Countries{
            get=> _countries; 
            set=>SetProperty(ref _countries, value); 
        }
        public DelegateCommand SearchCommand=> _searchCommand ??(_searchCommand = new DelegateCommand(ShowCities));

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCities();
            }
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }


        public WeatherResponse weather { get; set; }
       

        private async void LoadCountries()
        {

            string urlBase= App.Current.Resources["CityList"].ToString();
            string servicePrefix = "v2/";
            string controller = "all";
            
            Response response = await _apiService.Test<CityResponse>();

           _city = (List<CityResponse>)response.Result;

            ShowCities();


           // await App.Current.MainPage.DisplayAlert("OK", CityResponse.ToArray()[0].Name, "Accept");
           // Response response = await _apiService.GetListAsync<CityResponse>(urlBase,servicePrefix,controller);
           // CityResponse = (List<CityResponse>)response.Result;

            //await App.Current.MainPage.DisplayAlert("OK", CityResponse.ToArray()[0].Capital.ToString(), "Accept");
            /*string urlBase = App.Current.Resources["UrlAPIWeather"].ToString();
            string key = App.Current.Resources["KEYWeather"].ToString();
            string servicePrefix = "weather?q="+City;
            string controller = "&appid="+key;
            Response response = await _apiService.GetWeather(urlBase, servicePrefix, controller);

            weather = (WeatherResponse)response.Result;

            //            _visibility = weather.Visibility.ToString();
            Visibility = weather.Visibility.ToString();
            
           */
        }
        private void ShowCities()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Countries = new ObservableCollection<CityItemViewModel>
                    (_city.Select(c=> new CityItemViewModel(_navigationService, _apiService) {


                        Name = c.Name,
                        Flag = c.Flag,
                        Latlng = c.Latlng,
                        

                    }).ToList());
            }
            else
            {
                Countries = new ObservableCollection<CityItemViewModel>
                    (_city.Select(
                        c=> new CityItemViewModel(_navigationService, _apiService)
                        {
                            Name = c.Name,
                            Flag = c.Flag,
                            Latlng = c.Latlng,
                        }).Where(p => p.Name.ToLower().Contains(Search.ToLower())).ToList() );
            }
        }
    }
}
