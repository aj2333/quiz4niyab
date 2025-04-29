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
    public partial class viewMed: Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=09163263320;database=dbMedStore");
        MySqlCommand cmd = new MySqlCommand();
        public viewMed()
        {
            InitializeComponent();
        }

        public void loadRecords()
        {
            try
            {
                con.Open();
                string sql = "SELECT medID, brand_name, generic_name, description, quantity, price,  DATE_FORMAT(date_arrived, '%M %d, %Y') AS date_arrived, DATE_FORMAT(date_expiry, '%M %d, %Y') AS date_expiry FROM tblMedicine";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();

                da.Fill(dt);
                dgvMedicine.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void viewMed_Load(object sender, EventArgs e)
        {
            loadRecords();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Admin a = new Admin();
            a.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Medicine?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    string sql = "DELETE FROM tblMedicine WHERE medID = @medID";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@medID", txtMedID.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Record deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    loadRecords();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

                txtMedID.Clear();
                txtBrand.Clear();
                txtGeneric.Clear();
                txtDesc.Clear();
                txtPrice.Clear();
                txtQuantity.Clear();
                dateArrived.Value = DateTime.Today;
                dateExpiry.Value = DateTime.Today;
            }
            else
            {
                MessageBox.Show("Record not deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvMedicine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = dgvMedicine.Rows[index];

            txtMedID.Text = row.Cells[0].Value.ToString();
            txtBrand.Text = row.Cells[1].Value.ToString();
            txtGeneric.Text = row.Cells[2].Value.ToString();
            txtDesc.Text = row.Cells[3].Value.ToString();
            txtQuantity.Text = row.Cells[4].Value.ToString();
            txtPrice.Text = row.Cells[5].Value.ToString();
            dateArrived.Value = Convert.ToDateTime(row.Cells[6].Value);
            dateExpiry.Value = Convert.ToDateTime(row.Cells[7].Value);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to update this Medicine?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    string sql = "UPDATE tblMedicine SET brand_name = @brand_name, generic_name = @generic_name, description = @description, quantity = @quantity, price = @price," +
                        "date_arrived = @date_arrived, date_expiry = @date_expiry WHERE medID = @medID";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@medID", txtMedID.Text);
                    cmd.Parameters.AddWithValue("@brand_name", txtBrand.Text);
                    cmd.Parameters.AddWithValue("@generic_name", txtGeneric.Text);
                    cmd.Parameters.AddWithValue("@description", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(txtQuantity.Text));
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@date_arrived", dateArrived.Value.Date);
                    cmd.Parameters.AddWithValue("@date_expiry", dateExpiry.Value.Date);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Record updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    loadRecords();

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
                MessageBox.Show("Record not updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "SELECT * FROM tblMedicine WHERE brand_name LIKE @brand_name";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@brand_name", txtSearch.Text);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                dgvMedicine.DataSource = dt;

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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "SELECT * FROM tblMedicine WHERE brand_name LIKE  @brand_name";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@brand_name", txtSearch.Text + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                dgvMedicine.DataSource = dt;

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
    }
}
