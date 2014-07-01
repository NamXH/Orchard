using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

