using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Schoolager.Prism.Models;
using Schoolager.Prism.Services;
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

        private readonly IApiServices _apiService;

        

        public LogInPageViewModel(INavigationService navigationService, IApiServices apiService) : base(navigationService)
        {
            Title = "Login";
            IsEnable= true;
            IsRunning= false;
            IsBlock= true;


            _apiService = apiService;

        }
        public string Email { get; set; }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

       
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
                    await App.Current.MainPage.DisplayAlert("Error", 
                        "Check internet connection", 
                        "Accept");

                });
                return; 
            }
            IsBlock = false;

            IsRunning = true;
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an E-mail", "Accept");
                Password = string.Empty;
                IsBlock = true;
                IsRunning = false;

                return;

            }
            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a password", "Accept");
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

            IsRunning = false;
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "E-mail or Password wrong!", "Accept");
                IsBlock = true;
                return;
            }
            else
            {
                await NavigationService.NavigateAsync("NavigationPage/MenuPage");
            }
           

        }

    }
}
