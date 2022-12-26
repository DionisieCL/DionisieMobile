using RESTCountries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolager.Prism.Models
{
    public class CityResponse
    {
        public string Name { get; set; }
        public string NativeName { get; set; }

        public string Flag { get; set; }

        public Flags Flags { get; set; }

        public double[] Latlng { get; set; }
       
    }

    public class Flags
    {
        public string Svg { get; set; }
      //  public string Png { get; set; }
    }
}
