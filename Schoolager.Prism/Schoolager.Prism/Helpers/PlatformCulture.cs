using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolager.Prism.Helpers
{
    public class PlatformCulture
    {
        public string PlatformString { get; private set; }

        public string LanguageCode { get; private set; }

        public string LocaleCode { get; private set; }

        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture indentifier", "platformCultureString");
            }

            PlatformString = platformCultureString.Replace("_","-"); // .NET expects dash, not underscore
            int dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
            if(dashIndex > 0)
            {
                string[] parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocaleCode= parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = "";
            }
        }

        public override string ToString()
        {
            return PlatformString;
        }
    }
}
