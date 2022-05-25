using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class AddOfferViewModel : NotifyPropertyChangedHelp
    {

        private User _user;
        public User user
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyPropertyChanged("user");
            }

        }


        public AddOfferViewModel()
        {

        }

        OffersRepository offerRepository;
        public AddOfferViewModel(User user)
        {
            this.user = user;
            offerRepository = new OffersRepository();
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date.AddDays(7);
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private int _NumberOfPersons;
        public int NumberOfPersons
        {
            get { return _NumberOfPersons; }
            set
            {
                _NumberOfPersons = value;
                NotifyPropertyChanged("NumberOfPersons");
            }
        }

        private int _Price;
        public int Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                NotifyPropertyChanged("Price");
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

        private bool CanExecuteCommand { get; set; } = false;

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

        private ICommand addOfferCommand;
        public ICommand AddOfferCommand
        {
            get
            {
                addOfferCommand = new RelayCommand(AddOffer, param => CanExecuteCommand);
                return addOfferCommand;
            }
        }

        public void AddOffer(object param)
        {
            Offer offer = new()
            {
                Name = this.Name,
                NumberOfPersons = this.NumberOfPersons,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                Price = this.Price
            };
            if (offerRepository.AvailableRooms(NumberOfPersons, StartDate, EndDate) == 0)
            {
                offerRepository.AddOffer(offer);
                AdminPage adminPage = new AdminPage();
                AdminViewModel adminViewModel = new AdminViewModel();
                adminPage.DataContext = adminViewModel;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = adminPage;
                App.Current.MainWindow.Show();
            }
            else
            {
                ErrorMessage = "Not available rooms in that period";
            }

        }
    }
}
