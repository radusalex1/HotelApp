using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class HomeViewModel : NotifyPropertyChangedHelp
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

        private Offer _SelectedItemList;
        public Offer SelectedItemList
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


        private ObservableCollection<Offer> _Offers;
        public ObservableCollection<Offer> Offers
        {
            get
            {
                return _Offers;
            }
            set
            {
                _Offers = value;
                NotifyPropertyChanged("Offers");
            }
        }

        private bool CanExecuteCommand { get; set; } = false;


        public OffersRepository offersRepository;
        public ReservationRepository reservationRepository;
        public RoomRepository roomRepository;

        public HomeViewModel()
        {
            this.offersRepository = new OffersRepository();
            this.reservationRepository = new ReservationRepository();
            this.roomRepository = new RoomRepository();
            this.Offers = new ObservableCollection<Offer>(offersRepository.GetUpcomingOffers());
        }

        public HomeViewModel(User user)
        {
            this.User = user;
            this.offersRepository = new OffersRepository();
            this.reservationRepository = new ReservationRepository();
            this.roomRepository = new RoomRepository();
            this.Offers = new ObservableCollection<Offer>(offersRepository.GetUpcomingOffers());
        }


        private ICommand bookCommand;
        public ICommand BookCommand
        {
            get
            {
               
               bookCommand = new RelayCommand(BookOffer, param => CanExecuteCommand);
                
                return bookCommand;
            }
        }

        public void BookOffer(object param)
        {
            Reservations reservation = new()
            {
                Name = SelectedItemList.Name,
                UserId = User.Id,
                IsOffer = true,
                StartDate=SelectedItemList.StartDate,
                EndDate=SelectedItemList.EndDate,
                Price=SelectedItemList.Price,
               
            };

            if (MessageBox.Show("Do You want to pay know?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                reservation.Status = "unpaid";
            }
            else
            {
                reservation.Status = "paid";
            }

            reservationRepository.AddReservation(reservation,SelectedItemList.NumberOfPersons);

        }

        private ICommand openBookRoomPageCommand;
        public ICommand OpenBookRoomPageCommand
        {
            get
            {
                openBookRoomPageCommand = new RelayCommand(OpenBookRoomPage);
                return openBookRoomPageCommand;
            }
        }

        public void OpenBookRoomPage(object param)
        {
            BookRoomPage bookRoomPage=new BookRoomPage();
            BookRoomViewModel bookRoomViewModel=new BookRoomViewModel(User);
            bookRoomPage.DataContext = bookRoomViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = bookRoomPage;
            App.Current.MainWindow.Show();

        }


        private ICommand openReservationHistoryPageCommand;
        public ICommand OpenReservationHistoryPageCommand
        {
            get
            {
                openReservationHistoryPageCommand = new RelayCommand(OpenReservationHistoryPage);
                return openReservationHistoryPageCommand;
            }
        }

        public void OpenReservationHistoryPage(object param)
        {
            ReservationHistoryPage reservationHistoryPage=new ReservationHistoryPage();
            ReservationHistoryViewModel reservationHistoryViewModel=new ReservationHistoryViewModel(User);
            reservationHistoryPage.DataContext = reservationHistoryViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow= reservationHistoryPage;
            App.Current.MainWindow.Show();
        }




    }
}
