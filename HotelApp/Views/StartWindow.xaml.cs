using HotelApp.DBContext;
using System.Windows;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        HotelContext HotelContext;
        public StartWindow(HotelContext hotelContext)
        {

            this.HotelContext = hotelContext;
            InitializeComponent();
        }
        public StartWindow()
        {
            InitializeComponent();

        }

    }
}
