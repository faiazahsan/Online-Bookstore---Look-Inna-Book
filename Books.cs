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
using System.Collections;

namespace InnaBookStore
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            db.connection.Open();

            SqlCommand cmd = new SqlCommand("insert into books (BookName, IsbnNo, AuthorID, Gnre, Pages, Price, PerToPublish) values (N'" + txtName.Text + "','" + txtISBN.Text + "','" + comboBox1.SelectedIndex + 1+"','"+txtGenre.Text+"','"+txtNoOfpages.Text+"','"+txtPrice.Text+"','"+txtPercentage.Text+"')",db.connection);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Book Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            db.connection.Close();
            FetchBooks();
        }

        public static string Role = "";
        private void Books_Load(object sender, EventArgs e)
        {
            FetchID();
            FetchAuthors();
            FetchBooks();

            if (Role != "Admin")
            {
                panel3.Enabled = false;
            }
            else
            {

            }
        }

        ArrayList list = new ArrayList();
        void FetchAuthors()
        {
            db.connection.Open();


            SqlCommand cmd = new SqlCommand("select AuthorID, AuthorName from Authors",db.connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader[0].ToString());
                comboBox1.Items.Add(reader[1].ToString());
            }


            db.connection.Close();

        }




        void FetchAuthor()
        {
            //SqlCommand cmd = new SqlCommand("select AuthorID, AuthorName from Authors", db.connection);
            SqlDataAdapter da = new SqlDataAdapter("select AuthorID, AuthorName from Authors", db.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            //comboBox1.data
        }



        public void FetchID()
        {

            //////////////////////////////////                                      FETCH ID

            db.connection.Open();

            SqlDataAdapter da = new SqlDataAdapter("select count(OrderID)+1 from orders", db.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txtCid.Text = dt.Rows[0][0].ToString();

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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            txtCid2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtISBN.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtGenre.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtNoOfpages.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtPercentage.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Format("[BookName] like '%" + txtSearch.Text.Replace("'", "''") + "%' ");
        }
    }
}
