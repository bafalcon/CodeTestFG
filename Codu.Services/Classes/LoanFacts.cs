using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codu.Services.Classes
{
    public class LoanFacts
    {
        public decimal TotalInterest { get; set; }
        public decimal TotalAmountToRepay { get; set; }
        public decimal MonthlyEMIAmount { get; set; }
        public decimal InitialNumberOfPayments { get; set; }
    }
}
