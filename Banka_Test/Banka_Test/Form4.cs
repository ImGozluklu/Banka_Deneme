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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public string hepsa;
        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-SP67UG4\SQLEXPRESS;Initial Catalog=Banka_Database;Integrated Security=True");

        private void Form4_Load(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut5 = new SqlCommand("select * from tblhareket where GONDEREN=@hesapno", bgl);
            komut5.Parameters.AddWithValue("@hesapno", hepsa);
            SqlDataAdapter adapter = new SqlDataAdapter(komut5);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            bgl.Close();
        }
    }
}
