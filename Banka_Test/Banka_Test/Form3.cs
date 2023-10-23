using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banka_Test
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-SP67UG4\SQLEXPRESS;Initial Catalog=Banka_Database;Integrated Security=True");
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKISILER (AD,SOYAD,TC,TELEFON,HESAPNO,SIFRE) values(@p1,@p2,@p3,@p4,@p5,@p6)", bgl);
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktc.Text);
            komut.Parameters.AddWithValue("@p4", msktel.Text);
            komut.Parameters.AddWithValue("@p5", mskhesap.Text);
            komut.Parameters.AddWithValue("@p6", txtsifre.Text);
            komut.ExecuteNonQuery();

            SqlCommand hesapKomut = new SqlCommand("INSERT INTO TBLHESAP (HESAPNO, BAKIYE) VALUES (@p1, @p2)", bgl);
            hesapKomut.Parameters.AddWithValue("@p1", mskhesap.Text.ToString());
            hesapKomut.Parameters.AddWithValue("@p2", 0); 
            hesapKomut.ExecuteNonQuery();

            bgl.Close();
            MessageBox.Show("Müsteri bilgileri kaydedildi");

        }

        private void btnhesap_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            HesapVarMi(randomNumber);
            mskhesap.Text = randomNumber.ToString();

        }
        private bool HesapVarMi(int hesapNo)
        {
            bool varMi = false;
            bgl.Open();
            string query = "SELECT COUNT(*) FROM TBLHESAP WHERE HESAPNO = @HesapNo";
            using (SqlCommand command = new SqlCommand(query, bgl))
            {
                command.Parameters.AddWithValue("@HesapNo", hesapNo.ToString());
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    varMi = true;
                }
            }
            bgl.Close();
            return varMi;
        }






    }
}
