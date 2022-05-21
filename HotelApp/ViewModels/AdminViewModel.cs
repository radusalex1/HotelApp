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
        public AdminViewModel(User user)
        {
            this.User = user;
        }

        private ICommand addRoomWindow;
        public ICommand AddRoomWindow
        {
            get
            {
                addRoomWindow = new RelayCommand(OpenAddRoomWindow);
                return addRoomWindow;
            }
        }

        public void OpenAddRoomWindow(object param)
        {
            AddRoomWindow addRoomWindow = new AddRoomWindow();
            AddRoomViewModel addRoomViewModel = new AddRoomViewModel();
            addRoomWindow.DataContext = addRoomViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = addRoomWindow;
            App.Current.MainWindow.Show();
        }

    }
}
