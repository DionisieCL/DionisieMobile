using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Schoolager.Prism.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public object Result { get; set; }
    }
}
