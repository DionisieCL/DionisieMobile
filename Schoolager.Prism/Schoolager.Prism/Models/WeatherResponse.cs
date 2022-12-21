using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration;

namespace Schoolager.Prism.Models
{
    public class WeatherResponse
    {
        public string _base { get; set; }


        public int Visibility { get; set; }


        public int Dt { get; set; }

        public int Timezone { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }

      


        public class Weather
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string icon { get; set; }

        }

        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }
    }

    public class Sys
    {
        public int type { get; set; }   
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }

    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public int gust { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
    }
}
