﻿using Prism.Navigation.Xaml;
using System;
using Xamarin.Forms;

namespace Schoolager.Prism.Views
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
          
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }
        public void GoSchedule(object obj,EventArgs args)
        {
            Navigation.PushAsync(new SchedulePage());
        }
        public void GoMessages(object obj, EventArgs args)
        {
            Navigation.PushAsync(new MessagePage());
        }
    }
}
