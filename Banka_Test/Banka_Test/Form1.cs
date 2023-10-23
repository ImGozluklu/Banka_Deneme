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

namespace Banka_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-SP67UG4\SQLEXPRESS;Initial Catalog=Banka_Database;Integrated Security=True");

        private void lnkkayıt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 fr = new Form3();
            fr.Show();

        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("select * from TBLKISILER where  HESAPNO=@HESAPNO and SIFRE=@SIFRE", bgl);
            komut.Parameters.AddWithValue("@HESAPNO", mskhesap.Text);
            komut.Parameters.AddWithValue("@SIFRE", txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form2 fr = new Form2();
                fr.hesap = mskhesap.Text;
                fr.Show();
            }
            else
            {
                MessageBox.Show("Yanliş giriş yaptınız.");
            }
            bgl.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
