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

    public class EnumToPickerIdxConverter : IValueConverter
    {
        static EnumToPickerIdxConverter()
        {
            LengthUnitNames = new List<string>(Enum.GetNames(typeof(OrchardBlock.LengthUnit)));
            AreaUnitNames = new List<string>(Enum.GetNames(typeof(OrchardBlock.AreaUnit)));
            SprayerCatNames = new List<string>(Enum.GetNames(typeof(Sprayer.Cat)));
            VolumeUnitNames = new List<string>(Enum.GetNames(typeof(Sprayer.VolumeUnit)));
        }

        public static readonly List<string> LengthUnitNames;
        public static readonly List<string> AreaUnitNames;
        public static readonly List<string> SprayerCatNames;
        public static readonly List<string> VolumeUnitNames;

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
            if (currType == typeof(Sprayer.Cat))
            {
                var idx = SprayerCatNames.IndexOf(name);
                return idx;
            }
            if (currType == typeof(Sprayer.VolumeUnit))
            {
                var idx = VolumeUnitNames.IndexOf(name);
                return idx;
            }
            throw new InvalidDataException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(OrchardBlock.LengthUnit))
            {
                return GetEnumFromIdx((int)value, LengthUnitNames, targetType);
            }
            if (targetType == typeof(OrchardBlock.AreaUnit))
            {
                return GetEnumFromIdx((int)value, AreaUnitNames, targetType);
            }
            if (targetType == typeof(Sprayer.Cat))
            {
                return GetEnumFromIdx((int)value, SprayerCatNames, targetType);
            }
            if (targetType == typeof(Sprayer.VolumeUnit))
            {
                return GetEnumFromIdx((int)value, VolumeUnitNames, targetType);
            }
            throw new InvalidDataException();
        }

        private object GetEnumFromIdx(int idx, List<string> nameList, Type eType)
        {
            if (idx < 0 || idx >= nameList.Count)
            {
                return null;
            }
            var name = nameList[idx];
            var ret = Enum.Parse(eType, name);
            return ret;
        }
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

