using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Schoolager.Prism.Views;

namespace Schoolager.Prism.ViewModels
{
	public class FavoritesPageViewModel : ViewModelBase
    {
        private DelegateCommand _searchCommand;

        private string _response;
       
        public FavoritesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Search";

        }

        public string Response { 
            get => _response;
            set => SetProperty(ref (_response), value); }
        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ButtonClicked));

        public void ButtonClicked()
        {
            Response = "Teste";
        }

    }
}
