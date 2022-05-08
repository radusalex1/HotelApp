using HotelApp.Helps;

namespace HotelApp.ViewModels
{
    public class RegisterViewModel:NotifyPropertyChangedHelp
    {
        private string _Name { get; set; }
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
            }
        }
        private string _Surname { get; set; }

        private string _Email { get; set; }

        private string _Password { get; set; }

    }
}
