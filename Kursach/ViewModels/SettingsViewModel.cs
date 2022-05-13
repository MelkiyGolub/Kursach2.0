using Kursach.Objects;
using Kursach.Properties;
using System.Collections.Generic;

namespace Kursach.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private static List<string> _themes = new()
        {
            @"\Styles\YellowTheme.xaml",
            @"\Styles\SeroburoMalinovayaTheme.xaml",
            @"\Styles\CyanTheme.xaml",
            @"\Styles\PinkTheme.xaml",
            @"\Styles\LightPinkTheme.xaml",
            @"\Styles\LightSerburmalTheme.xaml",
            @"\Styles\LightBamblblinTheme.xaml",
            @"\Styles\LightCyan.xaml"

        };
        private static List<string> _themesNames = new()
        {
            "Yellow",
            "SeroburoMalinovayaTheme",
            "Cyan",
            "Pink",
            "Light Pink",
            "Light Serburmal",
            "Light Bamblblin",
            "Light Cyan"
        };

        public Command SetYellowThemeCommand { get; } = new(o =>
            SetTheme("Yellow"));

        public Command SetSeroburoMalinovayaThemeCommand { get; } = new(o =>
            SetTheme("SeroburoMalinovayaTheme"));

        public Command SetCyanThemeCommand { get; } = new(o =>
            SetTheme("Cyan"));

        public Command SetPinkThemeCommand { get; } = new(o =>
            SetTheme("Pink"));

        public Command SetLightPinkThemeCommand { get; } = new(o =>
            SetTheme("Light Pink"));

        public Command SetLightSerburmalThemeCommand { get; } = new(o =>
            SetTheme("Light Serburmal"));

        public Command SetLightBamblblinThemeCommand { get; } = new(o =>
            SetTheme("Light Bamblblin"));

        public Command SetLightCyanThemeCommand { get; } = new(o =>
            SetTheme("Light Cyan"));

        public static void SetTheme(string themeName)
        {
            var index = (byte)_themesNames.IndexOf(themeName);
            if (index > _themes.Count) return;

            App.ThemeDictionary.Source = new(_themes[index], System.UriKind.RelativeOrAbsolute);

            Settings.Default.themeIndex = index;
            Settings.Default.Save();
        }

        public static void InitTheme()
        {
            SetTheme(_themesNames[Settings.Default.themeIndex]);
        }
    }
}
