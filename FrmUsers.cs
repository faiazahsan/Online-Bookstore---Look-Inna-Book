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
    public partial class FrmUsers : Form
    {
        public FrmUsers()
        {
            InitializeComponent();
        }

        public static string Role = "";
        private void FrmUsers_Load(object sender, EventArgs e)
        {
            FetchID();
            FetchUsers();

            if (Role != "Admin")
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

            SqlDataAdapter da = new SqlDataAdapter("select count(userID)+1 from users", db.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txtCid.Text = dt.Rows[0][0].ToString();

            db.connection.Close();
            
        }

        void FetchUsers()
        {
            db.connection.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from Users", db.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;


            db.connection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            db.connection.Open();

            SqlCommand cmd = new SqlCommand("insert into users (name, Contact, Role, UserName, Password, Status) values (N'"+txtName.Text+"','"+txtContact.Text+"','"+comboBox1.Text+"',N'"+txtUsername.Text+"',N'"+txtpass.Text+"','Active')", db.connection);
            cmd.ExecuteNonQuery();

            MessageBox.Show("User Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            db.connection.Close();

            FetchUsers();
        }


        string status = "";
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
          
            txtCid.Text= txtCid2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtContact.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtUsername.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtpass.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            status = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            
            if (status.Trim() == "Active")
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(db.connection.State == ConnectionState.Open)
            {
                db.connection.Close();
            }

            db.connection.Open();
            if (checkBox2.Checked == true)
            {
                SqlCommand cmd = new SqlCommand("Update  users set status =  'Active' where userid = " + txtCid2.Text + "", db.connection);

                cmd.ExecuteNonQuery();

            }
            else if (checkBox2.Checked == false)
            {
                SqlCommand cmd = new SqlCommand("Update  users set status =  'Deactive' where userid = " + txtCid2.Text + "", db.connection);
                cmd.ExecuteNonQuery();


            }

            db.connection.Close();

            FetchUsers();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


}
