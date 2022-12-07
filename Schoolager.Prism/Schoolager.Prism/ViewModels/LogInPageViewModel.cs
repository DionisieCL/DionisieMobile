using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schoolager.Prism.ViewModels
{
	public class LogInPageViewModel : ViewModelBase
	{
        public LogInPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Login";
        }
	}
}
