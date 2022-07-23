using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codu.Services.Model
{
    public class Loan
    {
        public string BankName { get; set; }
        public string BorrowerName { get; set; }
        public decimal? Principle { get; set; }
        public decimal? LoanRate { get; set; }
        public decimal? LoanTermYears { get; set; }
    }

   



}
