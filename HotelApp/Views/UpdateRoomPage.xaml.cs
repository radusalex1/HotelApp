using System.Windows;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for UpdateRoomWindow.xaml
    /// </summary>
    public partial class UpdateRoomPage : Window
    {
        public UpdateRoomPage()
        {
            InitializeComponent();
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            UpdateRoomsPage updateRoomsPage = new UpdateRoomsPage();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = updateRoomsPage;
            App.Current.MainWindow.Show();
        }
    }
}
