using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Fluent;
using Charites.Windows.Mvc;

namespace $safeprojectname$;

public class $safeitemrootname$ : Application
{
    public override void Initialize()
    {
        Styles.Add(new FluentTheme(new Uri("avares://$safeprojectname$/Resources/Styles")) { Mode = FluentThemeMode.Dark });

        var baseUri = new Uri("avares://$safeprojectname$");
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
