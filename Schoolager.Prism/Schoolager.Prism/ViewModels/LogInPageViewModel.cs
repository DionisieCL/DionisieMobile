using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Schoolager.Prism.Models;
using Schoolager.Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Schoolager.Prism.ViewModels
{
    public class LogInPageViewModel : ViewModelBase
    {
        private string _password;
        private bool _isRunning;
        private bool _isEnable;
        private DelegateCommand _loginCommand;

        private readonly IApiServices _apiService;

        

        public LogInPageViewModel(INavigationService navigationService, IApiServices apiService) : base(navigationService)
        {
            Title = "Login";
            IsEnable= true;

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

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an E-mail", "Accept");
                Password = string.Empty;
                return;

            }
            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a password", "Accept");
                Password = string.Empty;
                return;

            }

            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.Login(url, "api", "/Users", Email, Password);

            UserResponse userResponse = (UserResponse)response.Result;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "E-mail or Password wrong!", "Accept");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Ok", userResponse.FirstName, "Accept");
            }

        }

    }
}
