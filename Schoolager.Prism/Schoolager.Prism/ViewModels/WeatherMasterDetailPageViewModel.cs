using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Schoolager.Prism.ItemViewModels;
using Schoolager.Prism.Models;
using Schoolager.Prism.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Schoolager.Prism.ViewModels
{
	public class WeatherMasterDetailPageViewModel : ViewModelBase
	{
        private readonly INavigationService _navigationService;

        public WeatherMasterDetailPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {

                 new Menu
               {

                Icon="ic_wb_sunny",
                PageName=$"{nameof(WeatherPage)}",
                Title="Weather"

               },


                         new Menu
               {

                Icon="ic_favorite_border",
                PageName=$"{nameof(FavoritesPage)}",
                Title="Favorites"
               },

                new Menu
               {

                Icon="ic_info_outline",
                PageName=$"{nameof(AboutPage)}",
                Title="About"
               }
            };
            Menus = new ObservableCollection<MenuItemViewModel>(

                menus.Select(m => new MenuItemViewModel(_navigationService)
                {

                    Icon =m.Icon,
                    PageName=m.PageName,
                    Title=m.Title,
                  //  IsLoginRequired=m.IsLoginRequired,

                }).ToList());
        }
    }
}
