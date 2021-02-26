using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjWinCsRemaxAdminApp.Business
{
    public class clsAgents
    {
        private string empNo;
        private clsPerson person;
        private decimal salary;
        private DateTime birthdate;
        private DateTime enrolldate;
        private clsAdmin admin;
        private clsListHouses houses;

        public clsAgents()
        {
            empNo = "Not Defined";
            person = new clsPerson();
            salary = 0;
            birthdate = enrolldate = new DateTime();
            admin = new clsAdmin();
            houses = new clsListHouses();
        }

        public clsAgents(string xempNo, string xname, string xgender, 
            string xemail, string xphoneNo, decimal xsalary, 
            System.DateTime xbirthdate, System.DateTime xenrolldate, clsAdmin xadmin,clsListHouses xhouses)
        {
            EmpNo = xempNo;
            Person = new clsPerson(xname, xgender, xemail, xphoneNo);
            Salary = xsalary;
            birthdate = xbirthdate;
            enrolldate = xenrolldate;
            admin = xadmin;
            houses = xhouses;
        }

        public string EmpNo
        {
            get => empNo;
            set
            {
                empNo = value;

            }
        }

        public clsPerson Person
        {
            get => person;
            set
            {
                person = value;
            }
        }

        public decimal Salary
        {
            get =>salary;
            set
            {
                salary = value;
            }
        }

        public clsAdmin Admin
        {
            get => admin;
            set
            {
                admin = value;
            }
        }

        public string Display()
        {
            string info = "\n---------Agent Info----------\nEmployee Number: " + empNo +
                "\n Personal Info: " + person.Display() + "\nSalary: " + salary + "\nBirthdate: " + birthdate +
                "\nEnrollDate: " + enrolldate + "\nAdministrator: " + admin + "\nHouse on hand for sale: " +
                houses;
            return info;
        }

        public void Create(string xempNo, string xname, string xgender, string xemail, 
            string xphoneNo, decimal xsalary, System.DateTime xbirthdate, 
            System.DateTime xenrolldate, clsAdmin xadmin,clsListHouses xhouses)
        {
            EmpNo = xempNo;
            Person = new clsPerson(xname, xgender, xemail, xphoneNo);
            Salary = xsalary;
            birthdate = xbirthdate;
            enrolldate = xenrolldate;
            admin = xadmin;
            houses = xhouses;

        }
    }
}
