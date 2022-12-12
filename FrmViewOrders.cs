using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InnaBookStore
{
    public partial class FrmViewOrders : Form
    {
        public FrmViewOrders()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += DataGridView1_DoubleClick;
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {

            frmOrders.decide = "Yes";
            frmOrders.OrderID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frmOrders f = new frmOrders();
            f.ShowDialog();
        }

        private void FrmViewOrders_Load(object sender, EventArgs e)
        {
            Fetch();
        }

        public static string UserID = "";
        public static string Role = "";

        void Fetch()
        {

            db.connection.Open();
            string qry = "";
            if (Role == "Admin")
            {
                qry = "select OrderID, UserName, Date, Amount from Orders inner join Users on Users.UserID = Orders.UserID";
            }
            else
            {
                qry = "select OrderID, UserName, Date, Amount from Orders inner join Users on Users.UserID = Orders.UserID where Users.UserID = '"+UserID+"'  ";

            }
            SqlDataAdapter da = new SqlDataAdapter(qry, db.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;


            db.connection.Close();
        }

        private void lbtTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
