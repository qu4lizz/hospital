using Hospital.DataObjects;
using Hospital.Services;
using Hospital.Utils;
using Hospital.XAML.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.XAML
{
    /// <summary>
    /// Interaction logic for NurseWindow.xaml
    /// </summary>
    public partial class NurseWindow : Window
    {
        public static Nurse CurrentNurse;
        public static NurseService NurseService;
        public NurseWindow(Nurse loggedIn)
        {
            CurrentNurse = loggedIn;
            NurseService = new();

            InitializeComponent();
            DataContext = this;
            this.Resources.MergedDictionaries.Add(LangHelper.GetResourceDictionary());
        }

        private void MouseClickAdmissions(object sender, MouseButtonEventArgs e)
        {
            InitSidebarColors();
            admissionsTextBlock.SetResourceReference(Control.StyleProperty, "SidebarSelectedTextBlock");
            MainPage.Content = new AdmissionsPage();
        }

        private void MouseClickAppointments(object sender, MouseButtonEventArgs e)
        {
            InitSidebarColors();
            appointmentsTextBlock.SetResourceReference(Control.StyleProperty, "SidebarSelectedTextBlock");
            MainPage.Content = new AppointmentsPage();
        }

        private void MouseClickRecords(object sender, MouseButtonEventArgs e)
        {
            InitSidebarColors();
            recordsTextBlock.SetResourceReference(Control.StyleProperty, "SidebarSelectedTextBlock");
            MainPage.Content = new RecordsPage();
        }

        private void MouseClickPatients(object sender, MouseButtonEventArgs e)
        {
            InitSidebarColors();
            patientsTextBlock.SetResourceReference(Control.StyleProperty, "SidebarSelectedTextBlock");
            MainPage.Content = new PatientsPage();
        }

        private void MouseClickSettings(object sender, MouseButtonEventArgs e)
        {
            InitSidebarColors();
            settingsTextBlock.SetResourceReference(Control.StyleProperty, "SidebarSelectedTextBlock");
            MainPage.Content = new SettingsPage();
        }

        private void InitSidebarColors()
        {
            admissionsTextBlock.SetResourceReference(Control.StyleProperty, "SidebarTextBlock");
            appointmentsTextBlock.SetResourceReference(Control.StyleProperty, "SidebarTextBlock");
            recordsTextBlock.SetResourceReference(Control.StyleProperty, "SidebarTextBlock");
            patientsTextBlock.SetResourceReference(Control.StyleProperty, "SidebarTextBlock");
            settingsTextBlock.SetResourceReference(Control.StyleProperty, "SidebarTextBlock");
        }

        public void UpdateLanguage()
        {
            this.Resources.MergedDictionaries.Add(LangHelper.GetResourceDictionary());
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }
    }
}
