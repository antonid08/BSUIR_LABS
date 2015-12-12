using System.Windows;
using lab7.ViewModel;


namespace lab7
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var mw = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };

            mw.Show();
        }
    }
}
