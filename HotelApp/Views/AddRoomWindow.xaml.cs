using System.Windows;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        public AddRoomWindow()
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
