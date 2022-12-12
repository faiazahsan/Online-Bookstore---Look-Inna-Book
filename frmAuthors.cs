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
    public partial class frmAuthors : Form
    {
        public frmAuthors()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += DataGridView1_DoubleClick;
        }

        string status;
        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            txtCid2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtContact.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtBankAccount.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            status = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            if (status == "Active")
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }
        }

        public static string role = "";
        private void frmAuthors_Load(object sender, EventArgs e)
        {
            FetchID();
            FetchBooks();

            if (role != "Admin")
            {
                panel3.Enabled = false;
            }
            else
            {

            }
        }


        public void FetchID()
        {

            //////////////////////////////////                                      FETCH ID

            db.connection.Open();

            SqlDataAdapter da = new SqlDataAdapter("select count(AuthorID)+1 from Authors", db.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txtCid.Text = dt.Rows[0][0].ToString();

            db.connection.Close();

        }


        void FetchBooks()
        {
            db.connection.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from Authors", db.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;


            db.connection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            db.connection.Open();
            SqlCommand cmd = new SqlCommand("insert into Authors(AuthorName,Address,Email,Phone,BankAccount,Status) values (N'"+txtName.Text+"',N'"+txtAddress.Text+"','"+txtEmail.Text+"','"+txtContact.Text+"','"+txtBankAccount.Text+"','Active')", db.connection);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Author Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            db.connection.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Format("[AuthorName] like '%" + txtSearch.Text.Replace("'", "''") + "%' ");

        }
    }
}
