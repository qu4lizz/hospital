using Hospital.DataObjects;
using Hospital.Services;
using Hospital.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for CreateSurgeryWindow.xaml
    /// </summary>
    public partial class CreateSurgeryWindow : Window
    {
        private SurgeriesService SurgeriesService = new();
        private PatientService PatientService = new();
        private DoctorService DoctorService = new();
        private Surgery? Surgery = null;
        public CreateSurgeryWindow()
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

        public CreateSurgeryWindow(Surgery surgery)
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

            DatePickerField.Value = surgery.Date;
            cb_Doctor.SelectedItem = cb_Doctor.Items.OfType<Doctor>().FirstOrDefault(doc => doc.Id == surgery.DoctorId);
            cb_Patient.SelectedItem = cb_Patient.Items.OfType<Patient>().FirstOrDefault(pat => pat.Id == surgery.PatientId);
            notes.Text = surgery.Notes;
            Surgery = surgery;

            Header.Text = LangHelper.GetString("UpdateSurgery");
            CreateButton.Content = LangHelper.GetString("Update");
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? surgeryDate = DatePickerField.Value;
            Patient patient = (Patient)cb_Patient.SelectedItem;
            Doctor doctor = (Doctor)cb_Doctor.SelectedItem;
            string notesStr = notes.Text;
            if (surgeryDate == null || patient == null || doctor == null || string.IsNullOrEmpty(notesStr))
            {
                new ErrorWindow(LangHelper.GetString("ErrorEmptyFields")).ShowDialog();
                return;
            }

            try
            {
                if (Surgery == null)
                {
                    SurgeriesService.Add(new Surgery { Date = (DateTime)surgeryDate, Notes = notesStr, DoctorId = doctor.Id, PatientId = patient.Id });
                    new SuccessWindow(LangHelper.GetString("SuccessCreated")).ShowDialog();
                    this.Close();
                }
                else
                {
                    Surgery.DoctorId = doctor.Id;
                    Surgery.PatientId = patient.Id;
                    Surgery.Date = (DateTime)surgeryDate;
                    Surgery.Notes = notesStr;

                    SurgeriesService.Update(Surgery);
                    new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                    this.Close();
                }
            } catch (Exception ex)
            {
                new ErrorWindow(ex.InnerException.Message).ShowDialog();
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
