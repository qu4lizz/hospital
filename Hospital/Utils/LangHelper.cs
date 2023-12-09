using System.Resources;
using System.Reflection;
using System.Globalization;
using System;
using System.Windows;

namespace Hospital.Utils
{
    public static class LangHelper
    {
        private static ResourceDictionary ResourceDictionary = new ResourceDictionary();
        public static string CurrentLanguage;

        public static event EventHandler LanguageChanged;

        private static void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(null, EventArgs.Empty);
        }

        public static string GetString(string name)
        {
            if (ResourceDictionary.Contains(name))
            {
                return ResourceDictionary[name] as string;
            }
            throw new InvalidOperationException("Invalid key");
        }

        public static void SwitchLanguage(string language)
        {
            switch (language)
            {
                case "SR":
                case "sr":
                    ResourceDictionary.Source = new Uri("../Language/Strings.sr.xaml", UriKind.Relative);
                    CurrentLanguage = "sr";
                    break;
                case "EN":
                case "en":
                    ResourceDictionary.Source = new Uri("../Language/Strings.en.xaml", UriKind.Relative);
                    CurrentLanguage = "en";
                    break;
                default:
                    throw new InvalidOperationException("Invalid language");
            }
            OnLanguageChanged();
        }

        public static ResourceDictionary GetResourceDictionary()
        {
            return ResourceDictionary;
        }
    }
}