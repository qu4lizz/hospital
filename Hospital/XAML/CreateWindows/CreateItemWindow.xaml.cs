using Hospital.DataObjects;
using Hospital.Services;
using Hospital.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.XAML.CreateWindows
{
    public partial class CreateItemWindow : Window
    {
        private ItemsService ItemsService = new();
        private Item? Item = null;

        public CreateItemWindow()
        {
            InitializeComponent();
        }

        public CreateItemWindow(Item item)
        {
            InitializeComponent();

            item_name.Text = item.ItemName;
            Item = item;

            Header.Text = LangHelper.GetString("UpdateRecord");
            CreateButton.Content = LangHelper.GetString("Update");
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string name = item_name.Text;

            if (name == null) {
                new ErrorWindow(LangHelper.GetString("ErrorEmptyFields")).ShowDialog();
                return;
            }

            try
            {
                if (Item == null)
                {
                    ItemsService.Add(new Item { ItemName = name});
                    new SuccessWindow(LangHelper.GetString("SuccessCreated")).ShowDialog();
                    this.Close();
                }
                else
                {
                    Item.ItemName = name;

                    ItemsService.Update(Item);
                    new SuccessWindow(LangHelper.GetString("SuccessUpdated")).ShowDialog();
                    this.Close();
                }
            } catch (Exception ex)
            {
                new ErrorWindow(ex.Message).ShowDialog();
            }
        }
    }
}
