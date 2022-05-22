using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for UpdateRoomsPage.xaml
    /// </summary>
    public partial class UpdateRoomsPage : Window
    {
        public UpdateRoomsPage()
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
