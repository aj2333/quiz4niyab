using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MedicalStoreSystem
{
    public partial class Form1: Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=09163263320;database=dbMedStore");
        MySqlCommand cmd = new MySqlCommand();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "SELECT * FROM tblAccount WHERE username = @username AND password = @password";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                MySqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.Read())
                {
                    if (txtUsername.Text == "user" && txtPassword.Text == "4321")
                    {
                        Form1 gs = new Form1();
                        gs.Show();
                        this.Hide();
                    }
                    else if (txtUsername.Text == "admin" && txtPassword.Text == "1234")
                    {
                        Admin admin = new Admin();
                        admin.Show();
                        this.Hide();
                    }
                    txtPassword.Clear();
                    txtUsername.Clear();

                }
                else
                {
                    MessageBox.Show("Invalid username and/or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsername.Clear();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
