using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GeographyQuizAvalonia.ViewModels;
using GeographyQuizAvalonia.Views;

namespace GeographyQuizAvalonia
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }
            // else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            // {
            //    singleViewPlatform.MainView = new MainView // Or some initial mobile view
            //    {
            //        DataContext = new MainViewModel() // Adjust as needed for mobile
            //    };
            // }


            base.OnFrameworkInitializationCompleted();
        }
    }
}
