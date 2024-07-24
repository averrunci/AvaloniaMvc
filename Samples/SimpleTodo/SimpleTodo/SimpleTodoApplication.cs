// Copyright (C) 2022-2024 Fievus
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
        base.Initialize();

        Styles.Add(new FluentTheme());
        DataTemplates.Add(new AvaloniaMvcDataTemplates());

        var baseUri = new Uri("avares://SimpleTodo");
        Styles.Add(new StyleInclude(baseUri) { Source = new Uri("Resources/Styles.axaml", UriKind.Relative) });
        DataTemplates.Add(new DataTemplateInclude(baseUri) { Source = new Uri("Resources/Templates.axaml", UriKind.Relative) });
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();

        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
        
        desktop.MainWindow = new Window
        {
            DataContext = new SimpleTodoHost(),
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };
        desktop.MainWindow.Classes.Add("window");
#if DEBUG
        desktop.MainWindow.AttachDevTools();
#endif
    }
}