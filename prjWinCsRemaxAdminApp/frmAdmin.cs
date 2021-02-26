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
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }
        DataTable tabAgent;
        int currentIndex;
        string mode;


        private void frmAdmin_Load(object sender, EventArgs e)
        {
            tabAgent = clsGlobal.mySet.Tables["Employee"];

            var types=tabAgent.AsEnumerable().Select(x => x["Type"].ToString()).Distinct();
            foreach(var item in types)
            {
                cbType.Items.Add(item);
            }
            currentIndex = 0;

            DisplayDatarow2Txt();

            ActivateButtons(true, true, true, false, false, true, true);
        }
        private void DisplayDatarow2Txt()
        {
            txtName.Text = tabAgent.Rows[currentIndex]["Name"].ToString();
            txtGender.Text = tabAgent.Rows[currentIndex]["Gender"].ToString();
            txtEmail.Text = tabAgent.Rows[currentIndex]["Email"].ToString();
            txtPhoneNo.Text = tabAgent.Rows[currentIndex]["PhoneNo"].ToString();
            txtSalary.Text = tabAgent.Rows[currentIndex]["Salary"].ToString();
            dtpBirth.Value = Convert.ToDateTime(tabAgent.Rows[currentIndex]["Birthdate"].ToString());
            dtpEnroll.Value = Convert.ToDateTime(tabAgent.Rows[currentIndex]["Enrolldate"].ToString());
            
            cbType.Text= tabAgent.Rows[currentIndex]["Type"].ToString();

            lblInfo.Text = "Agent No.:" + (currentIndex + 1) + " on a total of "
                + tabAgent.Rows.Count;


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
            if (currentIndex < tabAgent.Rows.Count - 1)
            {
                currentIndex++;
                DisplayDatarow2Txt();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = tabAgent.Rows.Count - 1;
            DisplayDatarow2Txt();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = "add";
            txtName.Text = txtGender.Text = txtEmail.Text = txtPhoneNo.Text
                = txtSalary.Text = "";
            dtpEnroll.ResetText();
            dtpBirth.ResetText();
            cbType.SelectedIndex = 1;
            txtName.Focus();
            lblInfo.Text = "-----Adding Mode-----";
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
            if (MessageBox.Show("Are you sure you want to delete this Agent Info?", "Company Deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //Delete the record(or datarow) positioned at the currentindex
                tabAgent.Rows[currentIndex].Delete();

                //Save (or synchronize) the contents of the mySet.tables["Client"] to the database
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpEmployee);
                clsGlobal.adpEmployee.Update(clsGlobal.mySet, "Employee");
                //Refrresh the datatable with the content of the database in the case of changes
                clsGlobal.mySet.Tables.Remove("Employee");
                clsGlobal.adpEmployee.Fill(clsGlobal.mySet, "Employee");


                currentIndex = 0;
                DisplayDatarow2Txt();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow currentRow;
            if (mode == "add")
            {
                if (clsGlobal.mySet.Tables["Employee"].Columns["Name"].ColumnName.Contains(txtName.Text) != true)
                {
                    currentRow = tabAgent.NewRow();
                    // affect the contents of textboxes in the current row
                    currentRow["Name"] = txtName.Text;
                    currentRow["Gender"] = txtGender.Text;
                    currentRow["Email"] = txtEmail.Text;
                    currentRow["PhoneNo"] = txtPhoneNo.Text;
                    currentRow["Salary"] = Convert.ToDecimal(txtSalary.Text);
                    currentRow["Birthdate"] = dtpBirth.Value;
                    currentRow["Enrolldate"] = dtpEnroll.Value;
                    currentRow["Type"] = cbType.SelectedItem;
                    //add the current row in the collection tabAgent.Rows
                    tabAgent.Rows.Add(currentRow);

                    //Save (or synchronize) the contents of the mySet.tables["Agent"] to the database
                    SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpEmployee);
                    clsGlobal.adpEmployee.Update(clsGlobal.mySet, "Employee");
                    //Refrresh the datatable with the content of the database in the case of changes
                    clsGlobal.mySet.Tables.Remove("Employee");
                    clsGlobal.adpEmployee.Fill(clsGlobal.mySet, "Employee");

                    tabAgent = clsGlobal.mySet.Tables["Employee"];

                    currentIndex = tabAgent.Rows.Count - 1;
                }
                else
                {
                    MessageBox.Show("This Agent Name already exists", "Adding Failed");
                    txtName.Focus();
                    return;
                }

            }
            else if (mode == "edit")
            {
                currentRow = clsGlobal.mySet.Tables["Employee"].Rows[currentIndex];
                // affect the contents of textboxes in the current row
                currentRow["Gender"] = txtGender.Text;
                currentRow["Email"] = txtEmail.Text;
                currentRow["PhoneNo"] = txtPhoneNo.Text;
                currentRow["Salary"] = Convert.ToDecimal(txtSalary.Text);
                currentRow["Birthdate"] = dtpBirth.Value;
                currentRow["Enrolldate"] = dtpEnroll.Value;
                currentRow["Type"] = cbType.SelectedItem;


                //Save (or synchronize) the contents of the mySet.tables["Agent"] to the database
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpEmployee);
                clsGlobal.adpEmployee.Update(clsGlobal.mySet, "Employee");
                //Refrresh the datatable with the content of the database in the case of changes
                clsGlobal.mySet.Tables.Remove("Employee");
                clsGlobal.adpEmployee.Fill(clsGlobal.mySet, "Employee");

                tabAgent = clsGlobal.mySet.Tables["Employee"];

            }
            //activate buttons
            ActivateButtons(true, true, true, false, false, true, true);
            DisplayDatarow2Txt();
            txtName.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = txtGender.Text = txtEmail.Text = txtPhoneNo.Text = txtSalary.Text = "";
            dtpEnroll.ResetText();
            dtpBirth.ResetText();
            cbType.SelectedIndex = 1;
            ActivateButtons(true, true, true, false, false, true, true);
            DisplayDatarow2Txt();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
