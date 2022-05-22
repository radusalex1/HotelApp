using HotelApp.DBContext;
using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Views;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class AdminViewModel:NotifyPropertyChangedHelp
    {
        private User _User;
        public User User
        {
            get { return _User; }
            set
            {
                _User = value;
                NotifyPropertyChanged("User");
            }
        }

        public AdminViewModel()
        {

        }

        HotelContext hotelContext;

        public AdminViewModel(User user, DBContext.HotelContext hotelContext)
        {
            this.User = user;
            this.hotelContext = hotelContext;
        }

        private ICommand updateRoomsWindow;
        public ICommand UpdateRoomsWindow
        {
            get
            {
                updateRoomsWindow = new RelayCommand(OpenUpdateRoomsWindow);
                return updateRoomsWindow;
            }
        }

        public void OpenUpdateRoomsWindow(object param)
        {
            UpdateRoomsPage updateRoomsWindow = new UpdateRoomsPage();
            UpdateRoomsViewModel updateRoomsViewModel = new UpdateRoomsViewModel();
            updateRoomsWindow.DataContext = updateRoomsViewModel; 
            App.Current.MainWindow.Close();
            App.Current.MainWindow = updateRoomsWindow;
            App.Current.MainWindow.Show();
        }

        private ICommand updatePrices;
        public ICommand UpdatePrices
        {
            get
            {
                updatePrices = new RelayCommand(OpenUpdatePricesWindow);
                return updatePrices;
            }
        }

        public void OpenUpdatePricesWindow(object param)
        {
            PricesPage pricesPage=new PricesPage();
            PricesViewModel pricesViewModel=new PricesViewModel();
            pricesPage.DataContext = pricesViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = pricesPage;
            App.Current.MainWindow.Show();
        }
    }
}
