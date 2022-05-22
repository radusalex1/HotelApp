using System.Windows;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for AddPricesPage.xaml
    /// </summary>
    public partial class AddPricesPage : Window
    {
        public AddPricesPage()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            PricesPage pricesPage = new PricesPage();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = pricesPage;
            App.Current.MainWindow.Show();
        }
    }
}
