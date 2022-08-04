using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Fluent;
using Charites.Windows.Mvc;

namespace AvaloniaMvcApp;

public class AvaloniaMvcAppApplication : Application
{
    public override void Initialize()
    {
        Styles.Add(new FluentTheme(new Uri("avares://AvaloniaMvcApp/Resources/Styles")) { Mode = FluentThemeMode.Dark });

        var baseUri = new Uri("avares://AvaloniaMvcApp");
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
                DataContext = new AvaloniaMvcAppHost()
            };
            desktop.MainWindow.Classes.Add("window");

#if DEBUG
            desktop.MainWindow.AttachDevTools();
#endif
        }

        base.OnFrameworkInitializationCompleted();
    }
}
