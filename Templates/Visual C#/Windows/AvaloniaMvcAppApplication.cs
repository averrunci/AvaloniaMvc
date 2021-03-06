﻿using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Charites.Windows.Mvc;

namespace $safeprojectname$
{
    public class $safeitemrootname$ : Application
    {
        public $safeitemrootname$()
        {
            var avaloniaBaseUri = new Uri("avares://Avalonia.Themes.Default");
            Styles.Add(new StyleInclude(avaloniaBaseUri) { Source = new Uri("DefaultTheme.xaml", UriKind.Relative) });
            Styles.Add(new StyleInclude(avaloniaBaseUri) { Source = new Uri("Accents/BaseDark.xaml", UriKind.Relative) });

            var baseUri = new Uri("avares://$safeprojectname$");
            Styles.Add(new StyleInclude(baseUri) { Source = new Uri("Resources/Styles.xaml", UriKind.Relative) });
            DataTemplates.Add(new DataTemplateInclude(baseUri) { Source = new Uri("Resources/Templates.xaml", UriKind.Relative) });
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new Window
                {
                    DataContext = new $safeprojectname$Host()
                };
                desktop.MainWindow.Classes.Add("window");

#if DEBUG
                desktop.MainWindow.AttachDevTools();
#endif
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
