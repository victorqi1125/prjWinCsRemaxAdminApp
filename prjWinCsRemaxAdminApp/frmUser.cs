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
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }
        DataTable tabHouse,tabEmployee;

        private void frmUser_Load(object sender, EventArgs e)
        {
            tabHouse = clsGlobal.mySet.Tables["House"];
            tabEmployee = clsGlobal.mySet.Tables["Employee"];
            dataGridView1.DataSource = tabHouse;
            cbUser.Items.Add("AllHouse");
            cbUser.Items.Add("OneHouse");
            cbUser.Items.Add("AllEmployee");
            cbUser.Items.Add("OneEmployee");

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNo.Text = "";
            dataGridView1.ClearSelection();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(cbUser.SelectedItem.ToString()=="AllHouse")
            {
                dataGridView1.DataSource = tabHouse;
            }
            else if(cbUser.SelectedItem.ToString() == "OneHouse" && txtNo.Text != null)
            {
                var HouseSelected = from oneH in tabHouse.AsEnumerable()
                                    where oneH.Field<int>("ReferenceNo") == Convert.
                                    ToInt32(txtNo.Text)
                                    select new { ReferenceNo = oneH.Field<int>("ReferenceNo"), 
                                    Address=oneH.Field<string>("Address"),
                                    Area=oneH.Field<string>("Area"),
                                    //YearOfBuilt=Convert.ToDateTime(oneH.Field<DateTime>("YearofBuilt")),
                                    Price = oneH.Field<decimal>("Price"),
                                    YearlyTax = oneH.Field<decimal>("YearlyTax"),
                                    Details = oneH.Field<string>("Details"),
                                    AgentNo=oneH.Field<int>("AgentNo")

                                    };
                dataGridView1.DataSource = HouseSelected.ToList();
            }
            else if(cbUser.SelectedItem.ToString() == "AllEmployee")
            {
                var AllEmployee = from emp in tabEmployee.AsEnumerable()
                            where emp.Field<string>("Type").Equals("Agent")
                            orderby emp.Field<int>("EmployeeNo") ascending
                            select new
                            {
                                EmployeeNo = emp.Field<int>("EmployeeNO"),
                                Name = emp.Field<string>("Name"),
                                Gender = emp.Field<string>("Gender"),
                                Email = emp.Field<string>("Email"),
                                PhoneNo = emp.Field<string>("PhoneNo")
                            };
                dataGridView1.DataSource = AllEmployee.ToList();
            }
            else if (cbUser.SelectedItem.ToString() == "OneEmployee"&&txtNo.Text!=null)
            {
                var EmployeeSelected = from oneEmp in tabEmployee.AsEnumerable()
                                       where oneEmp.Field<int>("EmployeeNo") == Convert.
                                       ToInt32(txtNo.Text)
                                       select new
                                       {
                                           EmployeeNo = oneEmp.Field<int>("EmployeeNO"),
                                           Name = oneEmp.Field<string>("Name"),
                                           Gender = oneEmp.Field<string>("Gender"),
                                           Email = oneEmp.Field<string>("Email"),
                                           PhoneNo = oneEmp.Field<string>("PhoneNo")

                                       };
                dataGridView1.DataSource = EmployeeSelected.ToList();
            }
        }
    }
}
