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
        public int Visibility { get; set; }

        public Wind Wind { get; set; }

        public Sys Sys { get; set; }

        public int Timezone { get; set; }

    }

    public class Weather
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string Icon { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
    }

    public class Sys
    {
        public int Sunrise;
        public int Sunset;
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }

    }
}
