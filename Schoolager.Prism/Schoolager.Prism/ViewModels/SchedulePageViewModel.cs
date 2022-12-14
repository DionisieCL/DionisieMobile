using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Schoolager.Prism.Models;

namespace Schoolager.Prism.ViewModels
{
    public class SchedulePageViewModel : ViewModelBase
    {
        public ObservableCollection<Event> Meetings { get; set; }

        public SchedulePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Meetings = new ObservableCollection<Event>();
        }
    }
}
