using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codu.Services.Classes;

namespace Codu.Services.Parser
{
    public class CommandParser
    {


        public CommandParser() { }

        public Command Parse(string commandString)
        {
            Command command = new Command();

            if (string.IsNullOrEmpty(commandString))
            {
                throw new Exception("empty command string");
            }


            var items = commandString.Split(' ');
            if (items != null)
            {
                if (items.Length > 0)
                {
                    var commandName = items[0];
                    switch (commandName.ToUpper())
                    {
                        case "LOAN":
                            //LOAN IDIDI Dale 10000 5 4
                            command.Type = Enums.CommandType.Loan;
                            command.BankName = getStringItem(items, 1);
                            command.BorrowerName = getStringItem(items, 2);
                            command.Principle = getDecimalItem(items, 3);
                            command.LoanTermYears = getDecimalItem(items, 4);
                            command.LoanRate = getDecimalItem(items, 5);

                            break;
                        case "PAYMENT":
                            //                            Format - PAYMENT BANK_NAME BORROWER_NAME LUMP_SUM_AMOUNT EMI_NO
                            //Example - PAYMENT MBI Dale 1000 5 means a lump sum payment of 1000 was done by Dale to MBI after 5 EMI payments.
                            command.Type = Enums.CommandType.Payment;
                            command.BankName = getStringItem(items, 1);
                            command.BorrowerName = getStringItem(items, 2);
                            command.Payment = getDecimalItem(items, 3);
                            command.EMI_NO = getIntItem(items, 4);
                            break;
                        case "BALANCE":
//                            Input format -BALANCE BANK_NAME BORROWER_NAME EMI_NO
//Example - BALANCE MBI Harry 12 means - print the amount paid including 12th EMI, and EMIs
//remaining for user Harry against the lender MB
                            command.Type = Enums.CommandType.Balance;
                            command.BankName = getStringItem(items, 1);
                            command.BorrowerName = getStringItem(items, 2);
                            command.EMI_NO = getIntItem(items, 3);
                            break;
                    }
                }
            }

            return command;
        }

        private string getStringItem(string[] items, int position)
        {
            var output = string.Empty;

            if (items[position] != null)
            {
                output = items[position];
            }
            return output;
        }

        private decimal? getDecimalItem(string[] items, int position)
        {
            decimal? output=null;

            if (items[position] != null)
            {
                try
                {
                    output = decimal.Parse(items[position]);
                }
                catch { }
            }
            return output;
        }

        private int? getIntItem(string[] items, int position)
        {
            int? output = null;

            if (items[position] != null)
            {
                try
                {
                    output = int.Parse(items[position]);
                }
                catch { }
            }
            return output;
        }

    }
}
