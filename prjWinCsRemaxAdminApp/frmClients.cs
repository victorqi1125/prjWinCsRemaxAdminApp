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
using prjWinCsRemaxAdminApp.Datasource;

namespace prjWinCsRemaxAdminApp
{
    public partial class frmClients : Form
    {
        public frmClients()
        {
            InitializeComponent();
        }
        DataTable tabClient;
        int currentIndex;
        string mode;
        private void frmClients_Load(object sender, EventArgs e)
        {
            tabClient = clsGlobal.mySet.Tables["Client"];

            currentIndex = 0;

            DisplayDatarow2Txt();

            ActivateButtons(true, true, true, false, false, true, true);

        }

        private void DisplayDatarow2Txt()
        {
            txtName.Text = tabClient.Rows[currentIndex]["Name"].ToString();
            txtGender.Text= tabClient.Rows[currentIndex]["Gender"].ToString();
            txtEmail.Text= tabClient.Rows[currentIndex]["Email"].ToString();
            txtPhoneNo.Text= tabClient.Rows[currentIndex]["PhoneNo"].ToString();
            txtLanguage.Text= tabClient.Rows[currentIndex]["Language"].ToString();
            lblInfo.Text = "Client No.:" + (currentIndex + 1) + " on a total of " 
                + tabClient.Rows.Count;


        }
        private void ActivateButtons(bool Add, bool Edit, bool Del, bool Save, bool Cancel, bool Navigates, bool Close)
        {
            btnAdd.Enabled = Add;
            btnEdit.Enabled = Edit;
            btnDelete.Enabled = Del;
            btnSave.Enabled = Save;
            btnCancel.Enabled = Cancel;
            btnFirst.Enabled = btnPrevious.Enabled = btnNext.Enabled
                = btnLast.Enabled = Navigates;
            btnExit.Enabled = Close;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            DisplayDatarow2Txt();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayDatarow2Txt();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < tabClient.Rows.Count - 1)
            {
                currentIndex++;
                DisplayDatarow2Txt();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = tabClient.Rows.Count - 1;
            DisplayDatarow2Txt();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = "add";
            txtName.Text = txtGender.Text = txtEmail.Text = txtPhoneNo.Text =txtLanguage.Text= "";
            
            txtName.Focus();
            lblInfo.Text = "-----ADDING MODE-----";
            ActivateButtons(false, false, false, true, true, false, false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            mode = "edit";
            txtName.Enabled = false;
            txtGender.Focus();
            lblInfo.Text = "-----EDITING MODE-----";
            //activate buttons
            ActivateButtons(false, false, false, true, true, false, false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Client Info?", "Client Deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //Delete the record(or datarow) positioned at the currentindex
                tabClient.Rows[currentIndex].Delete();

                //Save (or synchronize) the contents of the mySet.tables["Client"] to the database
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpClient);
                clsGlobal.adpClient.Update(clsGlobal.mySet, "Client");
                //Refrresh the datatable with the content of the database in the case of changes
                clsGlobal.mySet.Tables.Remove("Client");
                clsGlobal.adpClient.Fill(clsGlobal.mySet, "Client");


                currentIndex = 0;
                DisplayDatarow2Txt();

            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow currentRow;
            if (mode == "add")
            {
                if (clsGlobal.mySet.Tables["Client"].Columns["Name"].ColumnName.Contains(txtName.Text) != true)
                {
                    currentRow = tabClient.NewRow();
                    // affect the contents of textboxes in the current row
                    currentRow["Name"] = txtName.Text;
                    currentRow["Gender"] = txtGender.Text;
                    currentRow["Email"] = txtEmail.Text;
                    currentRow["PhoneNo"] = txtPhoneNo.Text;
                    currentRow["Language"] = txtLanguage.Text;
                    //add the current row in the collection tabClient.Rows
                    tabClient.Rows.Add(currentRow);

                    //Save (or synchronize) the contents of the mySet.tables["Client"] to the database
                    SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpClient);
                    clsGlobal.adpClient.Update(clsGlobal.mySet, "Client");
                    //Refrresh the datatable with the content of the database in the case of changes
                    clsGlobal.mySet.Tables.Remove("Client");
                    clsGlobal.adpClient.Fill(clsGlobal.mySet, "Client");

                    currentIndex = tabClient.Rows.Count - 1;
                }
                else
                {
                    MessageBox.Show("This Client Name already exists", "Adding Failed");
                    txtName.Focus();
                    return;
                }

            }
            else if (mode == "edit")
            {
                currentRow = clsGlobal.mySet.Tables["Client"].Rows[currentIndex];
                // affect the contents of textboxes in the current row
                currentRow["Name"] = txtName.Text;
                currentRow["Gender"] = txtGender.Text;
                currentRow["Email"] = txtEmail.Text;
                currentRow["PhoneNo"] = txtPhoneNo.Text;
                currentRow["Language"] = txtLanguage.Text;


                //Save (or synchronize) the contents of the mySet.tables["Client"] to the database
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpClient);
                clsGlobal.adpClient.Update(clsGlobal.mySet, "Client");
                //Refrresh the datatable with the content of the database in the case of changes
                clsGlobal.mySet.Tables.Remove("Client");
                clsGlobal.adpClient.Fill(clsGlobal.mySet, "Client");



            }
            //activate buttons
            ActivateButtons(true, true, true, false, false, true, true);
            DisplayDatarow2Txt();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = txtGender.Text = txtEmail.Text = txtPhoneNo.Text = txtLanguage.Text = "";
            ActivateButtons(true, true, true, false, false, true, true);
            DisplayDatarow2Txt();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
