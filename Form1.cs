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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtPass.KeyDown += TxtPass_KeyDown;
        }

        private void TxtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                db.connection.Open();

                SqlCommand cmd = new SqlCommand("select * from Users where role = '"+comboBox1.Text+"' and UserName = '"+txtUser.Text+"' and Password = '"+txtPass.Text+"' and Status = 'Active'",db.connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    frmOrders.UserID = FrmViewOrders.UserID = reader[0].ToString();
                    frmOrders.UserName = reader["Name"].ToString();
                    FrmViewOrders.Role = frmAuthors.role = Books.Role = FrmUsers.Role = frmDashboard.Role = reader["Role"].ToString();
                    frmDashboard f = new frmDashboard();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username or Password is Incorrect");
                }

                db.connection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.connection.Open();

            SqlCommand cmd = new SqlCommand("select * from Users where role = '" + comboBox1.Text + "' and UserName = '" + txtUser.Text + "' and Password = '" + txtPass.Text + "' and Status = 'Active'", db.connection);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                frmOrders.UserID = FrmViewOrders.UserID = reader[0].ToString();
                frmOrders.UserName = reader["Name"].ToString();
                FrmViewOrders.Role = frmAuthors.role = Books.Role = FrmUsers.Role = frmDashboard.Role = reader["Role"].ToString();
                frmDashboard f = new frmDashboard();
                f.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Username or Password is Incorrect");
            }

            db.connection.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
          txtPass.PasswordChar = checkBox1.Checked ? '\0' : '*';



        }
    }
}
