using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace prjWinCsRemaxAdminApp.Business
{
    public class clsHouses
    {
        private string refNo;
        private string address;
        private decimal area;
        private string builtYear;
        private decimal price;
        private decimal yearlyTax;
        private string detail;
        private DateTime dateAnounced;
        private clsAgents agent;
        private clsClients seller;

        public clsHouses(string xrefNo, string xaddress, decimal xarea, string xbuiltYear, decimal xyearlyTax, decimal xprice, string xdetail, clsAgents xagent, clsClients xseller, System.DateTime xdateAnounced)
        {
            refNo = xrefNo;
            address = xaddress;
            area = xarea;
            builtYear = xbuiltYear;
            price = xprice;
            yearlyTax = xyearlyTax;
            detail = xdetail;
            dateAnounced = xdateAnounced;
            agent = xagent;
            seller = xseller;
        }

        public clsHouses()
        {

            refNo=address = builtYear = detail = "Not Defined";
            area = price = yearlyTax = 0;
        }

        public string RefNo
        {
            get => refNo;
            set
            {
                refNo = value;
            }
        }

        public string Address
        {
            get => address;
            set
            {
                address = value;
            }
        }

        public decimal Area
        {
            get => area;
            set
            {
                area=value;
            }
        }

        public string BuiltYear
        {
            get => builtYear;
            set
            {
                builtYear = value;
            }
        }

        public decimal Price
        {
            get => price;
            set
            {
                price = value;
            }
        }

        public decimal YearlyTax
        {
            get => yearlyTax;
            set
            {
                yearlyTax = value;
            }
        }

        public string Detail
        {
            get => detail;
            set
            {
                detail = value;
            }
        }

        public void Post(string xrefNo, string xaddress, decimal xarea, string xbuiltYear, decimal xyearlyTax, decimal xprice, string xdetail, clsAgents xagent, clsClients xseller)
        {
            refNo = xrefNo;
            address = xaddress;
            area = xarea;
            builtYear = xbuiltYear;
            price = xprice;
            yearlyTax = xyearlyTax;
            detail = xdetail;
            dateAnounced = DateTime.Today;
            agent = xagent;
            seller = xseller;
        }

        public string Display()
        {
            string info = "\n-------------House-------------\nReference Number: " + refNo + "\nAddress: " +
                address + "\nArea: " + area + "\nYear Built: " + builtYear + "\nPrice: " + price + "\nYearly Tax: " +
                yearlyTax + "\nMore Details: " + detail + "\nDateAnounced: " + dateAnounced +
                "\nReal Estate Agent: " + agent + "\nSeller of the House: " + seller;
            return info;
        }
    }
}
