using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Codu.Services.Classes;
using Codu.Services.Model;

namespace Codu.Services.Helper
{
    public class LoanHelper
    {
        public LoanHelper() { }

        public LoanFacts getLoanFacts(Loan loan)
        {
            LoanFacts loanFacts = new LoanFacts();

            //The interest for the loan is calculated by I = P*N*R
            //where P is the principal amount,
            //N is the number of years and
            //R is the rate of interest.
            //The total amount to repay will be A = P + I The amount should be paid back monthly in the form of EMIs.

            loanFacts.TotalInterest = loan.Principle.Value * loan.LoanTermYears.Value * (loan.LoanRate.Value / 100);
            loanFacts.TotalAmountToRepay = loan.Principle.Value + loanFacts.TotalInterest;
            loanFacts.MonthlyEMIAmount = Math.Ceiling(loanFacts.TotalAmountToRepay / (12 * loan.LoanTermYears.Value));
            loanFacts.InitialNumberOfPayments = (12 * loan.LoanTermYears.Value);

            return loanFacts;
        }

        public LoanStatus getLoanStatus(LoanFacts loanFacts, List<Payment> payments, int EMI_No)
        {
            LoanStatus loanStatus = new LoanStatus();

            var totalEMIPaid = loanFacts.MonthlyEMIAmount * EMI_No;
            decimal? totalExtraPaid = 0m;
            loanStatus.TotalPaid = 0m;

            if (payments != null)
            {
                totalExtraPaid = payments.Sum(x => x.Amount);
                if (totalExtraPaid.HasValue)
                {
                    loanStatus.TotalPaid = totalEMIPaid + totalExtraPaid.Value;

                    // recalculate EMIs remaining
                    var loanOutstanding = loanFacts.TotalAmountToRepay - loanStatus.TotalPaid;
                    loanStatus.RemainingEMIs = Math.Ceiling(loanOutstanding / loanFacts.MonthlyEMIAmount);
                }
            }
            else
            {
                loanStatus.TotalPaid = totalEMIPaid;
                loanStatus.RemainingEMIs = loanFacts.InitialNumberOfPayments - EMI_No;
            }
            return loanStatus;
        }


    }
}
