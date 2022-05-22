using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class AddPricesViewModel : NotifyPropertyChangedHelp
    {
        PricesRepository pricesRepository;
        RoomRepository roomRepository;

        public AddPricesViewModel()
        {
            pricesRepository = new PricesRepository();
            roomRepository = new RoomRepository();
            this.Rooms = new ObservableCollection<Room>(roomRepository.GetAllRooms());
            this.StartDate = DateTime.Now.Date;
            this.EndDate = DateTime.Now.Date;
        }

        public ObservableCollection<Room> Rooms { get; }

        private Room selectedItemCombobox;
        public Room SelectedItemCombobox
        {
            get
            {
                return selectedItemCombobox;
            }
            set
            {
                selectedItemCombobox = value;
                CanExecuteCommand = false;
                NotifyPropertyChanged("SelectedItemCombobox");

            }
        }

        private int _PricePerNight;
        public int PricePerNight
        {
            get { return _PricePerNight; }
            set
            {
                _PricePerNight = value;
                NotifyPropertyChanged("PricePerNight");
            }
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

        private bool CanExecuteCommand { get; set; } = false;

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

        private ICommand addPriceCommand;

        public ICommand AddPriceCommand
        {
            get
            {
                addPriceCommand = new RelayCommand(AddPrice, param => CanExecuteCommand);
                return addPriceCommand;
            }
        }
        
        public void AddPrice(object param)
        {

            Room room = new()
            {
                NumberOfPersons = this.SelectedItemCombobox.NumberOfPersons,
                Category = this.SelectedItemCombobox.Category,
                Features = this.SelectedItemCombobox.Features,
                RoomNumber = this.SelectedItemCombobox.RoomNumber
            };

            Prices price = new()
            {
                Room=room,
                PricePerNight = this.PricePerNight,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
            };

            pricesRepository.AddPrice(price);

            PricesPage pricesPage = new PricesPage();
            PricesViewModel pricesViewModel = new PricesViewModel();
            pricesPage.DataContext = pricesViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = pricesPage;
            App.Current.MainWindow.Show();
        }
    }
}
