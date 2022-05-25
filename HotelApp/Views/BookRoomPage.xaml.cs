using HotelApp.ViewModels;
using System.Windows;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for BookRoomPage.xaml
    /// </summary>
    public partial class BookRoomPage : Window
    {
        public BookRoomPage()
        {
            InitializeComponent();
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            HomeViewModel homeViewModel = new HomeViewModel();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = homePage;
            App.Current.MainWindow.Show();
        }
    }
}
