using Schoolager.Prism.Interfaces;
using Schoolager.Prism.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Schoolager.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);

        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string Country => Resource.Country;

        public static string FeelsLike => Resource.FeelsLike;

        public static string Pressure => Resource.Pressure;

        public static string Humidity => Resource.Humidity;

        public static string Visibility => Resource.Visibility;

        public static string MaximumTemperature => Resource.MaximumTemperature;
        public static string MinimumTemperature => Resource.MinimumTemperature;

        public static string WindSpeed => Resource.WindSpeed;

        public static string Temperature => Resource.Temperature;

        public static string Author => Resource.Author;

        public static string AppVersion => Resource.AppVersion;

        public static string Company => Resource.Company;

        public static string EnterEmail => Resource.EnterEmail;
        public static string EnterPassword => Resource.EnterPassword;   
        public static string Login => Resource.Login;   

        public static string SearchCity => Resource.SearchCity;

        public static string SearchCountry => Resource.SearchCountry;

        public static string LogOut => Resource.LogOut;

        public static string AboutPage => Resource.AboutPage;

        public static string Weather => Resource.Weather;

        public static string SearchAll => Resource.SearchAll;

        public static string MustEmail => Resource.MustEmail;

        public static string MustPassword => Resource.MustPassword;


        public static string CredentialsInvalid => Resource.CredentialsInvalid;

        public static string Search => Resource.Search;

    }
}
