using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalStoreSystem
{
    public partial class Sell: Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=09163263320;database=dbMedStore");
        MySqlCommand cmd = new MySqlCommand();

        double Price, Quantity, Total;
        double Cash, Change;
        public Sell()
        {
            InitializeComponent();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "SELECT brand_name, generic_name, description, quantity, date_expiry, price FROM tblMedicine WHERE medID = @medID";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@medID", txtMedID.Text);

                MySqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    txtBrand.Text = r["brand_name"].ToString();
                    txtGeneric.Text = r["generic_name"].ToString();
                    txtDesc.Text = r["description"].ToString();
                    dateExpiry.Value = Convert.ToDateTime(r["date_expiry"]);
                    txtPrice.Text = r["price"].ToString();
                }
                else
                {
                    MessageBox.Show("No record found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMedID.Clear();
                }
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

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

            double.TryParse(txtPrice.Text, out Price);
            double.TryParse(txtQuantity.Text, out Quantity);

            Total = Price * Quantity;

            txtTotal.Text = Total.ToString("F2");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Save this Transaction?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (!double.TryParse(txtQuantity.Text, out Quantity) || Quantity <= 0)
                    {
                        MessageBox.Show("Please Enter a valid quantity", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    con.Open();

                    string query = "INSERT INTO tblTransaction (medID, order_quantity, date_bought, total_price, cash, change_amount) VALUES " +
                        "(@medID, @order_quantity, @date_bought, @total_price, @cash, @change_amount)";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@medID", txtMedID.Text);
                    comm.Parameters.AddWithValue("@order_quantity", txtQuantity.Text);
                    comm.Parameters.AddWithValue("@date_bought", dateBought.Value.Date);
                    comm.Parameters.AddWithValue("@total_price", txtTotal.Text);
                    comm.Parameters.AddWithValue("@cash", txtCash.Text);
                    comm.Parameters.AddWithValue("@change_amount", txtChange.Text);
                    comm.ExecuteNonQuery();

                    string Sql = "SELECT quantity FROM tblMedicine WHERE medID = @medID";
                    MySqlCommand Cmd = new MySqlCommand(Sql, con);
                    Cmd.Parameters.AddWithValue("@medID", txtMedID.Text);
                    object res = Cmd.ExecuteScalar();

                    int currentStock = Convert.ToInt32(res);

                    if (Quantity > currentStock)
                    {
                        MessageBox.Show($"Insufficient Stock, This Medicine has {currentStock} Stock Available", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    string sql = "UPDATE tblMedicine SET  quantity = quantity - @enterQuantity WHERE medID = @medID";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@medID", txtMedID.Text);
                    cmd.Parameters.AddWithValue("@enterQuantity", Quantity);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Transaction saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(txtCash.Text, out Cash);

            Change = (Cash - Total);
            txtChange.Text = Change.ToString("F2");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Admin a = new Admin();
            a.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMedID.Clear();
            txtBrand.Clear();
            txtGeneric.Clear();
            txtDesc.Clear();
            txtPrice.Clear();
            dateExpiry.Value = DateTime.Today;
            txtQuantity.Clear();
            txtTotal.Clear();
            txtCash.Clear();
            txtTotal.Clear();
            txtChange.Clear();
        }
    }
}
