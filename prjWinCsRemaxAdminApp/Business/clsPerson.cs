using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace prjWinCsRemaxAdminApp.Business
{
    public class clsPerson
    {
        private string name;
        private string gender;
        private string email;
        private string phoneNo;

        public clsPerson(string xname, string xgender, string xemail, string xphoneNo)
        {
            Name = xname;
            Gender = xgender;
            Email = xemail;
            PhoneNo = xphoneNo;
        }

        public clsPerson()
        {
            name = gender = email = phoneNo = "Not Defined";
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
            }
        }

        public string PhoneNo
        {
            get => phoneNo;
            set
            {
                phoneNo = value;
            }
        }

        public string Display()
        {
            string info = "\n-------------\n Name: " + name + "\nGender: " + gender + "\nEmail: " +
                "\nPhoneNumber: " + phoneNo;
            return info;
        }
    }
}
