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
    }
}
