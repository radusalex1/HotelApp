using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class AddRoomViewModel : NotifyPropertyChangedHelp
    {
        RoomRepository roomRepository;

        public AddRoomViewModel()
        {
            this.roomRepository=new RoomRepository();
        }

        private int _RoomNumber { get; set; }
        public int RoomNumber
        {
            get { return _RoomNumber; }
            set
            {
                _RoomNumber = value;
                if(roomRepository.CheckRoomNumber(RoomNumber)==false)
                {
                    CanExecuteCommand = true;
                    ErrorMessage = "";
                }
                else
                {
                    CanExecuteCommand = false;
                    ErrorMessage = "Room number already in user";
                }
                NotifyPropertyChanged("RoomNumber");
            }
        }

        private int _NumberOfPersons { get; set; }
        public int NumberOfPersons
        {
            get { return _NumberOfPersons; }
            set
            {
                _NumberOfPersons = value;

                NotifyPropertyChanged("NumberOfPersons");
            }
        }

        private string _Category;
        public string Category
        {
            get { return _Category; }
            set { _Category = value; NotifyPropertyChanged("Category"); }
        }

        private string _Features;
        public string Features
        {
            get { return _Features; }
            set { _Features = value; NotifyPropertyChanged("Features"); }
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

        private bool CanExecuteCommand { get; set; } = false;

        private ICommand addRoomCommand;
        public ICommand AddRoomCommand
        {
            get
            {
                CanExecuteCommand = true;
                addRoomCommand = new RelayCommand(AddRoom, param => CanExecuteCommand);
                return addRoomCommand;
            }
        }

        public void AddRoom(object param)
        {
            Room room = new()
            {
                RoomNumber = this.RoomNumber,
                NumberOfPersons = this.NumberOfPersons,
                Category = this.Category,
                Features = this.Features
            };

            roomRepository.AddRoom(room);
             

            UpdateRoomsPage updateRoomsPage = new UpdateRoomsPage();
            UpdateRoomsViewModel updateRoomsViewModel= new UpdateRoomsViewModel();
            updateRoomsPage.DataContext = updateRoomsViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = updateRoomsPage;
            App.Current.MainWindow.Show();

        }
    }
}
