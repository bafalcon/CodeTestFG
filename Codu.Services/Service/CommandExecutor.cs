using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Codu.Services.Model;
using Codu.Services.Parser;
using Codu.Services.Classes;
using Codu.Services.Helper;

namespace Codu.Services.Service
{
    public class CommandExecutor
    {




        private List<Loan> _loans;
        private List<Payment> _payments;
        private LoanHelper _loanHelper;
        public CommandExecutor(List<Loan> loans, List<Payment> payments, LoanHelper loanHelper)
        {
            _loans = loans;
            _payments = payments;
            _loanHelper = loanHelper;
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

               // according to the samples,   doesnt require any response? What if payment > loanbalance
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
                    var loanFacts = _loanHelper.getLoanFacts(theLoan);

                    if(command.EMI_NO.Value> loanFacts.InitialNumberOfPayments)
                    {
                        output.ErrorMessage = "INVALID EMI NUMBER";
                    }
                    else
                    {
                        var activePayments = new List<Payment>();
                        activePayments = _payments.Where(x => x.BankName == command.BankName &&
                                      x.BorrowerName == command.BorrowerName && !(x.EMI_NO > command.EMI_NO)).ToList();

                        var loanStatus = _loanHelper.getLoanStatus(loanFacts, activePayments, command.EMI_NO.Value);

                        if(command.EMI_NO.Value>loanStatus.RemainingEMIs)
                        {
                            output.ErrorMessage = "LOAN PAID OFF AT THIS EMI NUMBER";
                        }
                        else
                        {
                            // IDIDI Dale 8000 20
                            output.Message = string.Format("{0} {1} {2} {3}", theLoan.BankName, theLoan.BorrowerName,
                                    loanStatus.TotalPaid.ToString("0.##"), loanStatus.RemainingEMIs.ToString());
                            output.Status = true;
                        }
                    }
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





    }
}
