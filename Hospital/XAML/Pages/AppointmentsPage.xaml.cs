using Hospital.DataObjects;
using Hospital.Services;
using Hospital.Utils;
using Hospital.XAML.CreateWindows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.XAML.Pages
{
    /// <summary>
    /// Interaction logic for AppointmentsPage.xaml
    /// </summary>
    public partial class AppointmentsPage : Page
    {
        private AppointmentsService AppointmentsService = new();

        public AppointmentsPage()
        {
            InitializeComponent();
            DataContext = this;
            this.Resources.MergedDictionaries.Add(LangHelper.GetResourceDictionary());

            UpdateTable();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateAppointmentWindow().ShowDialog();

            UpdateTable();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Appointment appointment = (Appointment)button.DataContext;

            if (appointment != null)
            {
                new CreateAppointmentWindow(appointment).ShowDialog();

                UpdateTable();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Appointment appointment = (Appointment)button.DataContext;

            if (appointment != null)
            {
                if ((bool)new AreYouSureWindow().ShowDialog())
                {
                    AppointmentsService.Delete(appointment.Id);
                    new SuccessWindow(LangHelper.GetString("SuccessDeleted")).Show();
                    UpdateTable();
                }
            }
        }

        private ObservableCollection<Appointment> UpdateTable()
        {
            ObservableCollection<Appointment> appointments;
            if (UserContext.CurrentUserType == UserType.NURSE)
            {
                appointments = new(AppointmentsService.GetAll());
            }
            else
            {
                appointments = new(AppointmentsService.GetAllByDoctor(DoctorWindow.CurrentDoctor.Id));
            }
            DataGrid.ItemsSource = appointments;
            return appointments;
        }

        private void textBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Appointment> newCollection = new List<Appointment>();
            IEnumerable<Appointment> objects = (IEnumerable<Appointment>)UpdateTable();
            foreach (var obj in objects)
            {
                string filter = textBoxFilter.Text.ToLower();
                if (obj.Patient.Name.ToLower().Contains(filter) || obj.Patient.Surname.ToLower().Contains(filter) || obj.Patient.Contact.Contains(filter)
                    || obj.Date.ToString().ToLower().Contains(filter))
                    newCollection.Add(obj);
            }
            DataGrid.ItemsSource = newCollection;
        }
    }
}
