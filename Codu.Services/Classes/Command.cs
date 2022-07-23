using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codu.Services.Classes
{
    public class Command
    {
        public Enums.CommandType Type { get; set; }
        public string BankName { get; set; }
        public string BorrowerName { get; set; }
        public decimal? Principle { get; set; }
        public decimal? LoanRate { get; set; }
        public decimal? LoanTermYears { get; set; }
        public decimal? Payment { get; set; }
        public int? EMI_NO { get; set; }

        public bool isValidLoan()
        {
            var output = LoanRate != null && LoanTermYears != null && Principle != null;

            output = output && (!string.IsNullOrEmpty(BankName) && !string.IsNullOrEmpty(BorrowerName));
            return output;
        }

        public bool isValidPayment()
        {
            var output = Payment != null && EMI_NO != null;

            output = output && (!string.IsNullOrEmpty(BankName) && !string.IsNullOrEmpty(BorrowerName));
            return output;
        }

        public bool isValidBalance()
        {
            var output = EMI_NO != null;

            output = output && (!string.IsNullOrEmpty(BankName) && !string.IsNullOrEmpty(BorrowerName));
            return output;
        }

    }
}
