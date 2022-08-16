// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Fluent;
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleTodo;

public class SimpleTodoApplication : Application
{
    public override void Initialize()
    {
        Styles.Add(new FluentTheme(new Uri("avares://SimpleTodo/Resources/Styles")) { Mode = FluentThemeMode.Dark });

        var baseUri = new Uri("avares://SimpleTodo");
        Styles.Add(new StyleInclude(baseUri) { Source = new Uri("Resources/Styles.axaml", UriKind.Relative) });
        DataTemplates.Add(new DataTemplateInclude(baseUri) { Source = new Uri("Resources/Templates.axaml", UriKind.Relative) });

        base.Initialize();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Window
            {
                DataContext = new SimpleTodoHost()
            };
            desktop.MainWindow.Classes.Add("window");
        }

        base.OnFrameworkInitializationCompleted();
    }
}