using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjWinCsRemaxAdminApp.Business
{
    public class clsClients
    {
        private int clientNo;
        private clsPerson person;
        private string type;
        private string lang;
        private clsAgents agent;

        public clsClients(int xclientNo, string xname, string xgender, string xemail, string xphoneNo,string xtype, string xlang)
        {
            clientNo = xclientNo;
            person = new clsPerson(xname, xgender, xemail, xphoneNo);
            type = xtype;
            lang = xlang;
            agent = new clsAgents();
        }

        public clsClients()
        {
            clientNo = 0;
            person = new clsPerson();
            type = lang = "Not Defined";
            agent = new clsAgents();
        }

        public clsClients(int xclientNo, string xname, string xgender, string xemail, string xphoneNo,string xtype, string xlang, clsAgents xagent)
        {
            clientNo = xclientNo;
            person = new clsPerson(xname, xgender, xemail, xphoneNo);
            type = xtype;
            lang = xlang;
            agent = xagent;
        }

        public int ClientNo
        {
            get => clientNo;
            set
            {
                clientNo = value;
            }
        }

        public string Lang
        {
            get => lang;
            set
            {
                lang = value;
            }
        }
        public string Type
        {
            get => type;
            set => type = value;
        }
        public clsAgents Agent
        {
            get => agent;
            set
            {
                agent = value;
            }
        }

        public string Display()
        {
            string info = "\n-------Client---------\n Type: " + type + "\nClientNumber: " +
                clientNo + "\nPersonal Info: " + person + "\nLanguage Speaking: " +
                lang + "\nAgent: " + agent;
            return info;
        }

        public void CreateBuyer(int xclientNo, string xname, string xgender, string xemail, string xphoneNO, string type, string xlang, clsAgents xagent)
        {
            clientNo = xclientNo;
            person = new clsPerson(xname, xgender, xemail, xphoneNO);
            type = "Buyer";
            agent = xagent;
        }
        public void CreateSeller(int xclientNo, string xname, string xgender, string xemail, string xphoneNO, string type, string xlang, clsAgents xagent)
        {
            clientNo = xclientNo;
            person = new clsPerson(xname, xgender, xemail, xphoneNO);
            type = "Seller";
            agent = xagent;
        }
    }
}
