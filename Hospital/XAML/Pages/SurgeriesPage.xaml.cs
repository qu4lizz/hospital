using Hospital.DataObjects;
using Hospital.Services;
using Hospital.Utils;
using Hospital.XAML.CreateWindows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.XAML.Pages
{
    public partial class SurgeriesPage : Page
    {
        private SurgeriesService SurgeriesService = new();
        public SurgeriesPage()
        {
            InitializeComponent();
            DataContext = this;
            this.Resources.MergedDictionaries.Add(LangHelper.GetResourceDictionary());
            UpdateTable();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateSurgeryWindow().ShowDialog();

            UpdateTable();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Surgery surgery = (Surgery)button.DataContext;

            if (surgery != null)
            {
                new CreateSurgeryWindow(surgery).ShowDialog();

                UpdateTable();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Surgery surgery = (Surgery)button.DataContext;

            if (surgery != null)
            {
                if ((bool)new AreYouSureWindow().ShowDialog())
                {
                    SurgeriesService.Delete(surgery.IdSurgery);
                    new SuccessWindow(LangHelper.GetString("SuccessDeleted")).Show();
                    UpdateTable();
                }
            }
        }

        private ObservableCollection<Surgery> UpdateTable()
        {
            ObservableCollection<Surgery> surgery;
            if (UserContext.CurrentUserType == UserType.NURSE)
            {
                surgery = new(SurgeriesService.GetAll());
            }
            else
            {
                surgery = new(SurgeriesService.GetAllByDoctor(DoctorWindow.CurrentDoctor.Id));
            }
            DataGrid.ItemsSource = surgery;
            return surgery;
        }

        private void textBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Surgery> newCollection = new List<Surgery>();
            IEnumerable<Surgery> objects = (IEnumerable<Surgery>)UpdateTable();
            foreach (var obj in objects)
            {
                string filter = textBoxFilter.Text.ToLower();
                if (obj.Patient.Name.ToLower().Contains(filter) || obj.Patient.Surname.ToLower().Contains(filter) || obj.Patient.Contact.Contains(filter)
                    || obj.Notes.ToLower().Contains(filter) || obj.Date.ToString().ToLower().Contains(filter))
                    newCollection.Add(obj);
            }
            DataGrid.ItemsSource = newCollection;
        }
    }
}
