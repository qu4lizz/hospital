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
    /// Interaction logic for CreateAdmissionWindow.xaml
    /// </summary>
    public partial class CreateAdmissionWindow : Window
    {
        private AdmissionsService AdmissionsService = new();
        private PatientService PatientService = new();
        private Admission? Admission = null;
        public CreateAdmissionWindow()
        {
            InitializeComponent();
            GetPatientsAndPopulateComboBox();
        }

        public CreateAdmissionWindow(Admission admission)
        {
            InitializeComponent();
            GetPatientsAndPopulateComboBox();

            DatePickerAdmissionField.Value = admission.AdmissionDate;
            DatePickerDischargeField.Value = admission.DischargeDate;
            cb_Patient.SelectedItem = cb_Patient.Items.OfType<Patient>().FirstOrDefault(patient => patient.Id == admission.Patient.Id);

            Admission = admission;

            Header.Text = LangHelper.GetString("UpdateAdmission");
            CreateButton.Content = LangHelper.GetString("Update");
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? dischargeDate = DatePickerDischargeField.Value;
            DateTime? admissionDate = DatePickerAdmissionField.Value;
            Patient patient = (Patient)cb_Patient.SelectedItem;
            Trace.WriteLine(patient);
            if (dischargeDate == null || admissionDate == null || patient == null)
            {
                new ErrorWindow(LangHelper.GetString("ErrorEmptyFields")).ShowDialog();
                return;
            }

            if (Admission == null)
            {
                AdmissionsService.Add(new Admission { AdmissionDate = (DateTime)admissionDate, DischargeDate = (DateTime)dischargeDate, PatientId = patient.Id });
                new SuccessWindow(LangHelper.GetString("SuccessCreated")).ShowDialog();
                this.Close();
            }
            else
            {
                Admission.AdmissionDate = (DateTime)admissionDate;
                Admission.DischargeDate = (DateTime)dischargeDate;
                Admission.PatientId = patient.Id;

                AdmissionsService.Update(Admission);
                new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                this.Close();
            }
        }

        private void GetPatientsAndPopulateComboBox()
        {
            cb_Patient.ItemsSource = PatientService.GetAll();
        }
    }
}
