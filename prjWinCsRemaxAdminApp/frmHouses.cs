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
    public partial class frmHouses : Form
    {
        public frmHouses()
        {
            InitializeComponent();
        }
        DataTable tabHouse, tabClient,tabAgent;
        int currentIndex;
        string mode;
        private void frmHouses_Load(object sender, EventArgs e)
        {
            tabHouse = clsGlobal.mySet.Tables["House"];

            tabClient = clsGlobal.mySet.Tables["Client"];

            tabAgent = clsGlobal.mySet.Tables["Employee"];
            
            foreach(DataRow row in tabAgent.Rows)
            {
                if(row["Type"].ToString() == "Agent")
                {
                    cboAgent.Items.Add(row["Name"].ToString());
                }
            }

            currentIndex = 0;
            Display();
            ActivateButtons(true, true, true, false, false, true, true);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            Display();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                Display();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < tabHouse.Rows.Count - 1)
            {
                currentIndex++;
                Display();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = tabHouse.Rows.Count - 1;
            Display();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = "add";
            txtAddress.Text = txtArea.Text = txtYearofBuilt.Text = txtPrice.Text
                =txtTax.Text=txtDetails.Text= "";
            dtpListed.ResetText();
            txtAddress.Focus();
            lblInfo.Text = "-----Adding Mode-----";
            ActivateButtons(false, false, false, true, true, false, false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            mode = "edit";


            txtAddress.Focus();
            lblInfo.Text = "-----Editing Mode-----";
            ActivateButtons(false, false, false, true, true, false, false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Property Info?", "Property Deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //Delete the record(or datarow) positioned at the currentindex
                tabHouse.Rows[currentIndex].Delete();

                //Save (or synchronize) the contents of the mySet.tables["House"] to the database
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpHouse);
                clsGlobal.adpHouse.Update(clsGlobal.mySet, "House");
                //Refrresh the datatable with the content of the database in the case of changes
                clsGlobal.mySet.Tables.Remove("House");
                clsGlobal.adpHouse.Fill(clsGlobal.mySet, "House");

                tabHouse = clsGlobal.mySet.Tables["House"];

                currentIndex = 0;
                Display();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow currentRow;
            if (mode == "add")
            {
                if (clsGlobal.mySet.Tables["House"].Columns["Address"].ColumnName.Contains(txtAddress.Text) != true)
                {
                    currentRow = tabHouse.NewRow();
                    currentRow["Address"] = txtAddress.Text;
                    currentRow["Area"] = txtArea.Text;
                    currentRow["YearofBuilt"] = txtYearofBuilt.Text;
                    currentRow["Price"] = Convert.ToDecimal(txtPrice.Text);
                    currentRow["YearlyTax"] = Convert.ToDecimal(txtTax.Text);
                    currentRow["Details"] = txtDetails.Text;
                    currentRow["DataListed"] = dtpListed.Value;
                    //For the ReferHou, we need to search in the tabCompany
                    //the RefComapny of the one that have the same name than the 
                    //selected name in the combobox
                    foreach (DataRow myrow in tabAgent.Rows)
                    {
                        if (myrow["Name"].ToString() == cboAgent.SelectedItem.ToString())
                        {
                            currentRow["AgentNo"] = myrow["EmployeeNo"];
                        }
                    }

                    tabHouse.Rows.Add(currentRow);
                    //Save (or synchronize) the contents of the mySet.tables["House"] to the database
                    SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpHouse);
                    clsGlobal.adpHouse.Update(clsGlobal.mySet, "House");
                    //Refresh the datatable with the content of the database in the case of changes
                    clsGlobal.mySet.Tables.Remove("House");
                    clsGlobal.adpHouse.Fill(clsGlobal.mySet, "House");

                    tabHouse = clsGlobal.mySet.Tables["House"];

                    currentIndex = tabHouse.Rows.Count - 1;
                }
                else
                {
                    MessageBox.Show("This House already exists", "Adding Failed");
                    txtAddress.Focus();
                    return;
                }
            }
            else if (mode == "edit")
            {
                currentRow = tabHouse.Rows[currentIndex];
                currentRow["Address"] = txtAddress.Text;
                currentRow["Area"] = txtArea.Text;
                currentRow["YearofBuilt"] = txtYearofBuilt.Text;
                currentRow["Price"] = Convert.ToDecimal(txtPrice.Text);
                currentRow["YearlyTax"] = Convert.ToDecimal(txtTax.Text);
                currentRow["Details"] = txtDetails.Text;
                currentRow["DataListed"] = dtpListed.Value;
                //For the ReferCompany, we need to search in the tabCompany
                //the RefComapny of the one that have the same name than the 
                //selected name in the combobox
                foreach (DataRow myrow in tabAgent.Rows)
                {
                    if (myrow["Name"].ToString() == cboAgent.SelectedItem.ToString())
                    {
                        currentRow["AgentNo"] = myrow["EmployeeNo"];
                    }
                }

                tabHouse.Rows.Add(currentRow);
                //Save (or synchronize) the contents of the mySet.tables["Companies"] to the database
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpHouse);
                clsGlobal.adpHouse.Update(clsGlobal.mySet, "House");
                //Refresh the datatable with the content of the database in the case of changes
                clsGlobal.mySet.Tables.Remove("House");
                clsGlobal.adpHouse.Fill(clsGlobal.mySet, "House");

                tabHouse = clsGlobal.mySet.Tables["House"];

            }
            Display();
            ActivateButtons(true, true, true, false, false, true, true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Display();
            ActivateButtons(true, true, true, false, false, true, true);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActivateButtons(bool Add, bool Edit, bool Del, bool Save, bool Cancel, bool Navigates, bool Close )
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
        private void Display()
        {
            //try it once but cant fix it
            //var houses = from hs in tabHouse.AsEnumerable()
            //             select hs[currentindex];
            //txtAddress.Text=houses.Select(house"Address")

            txtAddress.Text = tabHouse.Rows[currentIndex]["Address"].ToString();
            txtArea.Text = tabHouse.Rows[currentIndex]["Area"].ToString();
            txtYearofBuilt.Text = tabHouse.Rows[currentIndex]["YearofBuilt"].ToString();
            txtPrice.Text = tabHouse.Rows[currentIndex]["Price"].ToString();
            txtTax.Text = tabHouse.Rows[currentIndex]["YearlyTax"].ToString();
            txtDetails.Text = tabHouse.Rows[currentIndex]["Details"].ToString();
            dtpListed.Value = Convert.ToDateTime(tabHouse.Rows[currentIndex]["DataListed"].ToString());

            //foreach (DataRow myRow in tabAgent.Rows)
            //{
            //    if (myRow["EmployeeNo"] == tabHouse.Rows[currentIndex]["AgentNo"]
            //        && myRow["Type"].ToString()=="Agent")
            //    {
            //        cboAgent.Text = myRow["Name"].ToString();
            //    }
            //}
            DataColumn[] keys = new DataColumn[1];
            keys[0] = tabAgent.Columns["EmployeeNo"];
            tabAgent.PrimaryKey = keys;

            DataRow myrow = tabAgent.Rows.Find(tabHouse.Rows[currentIndex]["AgentNo"]);
            if (myrow["Type"].ToString() == "Agent")
            {
                cboAgent.Text = myrow["Name"].ToString();
            }

            //IEnumerable<string> selectagents = (from ag in tabAgent.AsEnumerable()
            //                    from em in tabHouse.AsEnumerable()
            //                    where ag.Field<int>("EmployeeNo") == em.Field<int>("AgentNo")
            //                    orderby ag.Field<int>("EmployeeNo") ascending
            //                    select ag.Field<string>("Name")).Distinct();
            //DataTable tabSelectAg = selectagents.CopyToDataTable();
            lblInfo.Text = "Houses No. " + (currentIndex + 1) + " on a Total of " + tabHouse.Rows.Count;
        }
    }
}
