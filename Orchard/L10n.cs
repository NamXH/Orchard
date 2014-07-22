using System;
using Xamarin.Forms;
using System.Resources;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms.Xaml;

namespace Orchard
{
    public class L10n
    {
        /// <remarks>
        /// Maybe we can cache this info rather than querying every time
        /// </remarks>
        public static string Locale()
        {
            return DependencyService.Get<ILocale>().GetCurrent();
        }

        public static string Localize(string key, string comment)
        {

            var netLanguage = Locale();

            var temp = new ResourceManager("Orchard.Resx.AppResources", typeof(L10n).GetTypeInfo().Assembly);

            var result = temp.GetString(key, new CultureInfo(netLanguage));

            return result; 
        }
    }

    public interface ILocale
    {
        string GetCurrent();
    }

    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;

            var translated = L10n.Localize(Text, null); 

            return translated;
        }
    }
}

