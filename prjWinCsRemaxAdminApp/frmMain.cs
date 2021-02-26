using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prjWinCsRemaxAdminApp.Datasource;
using System.Data.SqlClient;

namespace prjWinCsRemaxAdminApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string info = "Are you sure you want to quit?";
            string title = "Application Notice";

            if(MessageBox.Show(info,title,MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void mnuAdmin_Click(object sender, EventArgs e)
        {
            frmAdmin myAdmin = new frmAdmin();
            //indicate to that it is the child of the current form frmMain(this)
            myAdmin.MdiParent = this;
            myAdmin.Show();
        }

        private void mnuAgent_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuUser_Click(object sender, EventArgs e)
        {
            frmUser myUser = new frmUser();
            myUser.MdiParent = this;
            myUser.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Create a global dataset
            clsGlobal.mySet = new DataSet();

            //open connection
            clsGlobal.myCon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=prjWinCsRemaxAdminApp;Integrated Security=True");
            clsGlobal.myCon.Open();

            //recuperate and insert the table Houses from database to dataset
            SqlCommand myCmd = new SqlCommand("select * from House", clsGlobal.myCon);
            clsGlobal.adpHouse = new SqlDataAdapter(myCmd);
            clsGlobal.adpHouse.Fill(clsGlobal.mySet, "House");

            //recuperate and insert the table Client from database to dataset
            myCmd = new SqlCommand("select * from Client", clsGlobal.myCon);
            clsGlobal.adpClient = new SqlDataAdapter(myCmd);
            clsGlobal.adpClient.Fill(clsGlobal.mySet, "Client");

            //recuperate and insert the table Employee from database to dataset
            myCmd = new SqlCommand("select * from Employee", clsGlobal.myCon);
            clsGlobal.adpEmployee = new SqlDataAdapter(myCmd);
            clsGlobal.adpEmployee.Fill(clsGlobal.mySet, "Employee");


        }

        private void manageClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClients myClient = new frmClients();
            myClient.MdiParent = this;
            myClient.Show();
        }

        private void manageHousesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHouses myHouse = new frmHouses();
            myHouse.MdiParent = this;
            myHouse.Show();
        }
    }
}
