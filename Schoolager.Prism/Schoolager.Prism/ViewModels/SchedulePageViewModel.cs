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
            //Events = new ObservableCollection<Event>();
        }

        public List<Event> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public async void LoadEventsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            //string url = "https://schoolager.azurewebsites.net".ToString();

            string prefix = "api";

            string controller = "/Users/GetLessonsById?id=2";

            Response response = await _apiService.GetListAsync<Event>(url, prefix, controller);


            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
            List<Event> test = new List<Event>();

            //test.Add((Event)response.Result);
            Events = (List<Event>) response.Result;

            /*List<Event> test = new List<Event>();
            Event e = new Event();
            e.Id = 1;
            e.WeekDay = 1;
            e.StartTime = new DateTime(2022, 12, 15, 8, 10, 20);
            e.EndTime = new DateTime(2022, 12, 15, 7, 10, 20);
            test.Add(e);    

            Events = test;*/

        }
    }
}
