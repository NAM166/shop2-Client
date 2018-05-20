using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop2Client
{
    class CustomerClient
    {
        public int CustomerID { get; set; }
        public string CName { get; set; }
        public string CAddress { get; set; }
        public int Phone { get; set; }


        public override string ToString()
        {
            return "Customer ID " + CustomerID + "Customer Name " + CName + "Customer Address" + CAddress + " Phone Number" + Phone;

        }
    }
}
