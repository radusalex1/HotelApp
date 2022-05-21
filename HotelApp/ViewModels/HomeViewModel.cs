using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
       
        public HomeViewModel()
        {

        }

        public HomeViewModel(User user)
        {
            this.User = user;
            this.offersRepository = new OffersRepository();
            this.Offers = new ObservableCollection<Offer>(offersRepository.GetUpcomingOffers());
        }


        private ICommand bookCommand;
        public ICommand BookCommand
        {
            get
            {
                if(bookCommand == null)
                {
                    bookCommand = new RelayCommand(BookOffer, param => CanExecuteCommand);
                }
                return bookCommand;
            }
        }

        public void BookOffer(object param)
        {
            //Reservations reservation = new()
            //{
            //    Name = SelectedItemList.Name,
            //    User = this.User,
            //}
        }
    }
}
