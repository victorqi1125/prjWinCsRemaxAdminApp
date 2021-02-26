using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjWinCsRemaxAdminApp.Business
{
    public class clsListHouses
    {
        private Dictionary<string, clsHouses> myList;

        public clsListHouses()
        {
            myList = new Dictionary<string, clsHouses>();

        }
        public int Quantity
        {
            get => myList.Count;
        }
        public Dictionary<string, clsHouses>.ValueCollection Elements
        {
            get => myList.Values;
        }
        public bool Add(clsHouses house)
        {
            if(Exist(house.RefNo)==false)
            {
                myList.Add(house.RefNo, house);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete(string refNo)
        {
            return myList.Remove(refNo);
        }
        public clsHouses Find(string refNo)
        {
            if(Exist(refNo)==true)
            {
                return myList[refNo];
            }
            else
            {
                return null;
            }
        }
        public bool Exist(string refNo)
        {
            return myList.ContainsKey(refNo);
        }
        public string Display()
        {
            string info = "";
            foreach(clsHouses i in myList.Values)
            {
                info += i.Display() + "\n";
            }
            return info;
        }

    }
}
