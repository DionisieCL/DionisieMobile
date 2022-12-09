﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Schoolager.Prism.ViewModels
{
    public class LogInPageViewModel : ViewModelBase
    {
        private string _password;
        private bool _isRunning;
        private bool _isEnable;
        private DelegateCommand _loginCommand;

        public LogInPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Login";
            IsEnable= true;
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

            await App.Current.MainPage.DisplayAlert("Ok", "You're logged", "Accept");
        }

    }
}
