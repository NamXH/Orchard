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

    public class EnumExt<T>
    {
        static EnumExt()
        {
            _toStr = new Dictionary<T, string>();

            foreach (var v in Enum.GetValues(typeof(T)))
            {
                _toStr.Add((T)v, Enum.GetName(typeof(T), v));
            }
        }

        protected static Dictionary<T, string> _toStr;

        public string GetDescription(T curr)
        {
            return _toStr[curr];
        }

        public IList<string> DescList()
        {
            return _toStr.Values.ToList();
        }

        public T FromDesc(string desc)
        {
            var ret = _toStr.First(x => string.CompareOrdinal(x.Value, desc) == 0);
            return ret.Key;
        }
    }

    public class OpModeEnumExt : EnumExt<Step1VM.OptimizeMode>
    {
        static OpModeEnumExt()
        {
            _toStr = new Dictionary<Step1VM.OptimizeMode, string>();
            _toStr.Add(Step1VM.OptimizeMode.LabelRate, "Label Rate");
            _toStr.Add(Step1VM.OptimizeMode.OptimizedRate, "Optimized Rate");
        }
    }

    public class EnumToPickerIdxCov<T, TEx> : IValueConverter where TEx : EnumExt<T>, new()
    {
        static EnumToPickerIdxCov()
        {
            _extObj = new TEx();
            Names = new List<string>(_extObj.DescList());
        }

        static EnumExt<T> _extObj;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var desc = _extObj.GetDescription((T)value);
            var idx = Names.IndexOf(desc);
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

            var ret = _extObj.FromDesc(name);
            return ret;
        }

        public static readonly List<string> Names;
    }

    public class OptimizeModeToPickerIdxCov : EnumToPickerIdxCov<Step1VM.OptimizeMode, OpModeEnumExt>
    {

    }

    public class LengthUnitToPickerIdxCov : EnumToPickerIdxCov<OrchardBlock.LengthUnit, EnumExt<OrchardBlock.LengthUnit>>
    {

    }

    public class AreaUnitToPickerIdxCov : EnumToPickerIdxCov<OrchardBlock.AreaUnit, EnumExt<OrchardBlock.AreaUnit>>
    {

    }

    public class SprayerCatToPickerIdxCov : EnumToPickerIdxCov<Sprayer.Cat, EnumExt<Sprayer.Cat>>
    {

    }

    public class VolumeUnitToPickerIdxCov : EnumToPickerIdxCov<Sprayer.VolumeUnit, EnumExt<Sprayer.VolumeUnit>>
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

