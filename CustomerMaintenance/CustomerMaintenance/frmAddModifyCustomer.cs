using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace CustomerMaintenance
{
    public partial class frmAddModifyCustomer : Form
    {
        public frmAddModifyCustomer()
        {
            InitializeComponent();
        }

        public bool addCustomer;
        public Customer customer;
        MMABooksEntities encontext = new MMABooksEntities();

        private void frmAddModifyCustomer_Load(object sender, EventArgs e)
        {
            this.LoadComboBox();
            if (addCustomer)
            {
                this.Text = "Add Customer";
                cboStates.SelectedIndex = -1;
            }
            else
            {
                this.Text = "Modify Customer";
                this.DisplayCustomerData();
            }
        }

        private void LoadComboBox()
        {
            try
            {
                // Code a query to retrieve the required information from
                // the States table, and sort the results by state name.
                // Bind the State combo box to the query results.
                var comboBoxStates =
                    from newState in encontext.States
                    select new { newState.StateCode };

                foreach (var element in comboBoxStates)
                {
                    cboStates.Items.Add(element.StateCode);
                }

                cboStates.DataSource = comboBoxStates;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void DisplayCustomerData()
        {
            txtName.Text = customer.Name;
            txtAddress.Text = customer.Address;
            txtCity.Text = customer.City;
            cboStates.SelectedValue = customer.StateCode;
            txtZipCode.Text = customer.ZipCode;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (addCustomer)
                {
                    customer = new Customer();
                    this.PutCustomerData(customer);

                    // Add the new vendor to the collection of vendors.
                    encontext.Customers.Add(customer);
                    encontext.SaveChanges();

                    try
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else
                {
                    this.PutCustomerData(customer);
                    try
                    {
                        // Update the database.
                        encontext.SaveChanges();
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        ex.Entries.Single().Reload();
                        if (encontext.Entry(customer).State
                            == EntityState.Detached)
                        {
                            MessageBox.Show("Another user has deleted that customer.",
                                "Concurrency Error");
                            this.DialogResult = DialogResult.Abort;
                        }
                        else
                        {
                            MessageBox.Show("Another user has updated that customer.",
                                "Concurrency Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                    }
                    // Add concurrency error handling.
                    // Place the catch block before the one for a generic exception.

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
        }

        private bool IsValidData()
        {
             return Validator.IsPresent(txtName) &&
                    Validator.IsPresent(txtAddress) &&
                    Validator.IsPresent(txtCity) &&
                    Validator.IsPresent(cboStates) &&
                    Validator.IsPresent(txtZipCode) &&
                    Validator.IsInt32(txtZipCode);
        }

        private void PutCustomerData(Customer customer)
        {
            customer.Name = txtName.Text;
            customer.Address = txtAddress.Text;
            customer.City = txtCity.Text;
            customer.StateCode = cboStates.SelectedItem.ToString();
            customer.ZipCode = txtZipCode.Text;
        }
    }
}
