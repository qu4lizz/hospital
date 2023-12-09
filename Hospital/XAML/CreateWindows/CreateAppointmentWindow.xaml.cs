using Hospital.DataObjects;
using Hospital.Services;
using Hospital.Utils;
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

namespace Hospital.XAML.CreateWindows
{
    /// <summary>
    /// Interaction logic for CreateAppointmentWindow.xaml
    /// </summary>
    public partial class CreateAppointmentWindow : Window
    {
        private AppointmentsService AppointmentsService = new();
        private PatientService PatientService = new();
        private DoctorService DoctorService = new();
        private Appointment? Appointment = null;

        public CreateAppointmentWindow()
        {
            InitializeComponent();
            GetPatientsAndPopulateComboBox();
            if (UserContext.CurrentUserType == UserType.NURSE)
            {
                GetDoctorsAndPopulateComboBox();
            }
            else
            {
                OneDoctorPopulateComboBox();
                cb_Doctor.SelectedItem = cb_Doctor.Items.OfType<Doctor>().FirstOrDefault(doc => doc.Id == DoctorWindow.CurrentDoctor.Id);
            }
        }

        public CreateAppointmentWindow(Appointment appointment)
        {
            InitializeComponent();

            GetPatientsAndPopulateComboBox();
            if (UserContext.CurrentUserType == UserType.NURSE)
            {
                GetDoctorsAndPopulateComboBox();
            }
            else
            {
                OneDoctorPopulateComboBox();
            }


            DatePickerField.Value = appointment.Date;
            cb_Doctor.SelectedItem = cb_Doctor.Items.OfType<Doctor>().FirstOrDefault(doc => doc.Id == appointment.DoctorId);
            cb_Patient.SelectedItem = cb_Patient.Items.OfType<Patient>().FirstOrDefault(pat => pat.Id == appointment.PatientId);
            Appointment = appointment;

            Header.Text = LangHelper.GetString("UpdateAppointment");
            CreateButton.Content = LangHelper.GetString("Update");
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? appointmentDate = DatePickerField.Value;
            Patient patient = (Patient)cb_Patient.SelectedItem;
            Doctor doctor = (Doctor)cb_Doctor.SelectedItem;
            if (appointmentDate == null || patient == null || doctor == null)
            {
                new ErrorWindow(LangHelper.GetString("ErrorEmptyFields")).ShowDialog();
                return;
            }

            if (Appointment == null)
            {
                AppointmentsService.Add(new Appointment { Date = (DateTime)appointmentDate, DoctorId = doctor.Id, PatientId = patient.Id });
                new SuccessWindow(LangHelper.GetString("SuccessCreated")).ShowDialog();
                this.Close();
            }
            else
            {
                Appointment.Date = (DateTime)appointmentDate;
                Appointment.DoctorId = doctor.Id;
                Appointment.PatientId = patient.Id;

                AppointmentsService.Update(Appointment);
                new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                this.Close();
            }
        }

        private void GetPatientsAndPopulateComboBox()
        {
            cb_Patient.ItemsSource = PatientService.GetAll();
        }

        private void GetDoctorsAndPopulateComboBox()
        {
            cb_Doctor.ItemsSource = DoctorService.GetAll();
        }

        private void OneDoctorPopulateComboBox()
        {
            List<Doctor> doctors = new List<Doctor>();
            doctors.Add(DoctorService.GetById(DoctorWindow.CurrentDoctor.Id));
            cb_Doctor.ItemsSource = doctors;
        }
    }
}
