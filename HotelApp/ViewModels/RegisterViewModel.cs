using HotelApp.DBContext;
using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Repositories;
using HotelApp.Views;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class RegisterViewModel : NotifyPropertyChangedHelp
    {
        #region DataMembers
        private bool ValidFirstName { get; set; } = false;
        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                Regex regex = new Regex(@"^[A-Z]{1}[a-z]+");
                if (regex.Match(firstName) == Match.Empty)
                {
                    ErrorMessage = "INVALID FIRST NAME";
                    ValidFirstName = false;
                    CanExecuteCommand = false;
                }
                else
                {
                    if (ErrorMessage == "INVALID FIRST NAME")
                    {
                        ErrorMessage = "";
                    }

                    ValidFirstName = true;

                    if (ValidFirstName && ValidLastName && ValidEmail && ValidPhoneNumber)
                    {
                        CanExecuteCommand = true;
                    }
                }

                NotifyPropertyChanged("FirstName");
            }
        }

        private bool ValidLastName { get; set; } = false;
        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                Regex regex = new Regex(@"^[A-Z]{1}[a-z]+");
                if (regex.Match(lastName) == Match.Empty)
                {
                    ErrorMessage = "INVALID LAST NAME";
                    ValidLastName = false;
                    CanExecuteCommand = false;
                }
                else
                {
                    if (ErrorMessage == "INVALID LAST NAME")
                    {
                        ErrorMessage = "";
                    }
                    ValidLastName = true;
                    if (ValidFirstName && ValidLastName && ValidEmail && ValidPhoneNumber)
                    {
                        CanExecuteCommand = true;
                    }
                }
                NotifyPropertyChanged("LastName");
            }
        }

        private bool ValidEmail { get; set; } = false;
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                try
                {
                    MailAddress address = new MailAddress(Email);
                    if (Email == address.Address)
                    {
                        if (ErrorMessage == "INVALID EMAIL FORMAT")
                        {
                            ErrorMessage = "";
                        }
                        ValidEmail = true;
                        if (ValidFirstName && ValidLastName && ValidEmail && ValidPhoneNumber)
                        {
                            CanExecuteCommand = true;
                        }
                    }
                }
                catch
                {
                    ErrorMessage = "INVALID EMAIL FORMAT";
                    ValidEmail = false;
                    CanExecuteCommand = false;
                }

                NotifyPropertyChanged("Email");
            }
        }

        private bool ValidPhoneNumber { get; set; } = false;
        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                Regex regex = new Regex(@"^0[0-9]{9}");
                if (regex.Match(phoneNumber) == Match.Empty)
                {
                    ErrorMessage = "INVALID PHONE NUMBER FORMAT";
                    ValidPhoneNumber = false;
                    CanExecuteCommand = false;
                }
                else
                {
                    if (ErrorMessage == "INVALID PHONE NUMBER FORMAT")
                    {
                        ErrorMessage = "";
                    }
                    ValidPhoneNumber = true;
                    if (ValidFirstName && ValidLastName && ValidEmail && ValidPhoneNumber)
                    {
                        CanExecuteCommand = true;
                    }
                }
                NotifyPropertyChanged("PhoneNumber");
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }
        #endregion

        #region CommandMembers
        private bool CanExecuteCommand { get; set; } = false;

        private ICommand signUpCommand;
        private HotelContext hotelContext;

        public RegisterViewModel(HotelContext hotelContext)
        {
            this.hotelContext = hotelContext;
        }

        public RegisterViewModel()
        {

        }

        public ICommand SignUpCommand
        {
            get
            {
                if (signUpCommand == null)
                {
                    signUpCommand = new RelayCommand(SignUp, param => CanExecuteCommand);
                }
                return signUpCommand;
            }
        }

        public void SignUp(object param)
        {
            string password = (param as PasswordBox).Password;


            User user = new User();
            user.Email = Email;
            user.Name = FirstName;
            user.Surname = LastName;
            user.Password = password;
            user.PhoneNumber = PhoneNumber;
            user.IsAdmin = false;

            UserRepository userRepository = new UserRepository();
            if (userRepository.ExistingUser(user) == false)
            {
                userRepository.InsertUser(user);

                LoginPage loginPage = new LoginPage();
                LoginViewModel loginViewModel = new LoginViewModel();

                loginPage.DataContext = loginViewModel;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = loginPage;
                loginPage.Show();
            }
            else
            {
                ErrorMessage = "Existing User/Change Email and password";
            }

        }
        #endregion

    }
}
