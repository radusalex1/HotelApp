using HotelApp.DBContext;
using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System;
using System.Collections.ObjectModel;
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

        private bool CanExecuteCommand { get; set; } = false;

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

        public AdminViewModel()
        {
            this.offersRepository = new OffersRepository();
            this.Offers = new ObservableCollection<Offer>(offersRepository.GetAllOffers());
        }

        private OffersRepository offersRepository;

        public AdminViewModel(User user)
        {
            this.User = user;
            this.User = user;
            this.offersRepository = new OffersRepository();
            this.Offers = new ObservableCollection<Offer>(offersRepository.GetAllOffers());

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

        private ICommand openAddOfferPageCommand;
        public ICommand OpenAddOfferPageCommand
        {
            get
            {
                openAddOfferPageCommand = new RelayCommand(OpenAddOfferPage);
                return openAddOfferPageCommand;
            }
        }

        public void OpenAddOfferPage(object param)
        {
            AddOfferPage addOfferPage = new AddOfferPage();
            AddOfferViewModel addOfferViewModel=new AddOfferViewModel(User);
            addOfferPage.DataContext = addOfferViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = addOfferPage;
            App.Current.MainWindow.Show();
        }

        private ICommand _OpenReservatiosStatusPageCommand;
        public ICommand OpenReservatiosStatusPageCommand
        {
            get
            {
                _OpenReservatiosStatusPageCommand = new RelayCommand(OpenStatusPage);
                return _OpenReservatiosStatusPageCommand;
            }
        }
        public void OpenStatusPage(object param)
        {
            ReservationStatusPage reservationStatusPage = new ReservationStatusPage();
            ReservationStatusViewModel reservationStatusViewModel=new ReservationStatusViewModel(User);
            reservationStatusPage.DataContext = reservationStatusViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = reservationStatusPage;
            App.Current.MainWindow.Show();
        }

    }
}
