using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Models
{
    public class CreditData
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Address { get; set; }
        public int Assessed_income { get; set; }
        public int Balance_of_debt { get; set; }
        public bool Complaints { get; set; }

    }
}
