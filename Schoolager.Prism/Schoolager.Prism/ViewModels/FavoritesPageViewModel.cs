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
        public FavoritesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Favorites";
        }
	}
}
