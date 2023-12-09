using Hospital.DataObjects;
using Hospital.Services;
using Hospital.Utils;
using Org.BouncyCastle.Bcpg;
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
    /// Interaction logic for CreatePatientWindow.xaml
    /// </summary>
    public partial class CreatePatientWindow : Window
    {
        private PatientService PatientService = new();
        private Patient? Patient = null;
        public CreatePatientWindow()
        {
            InitializeComponent();
        }

        public CreatePatientWindow(Patient patient)
        {
            InitializeComponent();

            NameText.Text = patient.Name;
            SurnameText.Text = patient.Surname;
            PhoneNumberText.Text = patient.Contact;
            AddressText.Text = patient.Address;
            DatePickerField.SelectedDate = patient.BirthDate;
            Patient = patient;
            Header.Text = LangHelper.GetString("UpdatePatient");
            CreateButton.Content = LangHelper.GetString("Update");
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = NameText.Text;
            string surname = SurnameText.Text;
            string phoneNumber = PhoneNumberText.Text;
            string address = AddressText.Text;
            DateTime? date = DatePickerField.SelectedDate;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||  string.IsNullOrWhiteSpace(phoneNumber) ||  string.IsNullOrWhiteSpace(address) || date == null) 
            {
                new ErrorWindow(LangHelper.GetString("ErrorEmptyFields")).ShowDialog();
            }
            else
            {
                if (Patient == null)
                {
                    PatientService.Add(new Patient { Address = address, BirthDate = (DateTime)date, Contact = phoneNumber, Name = name, Surname = surname });
                    new SuccessWindow(LangHelper.GetString("SuccessCreated")).ShowDialog();
                    this.Close();
                }
                else
                {
                    Patient.Surname = surname;
                    Patient.Name = name;
                    Patient.Contact = phoneNumber;
                    Patient.Address = address;
                    Patient.BirthDate = (DateTime)date;

                    PatientService.Update(Patient);
                    new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                    this.Close();
                }
            }
        }
    }
}
