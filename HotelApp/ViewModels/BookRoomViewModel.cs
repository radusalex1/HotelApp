using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class BookRoomViewModel : NotifyPropertyChangedHelp
    {
        RoomRepository roomRepository;
        ReservationRepository reservationRepository;
        PricesRepository pricesRepository;

        public BookRoomViewModel()
        {

        }

        public BookRoomViewModel(User user)
        {
            this.User = user;

            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date.AddDays(3);
            roomRepository = new RoomRepository();
            reservationRepository=new ReservationRepository();
            pricesRepository=new PricesRepository();

        }

        private int _Price;
        public int Price
        {
           get { return _Price; }
           set { 
                _Price = value;
                NotifyPropertyChanged("Price");

            }
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

        private int _NumberOfPersons;
        public int NumberOfPersons
        {
            get { return _NumberOfPersons; }
            set { _NumberOfPersons = value; NotifyPropertyChanged("NumberOfPersons"); }
        }

        private DateTime _StartDate;
        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;

                CanExecuteCommand = true;
                ErrorMessage = "";

                if (EndDate < StartDate)
                {
                    CanExecuteCommand = false;
                    ErrorMessage = "Wrong Dates";
                }
                if (StartDate < DateTime.Now.Date)
                {
                    CanExecuteCommand = false;
                    ErrorMessage = "Wrong Dates";
                }
                EndDate = StartDate.AddDays(3);
                NotifyPropertyChanged("StartDate");
            }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                _EndDate = value;

                CanExecuteCommand = true;
                ErrorMessage = "";

                if (EndDate < StartDate)
                {
                    CanExecuteCommand = false;
                    ErrorMessage = "Wrong Dates";
                }

                NotifyPropertyChanged("EndDate");
            }
        }

        private string _ErrorMessage;
        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                _ErrorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        private Room _SelectedItemList;
        public Room SelectedItemList
        {
            get
            {
                return _SelectedItemList;
            }
            set
            {
                _SelectedItemList = value;
                CanExecuteCommand = true;
                if(SelectedItemList != null)
                {
                        Price = CalculatePrice();
                }
               
                NotifyPropertyChanged("SelectedItemList");
            }
        }

        private ObservableCollection<Room> _Rooms;
        public ObservableCollection<Room> Rooms
        {
            get
            {
                return _Rooms;
            }
            set
            {
                _Rooms = value;
                NotifyPropertyChanged("Rooms");
            }
        }

        private bool CanExecuteCommand { get; set; } = false;

        private ICommand showAvailableRoomsCommand;
        public ICommand ShowAvailableRoomsCommand
        {
            get
            {
                showAvailableRoomsCommand = new RelayCommand(ShowAvailableRooms, param => CanExecuteCommand);
                return showAvailableRoomsCommand;
            }
        }

        public void ShowAvailableRooms(object param)
        {
            Rooms = roomRepository.GetAvailableRooms(StartDate.Date, EndDate.Date, NumberOfPersons);
        }

        private ICommand bookRoomCommand;
        public ICommand BookRoomCommand
        {
            get
            {
                bookRoomCommand = new RelayCommand(BookRoom, param => CanExecuteCommand);
                return bookRoomCommand;
            }
        }

        public void BookRoom(object param)
        {
            Reservations reservations = new Reservations()
            {
                Name=User.Name,
                User=User,
                Room=SelectedItemList,
                StartDate=StartDate,
                EndDate=EndDate,
                Price=Price
            };
            reservationRepository.AddReservation(reservations);
        }

        private int CalculatePrice()
        {
            ///nu calculeaza bine. acm da:)))
            int totalPrice = 0;

            Reservations reservation1 = new()
            {
                StartDate = this.StartDate,
                EndDate = this.EndDate
            };

            var pricesList = pricesRepository.GetPricesForRoom(SelectedItemList.Id);

            foreach (var price2 in pricesList)
            {
                if (reservation1.StartDate >= price2.StartDate && reservation1.EndDate <= price2.EndDate)
                {
                    ///caz1
                    totalPrice += (reservation1.EndDate - reservation1.StartDate).Days * price2.PricePerNight;
                    break;
                }
                else
                {
                    if (reservation1.StartDate <= price2.EndDate && reservation1.StartDate >= price2.StartDate)
                    {
                        totalPrice += (price2.EndDate - reservation1.StartDate).Days * price2.PricePerNight;
                    }
                    else
                    {
                        if (reservation1.EndDate >= price2.StartDate && reservation1.EndDate <= price2.EndDate)
                        {
                            totalPrice += (reservation1.EndDate - price2.StartDate).Days * price2.PricePerNight;
                        }
                        else
                        {
                            ///aduna tot intervalul
                            if(reservation1.StartDate<=price2.StartDate && reservation1.EndDate>=reservation1.EndDate)
                            {
                                totalPrice += (price2.EndDate - price2.StartDate).Days * price2.PricePerNight;
                            }
                        }
                    }
                }
            }

            return totalPrice;
        }
    }
}
