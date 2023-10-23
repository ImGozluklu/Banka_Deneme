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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-SP67UG4\SQLEXPRESS;Initial Catalog=Banka_Database;Integrated Security=True");

        public string hesap;
        private void Form2_Load(object sender, EventArgs e)
        {
            lblhesapno.Text = hesap;
            bgl.Open();
            SqlCommand komut = new SqlCommand("select * from TBLKISILER where hesapno=@p1", bgl);
            komut.Parameters.AddWithValue("@p1", hesap);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[1] + " " + dr[2];
                lbltc.Text = dr[3].ToString();
                lbltel.Text = dr[4].ToString();

            }
            bgl.Close();

            bgl.Open();
            SqlCommand komut4 = new SqlCommand("SELECT * FROM TBLHESAP WHERE HESAPNO = @l1", bgl);
            komut4.Parameters.AddWithValue("@l1", hesap);
            SqlDataReader dr2 = komut4.ExecuteReader();
            while (dr2.Read())
            {
                lblbakiye.Text = dr2["BAKIYE"].ToString();
            }
            bgl.Close();

            bgl.Open();
            SqlCommand komut5 = new SqlCommand("SELECT BAKIYE FROM TBLHESAP WHERE HESAPNO = @n1", bgl);
            komut4.Parameters.AddWithValue("@n1", hesap);
            SqlDataReader dr3 = komut4.ExecuteReader();
            if (dr3.Read())
            {
                lblbakiye.Text = dr3["BAKIYE"].ToString();
            }
            bgl.Close();

        }

        private void btngonder_Click(object sender, EventArgs e)
        {
            //para artıs

            bgl.Open();
            SqlCommand komut = new SqlCommand("Update TBLHESAP set BAKIYE=BAKIYE+@P1 WHERE HESAPNO=@P2 ", bgl);
            komut.Parameters.AddWithValue("@P1", Decimal.Parse(txttutar.Text));
            komut.Parameters.AddWithValue("@P2", mskhesap.Text);
            komut.ExecuteNonQuery();
            bgl.Close();

            //para azalış
            bgl.Open();

            SqlCommand komut2 = new SqlCommand("UPDATE tblhesap SET bakıye = bakıye - @k1 WHERE hesapno = @k2", bgl);
            komut2.Parameters.AddWithValue("@k1", decimal.Parse(txttutar.Text));
            komut2.Parameters.AddWithValue("@k2", hesap);
            komut2.ExecuteNonQuery();

            bgl.Close();
            MessageBox.Show("İşlem başarılı.");


            //hareket
            bgl.Open();
            SqlCommand hareketKomut = new SqlCommand("INSERT INTO TBLHAREKET (GONDEREN, ALICI, TUTAR) VALUES (@GONDEREN, @ALICI, @TUTAR)", bgl);
            hareketKomut.Parameters.AddWithValue("@GONDEREN", hesap);
            hareketKomut.Parameters.AddWithValue("@ALICI", mskhesap.Text);
            hareketKomut.Parameters.AddWithValue("@Tutar", Decimal.Parse(txttutar.Text));
            hareketKomut.ExecuteNonQuery();
            bgl.Close();

            // Yeniden bakiye bilgisini getir ve güncelle
            bgl.Open();
            SqlCommand komut4 = new SqlCommand("SELECT BAKIYE FROM TBLHESAP WHERE HESAPNO = @l1", bgl);
            komut4.Parameters.AddWithValue("@l1", hesap);
            SqlDataReader dr2 = komut4.ExecuteReader();
            if (dr2.Read())
            {
                lblbakiye.Text = dr2["BAKIYE"].ToString();
            }
            bgl.Close();

            




        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();
            frm.hepsa = hesap;
            frm.Show();
        }
    }
}
