using System.Windows;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for PricesPage.xaml
    /// </summary>
    public partial class PricesPage : Window
    {
        public PricesPage()
        {
            InitializeComponent();
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = adminPage;
            App.Current.MainWindow.Show();
        }
    }
}
