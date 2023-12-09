using Hospital.Services;
using Hospital.Utils;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.XAML
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private AuthService AuthService = new();
        public Register()
        {
            InitializeComponent();
            DataContext = this;
            this.Resources.MergedDictionaries.Add(LangHelper.GetResourceDictionary());
        }

        private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = FirstNameText.Text;
            string surname = LastNameText.Text;
            string phoneNumber = PhoneNumberText.Text;
            string username = UsernameText.Text;
            string password = PasswordText.Password;

            bool succeed = false;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                new ErrorWindow(LangHelper.GetString("ErrorEmptyFields")).ShowDialog();
                return;
            }

            if (rb_doctor.IsChecked == true)
            {
                succeed = await Task.Run(() => AuthService.RegisterDoctor(name, surname, phoneNumber, username, password));
            }
            else if (rb_manager.IsChecked == true) 
            {
                succeed = await Task.Run(() => AuthService.RegisterManager(name, surname, phoneNumber, username, password));
            }
            else
            {
                succeed = await Task.Run(() => AuthService.RegisterNurse(name, surname, phoneNumber, username, password));
            }

            if (succeed)
            {
                new SuccessWindow(LangHelper.GetString("SuccessNewRegistration")).ShowDialog();
                this.Close();
            }
            else
                new ErrorWindow(LangHelper.GetString("ErrorUsernameAlreadyExists")).ShowDialog();
        }
    }
}
