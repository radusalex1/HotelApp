using System.Windows;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = startWindow;
            App.Current.MainWindow.Show();
        }
    }
}
