using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Schoolager.Prism.Helpers;
using Schoolager.Prism.Models;
using Schoolager.Prism.Services;
using Schoolager.Prism.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Schoolager.Prism.ViewModels
{
    public class LogInPageViewModel : ViewModelBase
    {
        private string _password;
        private bool _isRunning;
        private bool _isEnable;
        private bool _isBlock;
        private DelegateCommand _loginCommand;
        private List<CityResponse> _city;
        private readonly IApiServices _apiService;
        private INavigationService _navigationService;

        

        public LogInPageViewModel(INavigationService navigationService, IApiServices apiService) : base(navigationService)
        {
            Title = Languages.Login;
            IsEnable= true;
            IsRunning= false;
            IsBlock= true;

            _navigationService = navigationService;
            _apiService = apiService;

        }
        public string Email { get; set; }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));
        public List<CityResponse> CityResponses
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }
       
        public bool IsRunning
        {
            get =>   _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public bool IsEnable
        {
            get => _isEnable;
            set => SetProperty(ref _isEnable, value);
        }
        public bool IsBlock
        {
            get => _isBlock;
            set => SetProperty(ref _isBlock, value);
        }

        private async void Login()
        {
            if(Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, 
                        Languages.ConnectionError, 
                        Languages.Accept);

                });
                return; 
            }
            IsBlock = false;

            IsRunning = true;
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error,
                        Languages.MustEmail,
                        Languages.Accept);
                Password = string.Empty;
                IsBlock = true;
                IsRunning = false;

                return;

            }
            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error,
                        Languages.MustPassword,
                        Languages.Accept);
                Password = string.Empty;
                IsBlock = true;
                IsRunning = false;

                return;

            }
            IsBlock= false;
            string url = App.Current.Resources["UrlAPI"].ToString();
            string controller = "/Users/GetUserByEmail";
            string servicePrefix = "api";
            User user = new User ();
            user.Email = Email;
            user.Password = Password;

            Response response = await _apiService.Login(url, servicePrefix, controller, user);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error,
                        Languages.CredentialsInvalid,
                        Languages.Accept);
                IsBlock = true;
                return;
            }
            else
            {
                IsRunning = false;
                
                await NavigationService.NavigateAsync($"/{nameof(WeatherMasterDetailPage)}/NavigationPage/WeatherPage");
            }
           

        }
    }
}
