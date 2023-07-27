using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Abstractions
{
    public interface ILocalisationStringSource
    {
        public string? GetStringByCodeAndCultural(long cultural, CultureInfo culture);
    }
}
