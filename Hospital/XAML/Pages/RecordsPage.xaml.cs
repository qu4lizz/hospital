using Hospital.DataObjects;
using Hospital.Services;
using Hospital.Utils;
using Hospital.XAML.CreateWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.XAML.Pages
{
    public partial class RecordsPage : Page
    {
        private RecordsService RecordsService = new();
        public RecordsPage()
        {
            InitializeComponent();
            DataContext = this;
            this.Resources.MergedDictionaries.Add(LangHelper.GetResourceDictionary());
            UpdateTable();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateRecordWindow().ShowDialog();

            UpdateTable();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Record record = (Record)button.DataContext;

            if (record != null)
            {
                new CreateRecordWindow(record).ShowDialog();

                UpdateTable();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Record record = (Record)button.DataContext;

            if (record != null)
            {
                if ((bool)new AreYouSureWindow().ShowDialog())
                {
                    RecordsService.Delete(record.Id);
                    new SuccessWindow(LangHelper.GetString("SuccessDeleted")).Show();
                    UpdateTable();
                }
            }
        }

        private ObservableCollection<Record> UpdateTable()
        {
            ObservableCollection<Record> records;
            if (UserContext.CurrentUserType == UserType.NURSE)
            {
                records = new(RecordsService.GetAll());
            }
            else
            {
                records = new(RecordsService.GetAllByDoctor(DoctorWindow.CurrentDoctor.Id));
            }
            DataGrid.ItemsSource = records;
            return records;
        }

        private void textBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Record> newCollection = new List<Record>();
            IEnumerable<Record> objects = (IEnumerable<Record>)UpdateTable();
            foreach (var obj in objects)
            {
                string filter = textBoxFilter.Text.ToLower();
                if (obj.Patient.Name.ToLower().Contains(filter) || obj.Patient.Surname.ToLower().Contains(filter) || obj.Patient.Contact.Contains(filter)
                    || obj.Diagnosis.ToLower().Contains(filter) || obj.Prescription.ToLower().Contains(filter) || obj.Date.ToString().ToLower().Contains(filter))
                    newCollection.Add(obj);
            }
            DataGrid.ItemsSource = newCollection;
        }
    }
}
