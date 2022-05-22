using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class UpdateRoomViewModel : NotifyPropertyChangedHelp
    {
        RoomRepository roomRepository;

        public UpdateRoomViewModel()
        {

        }

        public UpdateRoomViewModel(Room room)
        {
            this.roomRepository = new RoomRepository();
            this.Room = room;
            this.OldRoomNumber = room.RoomNumber;
            this.RoomNumber = room.RoomNumber;
            this.NumberOfPersons = room.NumberOfPersons;
            this.Category = room.Category;
            this.Features = room.Features;
        }

        private Room _Room;
        public Room Room
        {
            get { return this._Room; }
            set
            {
                this._Room = value;
            }
        }

        private int _OldRoomNumber;
        public int OldRoomNumber
        {
            get { return _OldRoomNumber; }
            set
            {
                _OldRoomNumber = value;
            }
        }

        private int _RoomNumber { get; set; }
        public int RoomNumber
        {
            get { return _RoomNumber; }
            set
            {
                _RoomNumber = value;
                if (RoomNumber != OldRoomNumber)
                {
                    if (roomRepository.CheckRoomNumber(RoomNumber) == false)
                    {
                        CanExecuteCommand = true;
                        ErrorMessage = "";
                    }
                    else
                    {
                        CanExecuteCommand = false;
                        ErrorMessage = "Room number already in user";
                    }
                }
                else
                {
                    CanExecuteCommand = true;
                    ErrorMessage = "";
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

        private ICommand updateRoomCommand;
        public ICommand UpdateRoomCommand
        {
            get
            {
                updateRoomCommand = new RelayCommand(UpdateRoom, param => CanExecuteCommand);
                return updateRoomCommand;
            }
        }

        public void UpdateRoom(object param)
        {
            Room newRoom = new()
            {
                RoomNumber = this.RoomNumber,
                NumberOfPersons = this.NumberOfPersons,
                Category = this.Category,
                Features = this.Features,
            };
            roomRepository.UpdateRoom(newRoom, this.OldRoomNumber);

            UpdateRoomsPage updateRoomsPage = new UpdateRoomsPage();
            UpdateRoomsViewModel updateRoomsViewModel = new UpdateRoomsViewModel();
            updateRoomsPage.DataContext = updateRoomsViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = updateRoomsPage;
            App.Current.MainWindow.Show();

        }

    }
}
