using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolager.Prism.Models
{
    public class CountryWeather
    {
        public string Name { get; set; }

        public string Flag { get; set; }

        public string Description { get; set; }

        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Humidity { get; set; }

        

        public string Icon { get; set; }
    }
}
