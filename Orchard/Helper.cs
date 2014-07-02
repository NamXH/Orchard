using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using System.Linq;
using System.Diagnostics;

namespace Orchard
{
    public static class Helper
    {
        public static string ReadTextData(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var resName = string.Format("Orchard.Data.{0}", filename);

            using (var stream = assembly.GetManifestResourceStream(resName))
            {
                using (var sr = new StreamReader(stream))
                {
                    var str = sr.ReadToEnd();
                    return str;
                }
            }
        }

        public static IEnumerable<string> ReadTextDataToLine(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var resName = string.Format("Orchard.Data.{0}", filename);

            using (var stream = assembly.GetManifestResourceStream(resName))
            {
                using (var sr = new StreamReader(stream))
                {
                    string str = null;
                    while ((str = sr.ReadLine()) != null)
                    {
                        yield return str;
                    }
                }
            }
        }

        public static string PictureFolderForType<T>()
        {
            var type = typeof(T);
            return type.ToString();
        }
    }

    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = (double)value;
            return d.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            var d = double.Parse(str);
            return d;
        }
    }

    public class EnumToPickerIdxConverter : IValueConverter
    {
        static EnumToPickerIdxConverter()
        {
            LengthUnitNames = new List<string>(Enum.GetNames(typeof(OrchardBlock.LengthUnit)));
            AreaUnitNames = new List<string>(Enum.GetNames(typeof(OrchardBlock.AreaUnit)));
        }

        public static readonly List<string> LengthUnitNames;
        public static readonly List<string> AreaUnitNames;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currType = value.GetType();
            var name = Enum.GetName(currType, value);

            if (currType == typeof(OrchardBlock.LengthUnit))
            {
                var idx = LengthUnitNames.IndexOf(name);
                return idx;
            }
            if (currType == typeof(OrchardBlock.AreaUnit))
            {
                var idx = AreaUnitNames.IndexOf(name);
                return idx;
            }
            throw new InvalidDataException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(OrchardBlock.LengthUnit))
            {
                var idx = (int)value;
                if (idx == -1)
                {
                    return null;
                }
                var name = LengthUnitNames[idx];
                var ret = Enum.Parse(targetType, name);
                return ret;
            }
            if (targetType == typeof(OrchardBlock.AreaUnit))
            {
                var idx = (int)value;
                if (idx == -1)
                {
                    return null;
                }
                var name = AreaUnitNames[idx];
                var ret = Enum.Parse(targetType, name);
                return ret;

            }
            throw new InvalidDataException();
        }
    }

    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source); 

            return imageSource;
        }
    }
}

