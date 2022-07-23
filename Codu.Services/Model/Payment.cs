using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codu.Services.Model
{
    public class Payment
    {
        public string BankName { get; set; }
        public string BorrowerName { get; set; }
        public decimal? Amount { get; set; }
        public int? EMI_NO { get; set; }
    }
}
