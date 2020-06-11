// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation;

namespace Charites.Windows.Samples.SimpleLoginDemo
{
    public class SimpleLoginDemoApplication : Application
    {
        public SimpleLoginDemoApplication()
        {
            var avaloniaBaseUri = new Uri("avares://Avalonia.Themes.Default");
            Styles.Add(new StyleInclude(avaloniaBaseUri) { Source = new Uri("DefaultTheme.xaml", UriKind.Relative) });
            Styles.Add(new StyleInclude(avaloniaBaseUri) { Source = new Uri("Accents/BaseDark.xaml", UriKind.Relative) });

            var baseUri = new Uri("avares://SimpleLoginDemo.Presentation");
            Styles.Add(new StyleInclude(baseUri) { Source = new Uri("Resources/Styles.xaml", UriKind.Relative) });
            DataTemplates.Add(new DataTemplateInclude(baseUri) { Source = new Uri("Resources/Templates.xaml", UriKind.Relative) });
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new Window
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    DataContext = new SimpleLoginDemoHost()
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
