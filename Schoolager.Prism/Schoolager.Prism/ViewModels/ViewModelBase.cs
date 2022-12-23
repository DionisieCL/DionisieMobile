using Prism.Mvvm;
using Prism.Navigation;
using Schoolager.Prism.Services;
using Schoolager.Prism.Models;
using Xamarin.Forms;
using System;

namespace Schoolager.Prism.ViewModels
{
    public class ViewModelBase: BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        public ViewModelBase(INavigationService navigationService) 
        {
            _navigationService = navigationService;
        }

        public string Title { get; set; }
        public INavigationService NavigationService { get; set; }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }


    }
}