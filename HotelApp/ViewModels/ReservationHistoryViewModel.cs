

using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class ReservationHistoryViewModel:NotifyPropertyChangedHelp
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
        ReservationRepository reservationsRepository;
        public ReservationHistoryViewModel()
        {

        }

        public ReservationHistoryViewModel(User user)
        {
            this.User = user;
            reservationsRepository = new ReservationRepository();
            Reservations = reservationsRepository.GetREservationsByUser(User);
        }

        private List<Reservations> _Reservations;
        public List<Reservations> Reservations
        {
            get
            {
                return _Reservations;
            }
            set
            {
                _Reservations = value;
                NotifyPropertyChanged("Offers");
            }
        }

        private ICommand backCommand;
        public ICommand BackCommand
        {
            get
            {
                backCommand = new RelayCommand(BackToHomePage);
                return backCommand;
            }
        }
        public void BackToHomePage(object param)
        {
            HomePage homePage = new HomePage();
            HomeViewModel homeViewModel =
                new HomeViewModel(User);
            homePage.DataContext = homeViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = homePage;
            homePage.Show();
        }
    }
}
