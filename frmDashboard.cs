using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InnaBookStore
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Books f = new Books();
            f.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsers f = new FrmUsers();
            f.ShowDialog();
        }

        private void saleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void authorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAuthors f = new frmAuthors();
            f.ShowDialog();
        }

        private void viewSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmViewOrders f = new FrmViewOrders();
            f.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void newSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrders f = new frmOrders();
            f.ShowDialog();
        }

        public static string Role = "";
        private void frmDashboard_Load(object sender, EventArgs e)
        {
            if (Role != "Admin")
            {
                authorsToolStripMenuItem.Visible = false;
                usersToolStripMenuItem.Visible = false;
                booksToolStripMenuItem.Visible = false;
            }
            else
            {
                booksToolStripMenuItem.Visible = true;
                usersToolStripMenuItem.Visible = true;
                authorsToolStripMenuItem.Visible = true;
            }
        }
    }
}
