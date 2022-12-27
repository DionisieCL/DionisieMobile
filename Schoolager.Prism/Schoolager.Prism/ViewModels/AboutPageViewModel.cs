using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Schoolager.Prism.Helpers;
using Schoolager.Prism.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schoolager.Prism.ViewModels
{
	public class AboutPageViewModel : ViewModelBase
	{
        public AboutPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.AboutPage;
        }
	}
}
