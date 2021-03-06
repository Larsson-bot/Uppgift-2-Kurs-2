﻿using LibaryAccess.Data;
using LibaryAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uppgift_2_Kurs_2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateErrand : Page
    {
        public CreateErrand()
        {
            this.InitializeComponent();
            LoadCustomers().GetAwaiter();
        }

        private async void btnCreateNewErrand_Click(object sender, RoutedEventArgs e)
        {
            if (tbxCustomerName.Text == "")
            {
                tbxCustomerName.Text = "EC UTBILDNING";
            }
            if (tbxErrandCatagory.Text == "")
            {
                tbxErrandCatagory.Text = "InternetIssues";
            }
            if (tbxErrandDescription.Text == "")
            {
                tbxErrandDescription.Text = "Cant connect to internet.";
            }
            await SqliteContext.CreateErrandAsync(new Errand { CustomerId = await SqliteContext.CreateCustomerAsync(new Customer { Name = tbxCustomerName.Text, Created = DateTime.Now }), Catagory = tbxErrandCatagory.Text, Description = tbxErrandDescription.Text });
            ClearAllTextBoxes();
            await LoadCustomers();
        }
        private void ClearAllTextBoxes()
        {
            tbxCustomerName.Text = "";
            tbxErrandCatagory.Text = "";
            tbxErrandDescription.Text = "";
            cbxExistingCustomers.SelectedIndex = -1;
        }

        private void ClearAllAssistingTextBoxes()
        {
            tbCombobox.Text = "";
            tbCatagory.Text = "";
            tbDescription.Text = "";
        }

        private async Task LoadCustomers()
        {
            cbxExistingCustomers.ItemsSource = await SqliteContext.GetCustomerNames();
        }

        private async void btnCreateNewErrandWithExistingCustomer_Click(object sender, RoutedEventArgs e)
        {
            ClearAllAssistingTextBoxes();

            if (cbxExistingCustomers.SelectedIndex == -1)
                tbCombobox.Text = "Please select a Customer from the combobox if you wish to create a Errand with a existing customer.";
            if (tbxErrandCatagory.Text == "")
            {
                tbCatagory.Text = "Please type in the catagory in the ErrandCatagory textbox.";
            }
            if (tbxErrandDescription.Text == "")
            {
                tbDescription.Text = "Please type in the description you want your errand to have in the ErrandDescription textbox.";
            }
            if (cbxExistingCustomers.SelectedIndex != -1 && tbxErrandCatagory.Text != "" && tbxErrandDescription.Text != "")
            {
                await SqliteContext.CreateErrandAsync(new Errand { CustomerId = await SqliteContext.GetCustomerIdByName(cbxExistingCustomers.SelectedItem.ToString()), Catagory = tbxErrandCatagory.Text, Description = tbxErrandDescription.Text });
                ClearAllTextBoxes();
            }
        }
    }
}
