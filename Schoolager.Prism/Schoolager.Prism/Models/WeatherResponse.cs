using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration;

namespace Schoolager.Prism.Models
{
    public class WeatherResponse
    {
        public Weather[] Weather { get; set; }
        public Main Main { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string Icon { get; set; }

    }
}
