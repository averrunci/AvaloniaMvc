// Copyright (C) 2020-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Fluent;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation;

namespace Charites.Windows.Samples.SimpleLoginDemo;

public class SimpleLoginDemoApplication : Application
{
    public override void Initialize()
    {
        base.Initialize();
        
        Styles.Add(new FluentTheme());

        var baseUri = new Uri("avares://SimpleLoginDemo.Presentation");
        Styles.Add(new StyleInclude(baseUri) { Source = new Uri("/Resources/Styles.axaml", UriKind.Relative) });
        DataTemplates.Add(new DataTemplateInclude(baseUri) { Source = new Uri("/Resources/Templates.axaml", UriKind.Relative) });
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();

        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
        
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
}