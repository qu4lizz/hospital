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
    public partial class CreateRecordWindow : Window
    {
        private RecordsService RecordsService = new();
        private PatientService PatientService = new();
        private DoctorService DoctorService = new();
        private Record? Record = null;
        public CreateRecordWindow()
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

        public CreateRecordWindow(Record record)
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

            DatePickerField.Value = record.Date;
            cb_Doctor.SelectedItem = cb_Doctor.Items.OfType<Doctor>().FirstOrDefault(doc => doc.Id == record.DoctorId);
            cb_Patient.SelectedItem = cb_Patient.Items.OfType<Patient>().FirstOrDefault(pat => pat.Id == record.PatientId);
            diagnosis.Text = record.Diagnosis;
            prescription.Text = record.Prescription;
            Record = record;

            Header.Text = LangHelper.GetString("UpdateRecord");
            CreateButton.Content = LangHelper.GetString("Update");
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? recordDate = DatePickerField.Value;
            Trace.WriteLine(recordDate.ToString());
            Patient patient = (Patient)cb_Patient.SelectedItem;
            Doctor doctor = (Doctor)cb_Doctor.SelectedItem;
            string prescriptionStr = prescription.Text;
            string diagnosisStr = diagnosis.Text;
            if (recordDate == null || patient == null || doctor == null || string.IsNullOrEmpty(prescriptionStr) || string.IsNullOrEmpty(diagnosisStr))
            {
                new ErrorWindow(LangHelper.GetString("ErrorEmptyFields")).ShowDialog();
                return;
            }

            try
            {
                if (Record == null)
                {
                    RecordsService.Add(new Record { Date = (DateTime)recordDate, Prescription = prescriptionStr, Diagnosis = diagnosisStr, DoctorId = doctor.Id, PatientId = patient.Id });
                    new SuccessWindow(LangHelper.GetString("SuccessCreated")).ShowDialog();
                    this.Close();
                }
                else
                {
                    Record.DoctorId = doctor.Id;
                    Record.PatientId = patient.Id;
                    Record.Date = (DateTime)recordDate;
                    Record.Prescription = prescriptionStr;
                    Record.Diagnosis = diagnosisStr;

                    RecordsService.Update(Record);
                    new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                    this.Close();
                }
            } catch (Exception ex)
            {
                new ErrorWindow(ex.Message).ShowDialog();
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
