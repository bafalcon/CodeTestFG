using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codu.Services.Classes
{
    public class LoanStatus
    {
        public decimal TotalPaid { get; set; }
        public decimal? RemainingEMIs { get; set; }
    }
}
