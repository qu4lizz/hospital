using Hospital.Utils;
using System;
using System.Windows.Controls;


namespace Hospital.XAML.Pages
{
    public partial class SettingsPage : Page
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }

        public SettingsPage()
        {
            if (UserContext.CurrentUserType == UserType.DOCTOR)
            {
                Username = DoctorWindow.CurrentDoctor.Username;
                FirstName = DoctorWindow.CurrentDoctor.Name;
                PhoneNumber = DoctorWindow.CurrentDoctor.Contact;
                Surname = DoctorWindow.CurrentDoctor.Surname;
            }
            else if (UserContext.CurrentUserType == UserType.MANAGER)
            {
                Username = ManagerWindow.CurrentManager.Username;
                FirstName = ManagerWindow.CurrentManager.Name;
                PhoneNumber = ManagerWindow.CurrentManager.Contact;
                Surname = ManagerWindow.CurrentManager.Surname;
            }
            else
            {
                Username = NurseWindow.CurrentNurse.Username;
                FirstName = NurseWindow.CurrentNurse.Name;
                PhoneNumber = NurseWindow.CurrentNurse.Contact;
                Surname = NurseWindow.CurrentNurse.Surname;
            }

            DataContext = this;
            InitializeComponent();
            this.Resources.MergedDictionaries.Add(LangHelper.GetResourceDictionary());
            InitCheckBoxes();
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)cb_lang.SelectedItem;
            string value = typeItem.Content.ToString();

            // TODO: update in database
            if (value == LangHelper.GetString("Serbian"))
            {
                LangHelper.SwitchLanguage("sr");

                if (UserContext.CurrentUserType == UserType.DOCTOR)
                {
                    DoctorWindow.CurrentDoctor.Language = "SR";
                    DoctorWindow.DoctorService.Update(DoctorWindow.CurrentDoctor);
                }
                else if (UserContext.CurrentUserType == UserType.MANAGER)
                {
                    ManagerWindow.CurrentManager.Language = "SR";
                    ManagerWindow.ManagerService.Update(ManagerWindow.CurrentManager);
                }
                else
                {
                    NurseWindow.CurrentNurse.Language = "SR";
                    NurseWindow.NurseService.Update(NurseWindow.CurrentNurse);
                }

                this.Resources.MergedDictionaries.Add(LangHelper.GetResourceDictionary());
            }
            else if (value == LangHelper.GetString("English"))
            {
                LangHelper.SwitchLanguage("en");

                if (UserContext.CurrentUserType == UserType.DOCTOR)
                {
                    DoctorWindow.CurrentDoctor.Language = "EN";
                    DoctorWindow.DoctorService.Update(DoctorWindow.CurrentDoctor);
                }
                else if (UserContext.CurrentUserType == UserType.MANAGER)
                {
                    ManagerWindow.CurrentManager.Language = "EN";
                    ManagerWindow.ManagerService.Update(ManagerWindow.CurrentManager);
                }
                else
                {
                    NurseWindow.CurrentNurse.Language = "EN";
                    NurseWindow.NurseService.Update(NurseWindow.CurrentNurse);
                }

                this.Resources.MergedDictionaries.Add(LangHelper.GetResourceDictionary());
            }
            else throw new InvalidOperationException("Invalid language selection");
        }

        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)cb_theme.SelectedItem;
            string value = typeItem.Content.ToString();


            if (value == LangHelper.GetString("LightTheme"))
            {

                if (UserContext.CurrentUserType == UserType.DOCTOR)
                {
                    DoctorWindow.CurrentDoctor.Theme = "Light";
                    DoctorWindow.DoctorService.Update(DoctorWindow.CurrentDoctor);
                    AppTheme.ChangeThemeToLight();
                }
                else if (UserContext.CurrentUserType == UserType.MANAGER)
                {
                    ManagerWindow.CurrentManager.Theme = "Light";
                    ManagerWindow.ManagerService.Update(ManagerWindow.CurrentManager);
                    AppTheme.ChangeThemeToLight();
                }
                else
                {
                    NurseWindow.CurrentNurse.Theme = "Light";
                    NurseWindow.NurseService.Update(NurseWindow.CurrentNurse);
                    AppTheme.ChangeThemeToLight();
                }
            }
            else if (value == LangHelper.GetString("DarkTheme"))
            {
                if (UserContext.CurrentUserType == UserType.DOCTOR)
                {
                    DoctorWindow.CurrentDoctor.Theme = "Dark";
                    DoctorWindow.DoctorService.Update(DoctorWindow.CurrentDoctor);
                    AppTheme.ChangeThemeToDark();
                }
                else if (UserContext.CurrentUserType == UserType.MANAGER)
                {
                    ManagerWindow.CurrentManager.Theme = "Dark";
                    ManagerWindow.ManagerService.Update(ManagerWindow.CurrentManager);
                    AppTheme.ChangeThemeToDark();
                }
                else
                {
                    NurseWindow.CurrentNurse.Theme = "Dark";
                    NurseWindow.NurseService.Update(NurseWindow.CurrentNurse);
                    AppTheme.ChangeThemeToDark();
                }
            }
            else if (value == LangHelper.GetString("GreenTheme"))
            {
                if (UserContext.CurrentUserType == UserType.DOCTOR)
                {
                    DoctorWindow.CurrentDoctor.Theme = "Green";
                    DoctorWindow.DoctorService.Update(DoctorWindow.CurrentDoctor);
                    AppTheme.ChangeThemeToGreen();
                }
                else if (UserContext.CurrentUserType == UserType.MANAGER)
                {
                    ManagerWindow.CurrentManager.Theme = "Green";
                    ManagerWindow.ManagerService.Update(ManagerWindow.CurrentManager);
                    AppTheme.ChangeThemeToGreen();
                }
                else
                {
                    NurseWindow.CurrentNurse.Theme = "Green";
                    NurseWindow.NurseService.Update(NurseWindow.CurrentNurse);
                    AppTheme.ChangeThemeToGreen();
                }
            }
            else throw new InvalidOperationException("Invalid theme selection");
        }

        private void InitCheckBoxes()
        {
            if (LangHelper.CurrentLanguage == "en")
            {
                cb_lang.SelectedItem = cbi_lang_en;
            }
            else if (LangHelper.CurrentLanguage == "sr")
            {
                cb_lang.SelectedItem = cbi_lang_sr;
            }
            else throw new InvalidOperationException("No such language");

            if (AppTheme.CurrentTheme == "Light")
            {
                cb_theme.SelectedItem = cbi_theme_light;
            }
            else if (AppTheme.CurrentTheme == "Dark")
            {
                cb_theme.SelectedItem = cbi_theme_dark;
            }
            else if (AppTheme.CurrentTheme == "Green")
            {
                cb_theme.SelectedItem = cbi_theme_green;
            }
            else throw new InvalidOperationException("No such theme");
        }

        private async void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string name = FirstNameText.Text;
            string surname = LastNameText.Text;
            string phoneNumber = PhoneNumberText.Text;
            string username = UsernameText.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(username))
            {
                new ErrorWindow(LangHelper.GetString("ErrorEmptyFields")).ShowDialog();
                return;
            }

            bool succeed;

            if (UserContext.CurrentUserType == UserType.DOCTOR)
            {
                DoctorWindow.CurrentDoctor.Name = name;
                DoctorWindow.CurrentDoctor.Surname = surname;
                DoctorWindow.CurrentDoctor.Username = username;
                DoctorWindow.CurrentDoctor.Contact = phoneNumber;


                succeed = await DoctorWindow.DoctorService.Update(DoctorWindow.CurrentDoctor);
                if (succeed)
                {
                    new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                }
                else
                {
                    new ErrorWindow(LangHelper.GetString("ErrorUpdated")).ShowDialog();
                }
                DoctorWindow.CurrentDoctor = await DoctorWindow.DoctorService.GetByIdAsync(DoctorWindow.CurrentDoctor.Id);
            }
            else if (UserContext.CurrentUserType == UserType.MANAGER)
            {
                ManagerWindow.CurrentManager.Name = name;
                ManagerWindow.CurrentManager.Surname = surname;
                ManagerWindow.CurrentManager.Username = username;
                ManagerWindow.CurrentManager.Contact = phoneNumber;


                succeed = await ManagerWindow.ManagerService.Update(ManagerWindow.CurrentManager);
                if (succeed)
                {
                    new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                }
                else
                {
                    new ErrorWindow(LangHelper.GetString("ErrorUpdated")).ShowDialog();
                }
                ManagerWindow.CurrentManager = await ManagerWindow.ManagerService.GetById(ManagerWindow.CurrentManager.Id);
            }
            else
            {
                NurseWindow.CurrentNurse.Name = name;
                NurseWindow.CurrentNurse.Surname = surname;
                NurseWindow.CurrentNurse.Username = username;
                NurseWindow.CurrentNurse.Contact = phoneNumber;


                succeed = await NurseWindow.NurseService.Update(NurseWindow.CurrentNurse);
                if (succeed)
                {
                    new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                }
                else
                {
                    new ErrorWindow(LangHelper.GetString("ErrorUpdated")).ShowDialog();
                }
                NurseWindow.CurrentNurse = await NurseWindow.NurseService.GetById(NurseWindow.CurrentNurse.Id);
            }
        }

        private async void ChangePasswordBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string oldPassword = OldPasswordText.Password;
            string newPassword = NewPasswordText.Password;

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                new ErrorWindow(LangHelper.GetString("ErrorEmptyFields")).ShowDialog();
                return;
            }
            else if (oldPassword != DoctorWindow.CurrentDoctor.Password)
            {
                new ErrorWindow(LangHelper.GetString("ErrorWrongCredentials")).ShowDialog();
                return;
            }

            bool succeed;

            if (UserContext.CurrentUserType == UserType.DOCTOR)
            {
                DoctorWindow.CurrentDoctor.Password = newPassword;

                succeed = await DoctorWindow.DoctorService.Update(DoctorWindow.CurrentDoctor);
                if (succeed)
                {
                    new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                }
                else
                {
                    new ErrorWindow(LangHelper.GetString("ErrorUpdated")).ShowDialog();
                }
                DoctorWindow.CurrentDoctor = await DoctorWindow.DoctorService.GetByIdAsync(DoctorWindow.CurrentDoctor.Id);
            }
            else if (UserContext.CurrentUserType == UserType.MANAGER)
            {
                ManagerWindow.CurrentManager.Password = newPassword;

                succeed = await ManagerWindow.ManagerService.Update(ManagerWindow.CurrentManager);
                if (succeed)
                {
                    new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                }
                else
                {
                    new ErrorWindow(LangHelper.GetString("ErrorUpdated")).ShowDialog();
                }
                ManagerWindow.CurrentManager = await ManagerWindow.ManagerService.GetById(ManagerWindow.CurrentManager.Id);
            }
            else
            {
                NurseWindow.CurrentNurse.Password = newPassword;

                succeed = await NurseWindow.NurseService.Update(NurseWindow.CurrentNurse);
                if (succeed)
                {
                    new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                }
                else
                {
                    new ErrorWindow(LangHelper.GetString("ErrorUpdated")).ShowDialog();
                }
                NurseWindow.CurrentNurse = await NurseWindow.NurseService.GetById(NurseWindow.CurrentNurse.Id);
            }

            OldPasswordText.Password = "";
            NewPasswordText.Password = "";
        }
    }
}
