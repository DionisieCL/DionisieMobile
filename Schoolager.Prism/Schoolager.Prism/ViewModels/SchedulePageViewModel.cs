using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Schoolager.Prism.Models;
using Schoolager.Prism.Services;
using Example;
using Xamarin.Essentials;
using Syncfusion.SfSchedule.XForms;
using System.Globalization;

namespace Schoolager.Prism.ViewModels
{
    public class SchedulePageViewModel : ViewModelBase
    {
        private readonly IApiServices _apiService;
        private List<Event> _events;

        public SchedulePageViewModel(INavigationService navigationService, IApiServices apiService) : base(navigationService)
        {
            _apiService = apiService;
          
            LoadEventsAsync();
        }

        public List<Event> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public async void LoadEventsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();

            string prefix = "api";

            string controller = "/Users/GetLessonsById";

            Response response = await _apiService.GetListAsync<Event>(url, prefix, controller);


            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            Events = (List<Event>) response.Result;


        }
    }
}
