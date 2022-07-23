using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Codu.Services.Model;
using Codu.Services.Parser;
using Codu.Services.Classes;

namespace Codu.Services.Service
{
    public class CommandExecutor
    {

        private class LoanFacts
        {
            public decimal TotalInterest { get; set; }
            public decimal TotalAmountToRepay { get; set; }
            public decimal MonthlyEMIAmount { get; set; }
            public decimal InitialNumberOfPayments { get; set; }
        }


        private List<Loan> _loans;
        private List<Payment> _payments;
        public CommandExecutor(List<Loan> loans, List<Payment> payments)
        {
            _loans = loans;
            _payments = payments;
        }

        public CommandResult ExecuteLoan(Command command)
        {
            CommandResult output = new CommandResult();

            if (command.isValidLoan())
            {
                _loans.Add(new Loan()
                {
                    BankName = command.BankName,
                    BorrowerName = command.BorrowerName,
                    LoanRate = command.LoanRate,
                    LoanTermYears = command.LoanTermYears,
                    Principle = command.Principle
                });


                // according to the samples, successfuly adding a loan doesnt require any response?
                output.Status = true;
            }
            else
            {
                output.ErrorMessage = "INVALID LOAN COMMAND";
            }

            return output;
        }


        public CommandResult ExecutePayment(Command command)
        {
            CommandResult output = new CommandResult();

            if (command.isValidPayment())
            {
                _payments.Add(new Payment()
                {
                    BankName = command.BankName,
                    BorrowerName = command.BorrowerName,
                    Amount = command.Payment,
                    EMI_NO = command.EMI_NO
                });


                // according to the samples,   doesnt require any response?
                output.Status = true;
            }
            else
            {
                output.ErrorMessage = "INVALID PAYMENT COMMAND";
            }

            return output;
        }


        public CommandResult ExecuteBalance(Command command)
        {
            CommandResult output = new CommandResult();

            if (command.isValidBalance())
            {
                var theLoan = _loans.Where(x => x.BankName == command.BankName &&
                                    x.BorrowerName == command.BorrowerName).FirstOrDefault();

                if (theLoan != null)
                {
                    // get the loan facts
                    var loanFacts = getLoanFacts(theLoan);

                    var totalEMIPaid = loanFacts.MonthlyEMIAmount * command.EMI_NO.Value;
                    decimal? totalExtraPaid = 0m;
                    decimal totalPaid = 0m;
                    decimal remainingEMIs = loanFacts.InitialNumberOfPayments;

                    var activePayments = _payments.Where(x => x.BankName == command.BankName &&
                                    x.BorrowerName == command.BorrowerName && !(x.EMI_NO > command.EMI_NO)).ToList();

                    if(activePayments!=null)
                    {
                        totalExtraPaid = activePayments.Sum(x => x.Amount);
                        if(totalExtraPaid.HasValue)
                        {
                            totalPaid = totalEMIPaid + totalExtraPaid.Value;

                            // recalculate EMIs remaining
                            var loanOutstanding = loanFacts.TotalAmountToRepay - totalPaid;
                            remainingEMIs = Math.Ceiling(loanOutstanding / loanFacts.MonthlyEMIAmount);

                        }
                    }
                    else
                    {
                        remainingEMIs = remainingEMIs  - command.EMI_NO.Value;
                    }



                    // IDIDI Dale 8000 20
                    output.Message = string.Format("{0} {1} {2} {3}", theLoan.BankName, theLoan.BorrowerName,
                            totalPaid.ToString("0.##"), remainingEMIs.ToString());
                    output.Status = true;
                }
                else
                {
                    output.ErrorMessage = "LOAN NOT FOUND";
                }

            }
            else
            {
                output.ErrorMessage = "INVALID PAYMENT COMMAND";
            }

            return output;
        }

        private LoanFacts getLoanFacts(Loan loan)
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



    }
}
