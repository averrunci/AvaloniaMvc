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
        base.Initialize();

        Styles.Add(new FluentTheme());

        var baseUri = new Uri("avares://$safeprojectname$");
        Styles.Add(new StyleInclude(baseUri) { Source = new Uri("/Resources/Styles.axaml", UriKind.Relative) });
        DataTemplates.Add(new DataTemplateInclude(baseUri) { Source = new Uri("/Resources/Templates.axaml", UriKind.Relative) });
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();

        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;

        desktop.MainWindow = new Window
        {
            DataContext = new $safeprojectname$Host()
        };
        desktop.MainWindow.Classes.Add("window");
#if DEBUG
        desktop.MainWindow.AttachDevTools();
#endif
    }
}
