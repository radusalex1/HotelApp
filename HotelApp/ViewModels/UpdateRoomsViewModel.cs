using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class UpdateRoomsViewModel : NotifyPropertyChangedHelp
    {
        RoomRepository roomRepository;

        public UpdateRoomsViewModel()
        {
            this.roomRepository = new RoomRepository();
            Rooms = new ObservableCollection<Room>(roomRepository.GetAllRooms());
        }


        private bool CanExecuteCommand { get; set; } = false;

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

        private ICommand addRoomCommand;
        public ICommand AddRoomCommand
        {
            get
            {
                addRoomCommand = new RelayCommand(OpenAddRoomWindow);
                return addRoomCommand;
            }
        }

        public void OpenAddRoomWindow(object param)
        {
            AddRoomWindow addRoomWindow = new AddRoomWindow();
            AddRoomViewModel addRoomViewModel=new AddRoomViewModel();
            addRoomWindow.DataContext=addRoomViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = addRoomWindow;
            App.Current.MainWindow.Show();
        }

        private ICommand deleteRoomCommand;
        public ICommand DeleteRoomCommand
        {
            get
            {
               
                deleteRoomCommand = new RelayCommand(DeleteRoom,param=>CanExecuteCommand);
                
                return deleteRoomCommand;
            }
        }

        public void DeleteRoom(object param)
        {
            roomRepository.DeleteRoom(SelectedItemList);

            UpdateRoomsPage updateRoomsPage = new UpdateRoomsPage();
            UpdateRoomsViewModel updateRoomsViewModel = new UpdateRoomsViewModel();
            updateRoomsPage.DataContext = updateRoomsViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = updateRoomsPage;
            App.Current.MainWindow.Show();
        }

        private ICommand updateRoomCommand;
        public ICommand UpdateRoomCommand
        {
            get
            {
                updateRoomCommand = new RelayCommand(UpdateRoom,param=>CanExecuteCommand);
                return updateRoomCommand;
            }
        }

        public void UpdateRoom(object param)
        {
            UpdateRoomPage updateRoomPage = new UpdateRoomPage();
            UpdateRoomViewModel updateRoomViewModel=new UpdateRoomViewModel(this.SelectedItemList);
            updateRoomPage.DataContext = updateRoomViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow=updateRoomPage; ;
            App.Current.MainWindow.Show();

        }
    }
}
