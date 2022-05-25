using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System.Collections.Generic;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class ReservationStatusViewModel:NotifyPropertyChangedHelp
    {
        ReservationRepository reservationsRepository;

        public ReservationStatusViewModel()
        {

        }

        public ReservationStatusViewModel(User user)
        {
            reservationsRepository=new ReservationRepository();
            Reservations=reservationsRepository.GetAllReservations();
            this.User=user;
        }

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

        private ICommand backCommand;
        public ICommand BackCommand
        {
            get
            {
                backCommand = new RelayCommand(BackToHomePage);
                return backCommand;
            }
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

        private Reservations _SelectedItemList;
        public Reservations SelectedItemList
        {
            get
            {
                return _SelectedItemList;
            }
            set
            {
                _SelectedItemList = value;
                CanExecuteCommand = true;
               

                NotifyPropertyChanged("SelectedItemList");
            }
        }

        private bool CanExecuteCommand { get; set; } = false;


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

        private ICommand _ChangeStatusCommand;
        public ICommand ChangeStatusCommand
        {
            get
            {
                _ChangeStatusCommand = new RelayCommand(ChangeStatusOfReservatios);
                return _ChangeStatusCommand;
            }
        }

        public void ChangeStatusOfReservatios(object param)
        {
            reservationsRepository.ChageStatusOfReservation(SelectedItemList);

            ReservationStatusPage reservationStatusPage = new ReservationStatusPage();
            ReservationStatusViewModel reservationStatusViewModel = new ReservationStatusViewModel(User);
            reservationStatusPage.DataContext = reservationStatusViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = reservationStatusPage;
            App.Current.MainWindow.Show();
        }



    }
}
