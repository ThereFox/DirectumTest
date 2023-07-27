using ClassLibrary1.Abstractions;
using System.Globalization;

namespace ClassLibrary1
{
    public class LocalizationManager
    {
        private List<ILocalisationStringSource> _sourses;

        public string? GetString(long stringCode, CultureInfo? culture)
        {
            if(_sourses.Count== 0)
            {
                return null;
            }

            if(culture== null)
            {
                culture = Thread.CurrentThread.CurrentCulture;
            }

            var avaliableStrings = _sourses.ConvertAll(sourse => sourse.GetStringByCodeAndCultural(stringCode, culture));

            if(avaliableStrings.Any(element => element is not null) == false)
            {
                return null;
            }

            return getValidStringFromAvaliable(avaliableStrings);
        }

        private string getValidStringFromAvaliable(List<string> avaliableString)
        {
            return avaliableString.Where(localisationString => localisationString != null).First();
        }


        public void RegisterSource(ILocalisationStringSource sourse)
        {
            _sourses.Add(sourse);
        }

        public LocalizationManager()
        {
            _sourses = new();
        }
        public LocalizationManager(List<ILocalisationStringSource> sourses)
        {
            _sourses= sourses;
        }

    }
}