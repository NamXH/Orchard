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

    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = (int)value;
            return d.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            var d = int.Parse(str);
            return d;
        }
    }

    public class EnumToPickerIdxCov<T> : IValueConverter
    {
        static EnumToPickerIdxCov()
        {
            Names = new List<string>(Enum.GetNames(typeof(T)));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currType = value.GetType();
            var name = Enum.GetName(currType, value);
            var idx = Names.IndexOf(name);
            return idx;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var idx = (int)value;
            if (idx < 0 || idx >= Names.Count)
            {
                return null;
            }
            var name = Names[idx];
            var ret = Enum.Parse(targetType, name);
            return ret;
        }

        public static readonly List<string> Names;
    }

    public class LengthUnitToPickerIdxCov : EnumToPickerIdxCov<OrchardBlock.LengthUnit>
    {

    }

    public class AreaUnitToPickerIdxCov : EnumToPickerIdxCov<OrchardBlock.AreaUnit>
    {

    }

    public class SprayerCatToPickerIdxCov : EnumToPickerIdxCov<Sprayer.Cat>
    {

    }

    public class VolumeUnitToPickerIdxCov : EnumToPickerIdxCov<Sprayer.VolumeUnit>
    {

    }

    public class YearToPickerIdxConverter : IValueConverter
    {
        static YearToPickerIdxConverter()
        {
            YearNames = new List<string>();
            _currYear = DateTime.Now.Year;
            for (var i = 0; i < 100; ++i)
            {
                YearNames.Add((_currYear - i).ToString());
            }
        }

        public static readonly List<string> YearNames;

        static int _currYear;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currValYear = ((DateTime)value).Year;
            var idx = _currYear - currValYear;
            if (idx < 0 || idx > 99)
            {
                idx = -1;
            }
            return idx;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var idx = (int)value;
            var currDt = new DateTime(_currYear, 1, 1);
            return currDt.AddYears(-idx);
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

