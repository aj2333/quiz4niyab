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
    public partial class Admin: Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=09163263320;database=dbMedStore");
        MySqlCommand cmd = new MySqlCommand();
        public Admin()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddMed a = new AddMed();
            a.Show();
            this.Hide();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            viewMed v = new viewMed();
            v.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            Sell s = new Sell();
            s.Show();
            this.Hide();
        }

        private void btnExpired_Click(object sender, EventArgs e)
        {
            expiredMed em = new expiredMed();
            em.Show();
            this.Hide();
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            transactionHistory t = new transactionHistory();
            t.Show();
            this.Hide();
        }
    }
}
