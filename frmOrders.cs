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
    public partial class frmOrders : Form
    {
        public frmOrders()
        {
            InitializeComponent();
            dataGridView2.CellEndEdit += DataGridView2_CellEndEdit;
        }

        public static string UserID = "";
        public static string UserName = "";
        private void DataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                dataGridView2.CurrentRow.Cells["Total"].Value = (Convert.ToInt32(dataGridView2.CurrentRow.Cells["Price"].Value) * Convert.ToInt32(dataGridView2.CurrentRow.Cells["Qty"].Value));
                SumAmmount();
            }
            
        }

        void SumAmmount()
        {
            double sum = 0;
            try
            {
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    sum += Convert.ToDouble(dataGridView2.Rows[i].Cells["Total"].Value);
                }
                txtGrandTotal.Text = sum.ToString();

            }
            catch (Exception)
            {

            }
      
        }

        DataTable dt = new DataTable();


        public static string decide = "";
        public static string OrderID = "";
        private void frmOrders_Load(object sender, EventArgs e)
        {
            txtUser.Text = UserName;

            dt.Columns.Add("Book ID", typeof(string));
            dt.Columns.Add("Book Name", typeof(string));
            dt.Columns.Add("Percentage", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Total", typeof(string));

            dataGridView2.DataSource = dt;


            FetchID();

            FetchBooks();
            DataGridViewLinkColumn link = new DataGridViewLinkColumn();
            link.HeaderText = "Add To Cart";
            link.Text = "Add";
            link.Name = "Add";
            link.LinkBehavior = LinkBehavior.HoverUnderline;
            link.UseColumnTextForLinkValue = true;


            dataGridView1.Columns.Add(link);

            if (decide == "Yes")
            {
                FetchRecord();
            }
            else
            {

            }

        }
          

        public void FetchRecord()
        {
            db.connection.Open();

            SqlCommand cmd = new SqlCommand("select * from SearchOrders where OrderID = '"+OrderID+"' ",db.connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtOid.Text = OrderID;
                txtUser.Text = reader["UserName"].ToString();
                txtGrandTotal.Text = reader["Amount"].ToString();
                dt.Rows.Add(reader["BookID"].ToString(), reader["BookName"].ToString(), reader["PerToPublish"].ToString(), reader["Price"].ToString(), reader["Qty"].ToString(), (Convert.ToInt32(reader["Price"]) * (Convert.ToInt32(reader["Qty"]) )));

            }


            db.connection.Close();
        }
        public void FetchID()
        {

            //////////////////////////////////                                      FETCH ID

            db.connection.Open();

            SqlDataAdapter da = new SqlDataAdapter("select count(OrderID)+1 from Orders", db.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txtOid.Text = dt.Rows[0][0].ToString();

            db.connection.Close();

        }


        void FetchBooks()
        {
            db.connection.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from Books", db.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;


            db.connection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["add"].Index)
            {
                dt.Rows.Add(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells["PerToPublish"].Value.ToString(), dataGridView1.CurrentRow.Cells["Price"].Value.ToString());
              //  dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            db.connection.Open();
            SqlCommand cmd = new SqlCommand("insert into Orders (UserID,Date, Amount) values ('"+UserID+"',Getdate(),'"+txtGrandTotal.Text+"')",db.connection);
            cmd.ExecuteNonQuery();


            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                SqlCommand cmd1 = new SqlCommand("insert into Order_details (OrderID,BookID,Price,Qty,PerToPublish) values ('"+txtOid.Text+"','"+dataGridView2.Rows[i].Cells["Book ID"].Value.ToString()+"','"+ dataGridView2.Rows[i].Cells["Price"].Value.ToString() + "','"+ dataGridView2.Rows[i].Cells["Qty"].Value.ToString() + "','"+ dataGridView2.Rows[i].Cells["Percentage"].Value.ToString() + "')", db.connection);
                cmd1.ExecuteNonQuery();

            }

            db.connection.Close();
            
            MessageBox.Show("Invoice Saved Successfully!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);




            dt.Rows.Clear();
            FetchBooks();
            FetchID();
        }
    }
}
