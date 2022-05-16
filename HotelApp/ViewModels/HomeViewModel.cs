using HotelApp.Helps;
using HotelApp.Models;

namespace HotelApp.ViewModels
{
    public class HomeViewModel:NotifyPropertyChangedHelp
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyPropertyChanged("User");
            }
        }
        public HomeViewModel()
        {

        }
        public HomeViewModel(User user)
        {
            this.User= user;
        }
    }
}
