using Hospital.Services;
using Hospital.XAML;
using Hospital.Utils;
using System.Threading.Tasks;
using System.Windows;
using Hospital.DataObjects;

namespace Hospital
{
    public partial class Login : Window
    {
        private AuthService AuthService = new();

        public Login()
        {
            LangHelper.SwitchLanguage("sr");
            InitializeComponent();
            DataContext = this;
        }

        private async void OnLoginClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameText.Text;
            string password = PasswordText.Password;
            Doctor doctor;
            Manager manager;
            Nurse nurse;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) 
            {
                new ErrorWindow(LangHelper.GetString("ErrorEmptyFields")).ShowDialog();
                return;
            }

            if ((manager = await Task.Run(() => AuthService.LoginManager(username, password))) != null)
            {
                UserContext.CurrentUserType = UserType.MANAGER;
                new ManagerWindow(manager).Show();
                this.Close();
                
                LangHelper.SwitchLanguage(manager.Language);
                AppTheme.SwitchTheme(manager.Theme);
            }
            else if ((doctor = await Task.Run(() => AuthService.LoginDoctor(username, password))) != null)
            {
                UserContext.CurrentUserType = UserType.DOCTOR;
                new DoctorWindow(doctor).Show();
                this.Close();

                LangHelper.SwitchLanguage(doctor.Language);
                AppTheme.SwitchTheme(doctor.Theme);
            }
            else if ((nurse = await Task.Run(() => AuthService.LoginNurse(username, password))) != null)
            {
                UserContext.CurrentUserType = UserType.NURSE;
                new NurseWindow(nurse).Show();
                this.Close();

                LangHelper.SwitchLanguage(nurse.Language);
                AppTheme.SwitchTheme(nurse.Theme);
            }
            else
            {
                new ErrorWindow(LangHelper.GetString("ErrorWrongCredentials")).ShowDialog();
            }

        }
    }
}
