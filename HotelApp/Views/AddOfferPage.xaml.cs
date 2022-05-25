using System.Windows;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for AddOfferPage.xaml
    /// </summary>
    public partial class AddOfferPage : Window
    {
        public AddOfferPage()
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
