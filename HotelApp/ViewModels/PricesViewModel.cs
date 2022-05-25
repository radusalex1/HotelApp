using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class PricesViewModel:NotifyPropertyChangedHelp
    {
        private PricesRepository pricesRepository;

        private Prices _SelectedItemList;
        public Prices SelectedItemList
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


        private ObservableCollection<Prices> _Prices;
        public ObservableCollection<Prices> Prices
        {
            get
            {
                return _Prices;
            }
            set
            {
                _Prices = value;
                NotifyPropertyChanged("Offers");
            }
        }

        private bool CanExecuteCommand { get; set; } = false;

        public PricesViewModel()
        {
            this.pricesRepository = new PricesRepository();
            Prices = new ObservableCollection<Prices>(pricesRepository.GetAllPrices());
        }


        private ICommand addPrice;
        public ICommand AddPrice
        {
            get
            {
                addPrice = new RelayCommand(OpenAddPriceWindow);
                return addPrice;
            }
        }

        public void OpenAddPriceWindow(object param)
        {
            AddPricesPage addPricesPage = new AddPricesPage();
            AddPricesViewModel addPricesViewModel = new AddPricesViewModel();
            addPricesPage.DataContext = addPricesViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = addPricesPage;
            App.Current.MainWindow.Show();
        }


        private ICommand deletePriceCommand;
        public ICommand DeletePriceCommand
        {
            get
            {
                deletePriceCommand = new RelayCommand(DeletePrice, param => CanExecuteCommand);
                return deletePriceCommand;
            }
        }

        public void DeletePrice(object Param)
        {
            pricesRepository.DeletePrices(this.SelectedItemList);
            PricesPage pricesPage = new PricesPage();
            PricesViewModel pricesViewModel=new PricesViewModel();
            pricesPage.DataContext = pricesViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = pricesPage;
            App.Current.MainWindow.Show();
        }
    }
}
