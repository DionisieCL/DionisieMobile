using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Schoolager.Prism.Models
{
    public class Event
    {
        public string EventName { get; set; }
        public string Organizer { get; set; }
        public string ContactID { get; set; }
        public int Capacity { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool AllDay { get; set; }
        public Color color { get; set; }
    }
}
