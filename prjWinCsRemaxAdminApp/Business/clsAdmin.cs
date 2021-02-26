using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjWinCsRemaxAdminApp.Business
{
    public class clsAdmin
    {
        private int adminNo;
        private int empNo;
        private clsPerson person;
        private DateTime birthdate;
        private DateTime enrolldate;

        public clsAdmin(int xadminNo, int xempNo, string xname, string xgender, string xemail, string xphoneNo, System.DateTime xbirthdate, System.DateTime xenrolldate)
        {
            adminNo = xadminNo;
            empNo = xempNo;
            person = new clsPerson(xname, xgender, xemail, xphoneNo);
            birthdate = xbirthdate;
            enrolldate = xenrolldate;

        }

        public clsAdmin()
        {
            adminNo = empNo = 0;
            person = new clsPerson();
            birthdate = enrolldate = new DateTime();
        }

        public int AdminNo
        {
            get => adminNo;
            set
            {
                adminNo = value;
            }
        }

        public int EmpNo
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

        public System.DateTime Birthdate
        {
            get => birthdate;
            set
            {
                birthdate = value;
            }
        }

        public System.DateTime Enrolldate
        {
            get => enrolldate;
            set
            {
                enrolldate = value;
            }
        }

        public string Display()
        {
            string info = "\n-------Admin-------\nAdmin Number: " + adminNo + "\nEmployee Number: " +
                empNo + "\nPersonal Info: " + person + "\nBirthdate: " + birthdate + "\nEnrollment Date: " +
                enrolldate+"\n";
            return info;
        }

        public void Create(int xadminNo,int xempNo,clsPerson xperson,DateTime xbirthdate,DateTime xenrolldate)
        {
            adminNo = xadminNo;
            empNo = xempNo;
            person = xperson;
            birthdate = xbirthdate;
            enrolldate = xenrolldate;

        }
    }
}
