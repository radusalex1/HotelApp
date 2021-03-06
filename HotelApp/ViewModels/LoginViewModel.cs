using HotelApp.DBContext;
using HotelApp.Helps;
using HotelApp.Repositories;
using HotelApp.Views;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class LoginViewModel:NotifyPropertyChangedHelp
    {
        public LoginViewModel()
        {

        }
      

        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                ErrorMessage = "";
                Regex regex = new Regex(@"^[A-Za-z0-9._]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
                if (regex.Match(Email) == Match.Empty)
                {
                    ErrorMessage = "INVALID EMAIL FORMAT";
                    CanExecuteCommand = false;
                }
                else
                {
                    CanExecuteCommand = true;
                }
                NotifyPropertyChanged("Email");

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

        public bool CanExecuteCommand { get; set; } = false;

        private ICommand signInCommand;
        public ICommand SignInCommand
        {
            get
            {
                if (signInCommand == null)
                {
                    signInCommand = new RelayCommand(SignIn, param => CanExecuteCommand);
                }
                return signInCommand;
            }
        }

        public void SignIn(object param)
        {
            string password = (param as PasswordBox).Password;

            if(password.Length == 0)
            {
                MessageBox.Show("Enter your password");
            }
            else
            {
                UserRepository userRepository= new UserRepository();    
                if(userRepository.LogInCheckCredentials(Email,password))
                {
                    var user = userRepository.GetUser(Email);

                    if (user.IsAdmin == true)
                    {
                        ///pagina de admini;
                        AdminPage adminPage = new AdminPage();
                        AdminViewModel adminViewModel = 
                            new AdminViewModel(user);
                        adminPage.DataContext = adminViewModel;
                        App.Current.MainWindow.Close();
                        App.Current.MainWindow = adminPage;
                        App.Current.MainWindow.Show();

                    }
                    else
                    {
                        ///go to main menu with logged user
                        HomePage homePage = new HomePage();
                        HomeViewModel homeViewModel =
                            new HomeViewModel(user);
                        homePage.DataContext = homeViewModel;
                        App.Current.MainWindow.Close();
                        App.Current.MainWindow = homePage;
                        homePage.Show();
                    }
                }
                else
                {
                    ErrorMessage = "INVALID CREDENTIALS";
                }

            }

        }
    }
}
