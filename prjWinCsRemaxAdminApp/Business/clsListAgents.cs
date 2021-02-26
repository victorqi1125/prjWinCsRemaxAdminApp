using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjWinCsRemaxAdminApp.Business
{
    public class clsListAgents
    {
        private Dictionary<string, clsAgents> myList;

        public int Quantity
        {
            get => myList.Count;
        }

        public Dictionary<string, clsAgents>.ValueCollection Elements
        {
            get => myList.Values;
        }

        public bool Add(clsAgents client)
        {
            if (Exist(client.EmpNo) == false)
            {
                myList.Add(client.EmpNo, client);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(string empNo)
        {
            return myList.Remove(empNo);
        }

        public clsAgents Find(string empNo)
        {
            if (Exist(empNo) == true)
            {
                return myList[empNo];
            }
            else
            {
                return null;
            }
        }

        public bool Exist(string empNo)
        {
            return myList.ContainsKey(empNo);

        }

        public string Display()
        {
            string info = "";
            foreach (clsAgents item in myList.Values)
            {
                info += item.Display() + "\n";
            }
            return info;
        }
    }
}
