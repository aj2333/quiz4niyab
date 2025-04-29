using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalStoreSystem
{
    public partial class AddMed: Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=09163263320;database=dbMedStore");
        MySqlCommand cmd = new MySqlCommand();
        public AddMed()
        {
            InitializeComponent();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to add this Medicine?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    MySqlTransaction transaction = con.BeginTransaction();
                    string sql = "INSERT INTO tblMedicine(medID, brand_name, generic_name, description, quantity, price, date_arrived, date_expiry) VALUES (@medID, @brand_name, @generic_name, @description, @quantity, @price, @date" +
                        "_arrived, @date_expiry)";
                    MySqlCommand cmd = new MySqlCommand(sql, con, transaction);
                    cmd.Parameters.AddWithValue("@medID", txtMedID.Text);
                    cmd.Parameters.AddWithValue("@brand_name", txtBrand.Text);
                    cmd.Parameters.AddWithValue("@generic_name", txtGeneric.Text);
                    cmd.Parameters.AddWithValue("@description", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(txtQuantity.Text));
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@date_arrived", dateArrived.Value.Date);
                    cmd.Parameters.AddWithValue("@date_expiry", dateExpiry.Value.Date);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Record added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    transaction.Commit();
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Record not added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMedID.Clear();
            txtBrand.Clear();
            txtGeneric.Clear();
            txtDesc.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            dateArrived.Value = DateTime.Today;
            dateExpiry.Value = DateTime.Today;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Admin a = new Admin();
            a.Show();
            this.Hide();
        }

        private void AddMed_Load(object sender, EventArgs e)
        {

        }
    }
}
