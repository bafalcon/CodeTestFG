using System.Collections.Generic;
using System.Linq;
using System.IO; 

using NUnit.Framework;

using Codu.Services.Parser;
using Codu.Services.Service;
using Codu.Services.Model;
using Codu.Services.Helper;

namespace Codu.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }




        [Test]
        public void ParseBalanceValid()
        {
            CommandParser parser = new CommandParser();

            string command = "BALANCE IDIDI Dale 3";

            var result = parser.Parse(command);
            Assert.IsTrue(result.isValidBalance());
        }


        [Test]
        public void ParseLoanValid()
        {
            CommandParser parser = new CommandParser();

            string command = "LOAN IDIDI Dale 5000 1 6";

            var result = parser.Parse(command);
            Assert.IsTrue(result.isValidLoan());
        }

        [Test]
        public void ParseLoanInvalid()
        {
            CommandParser parser = new CommandParser();

            string command = "LOAN IDIDI Dale 1 6";

            var result = parser.Parse(command);
            Assert.IsFalse(result.isValidLoan());
        }

        [Test]
        public void ExecuteLoanValid()
        {
            CommandParser parser = new CommandParser();
            LoanHelper loanHelper = new LoanHelper();
            List<Loan> Loans = new List<Loan>();
            List<Payment> Payments = new List<Payment>();

            string command = "LOAN IDIDI Dale 5000 1 6";
            var parseResult = parser.Parse(command);

            CommandExecutor commandExecutor = new CommandExecutor(Loans, Payments, loanHelper);

            var result = commandExecutor.ExecuteLoan(parseResult);

            Assert.IsTrue(result.Status);
        }

        [Test]
        public void ExecuteBalanceValid()
        {
            CommandParser parser = new CommandParser();
            List<Loan> Loans = new List<Loan>();
            List<Payment> Payments = new List<Payment>();
            LoanHelper loanHelper = new LoanHelper();
            CommandExecutor commandExecutor = new CommandExecutor(Loans, Payments, loanHelper);


            string command = "LOAN IDIDI Dale 5000 1 6";
            var parseResult = parser.Parse(command);

            var result = commandExecutor.ExecuteLoan(parseResult);

            string commandBalance = "BALANCE IDIDI Dale 6";
            var parseResultBalance = parser.Parse(commandBalance);

            var resultBalance = commandExecutor.ExecuteBalance(parseResultBalance);


            Assert.IsTrue(resultBalance.Status);
        }

        [Test]
        public void ExecuteBalanceInvalidEMI()
        {
            CommandParser parser = new CommandParser();
            List<Loan> Loans = new List<Loan>();
            List<Payment> Payments = new List<Payment>();
            LoanHelper loanHelper = new LoanHelper();
            CommandExecutor commandExecutor = new CommandExecutor(Loans, Payments, loanHelper);


            string command = "LOAN IDIDI Dale 5000 1 6";
            var parseResult = parser.Parse(command);

            var result = commandExecutor.ExecuteLoan(parseResult);

            string commandBalance = "BALANCE IDIDI Dale 20";
            var parseResultBalance = parser.Parse(commandBalance);

            var resultBalance = commandExecutor.ExecuteBalance(parseResultBalance);


            Assert.IsTrue(resultBalance.Status==false);
        }


        [Test]
        public void ExecutePaymentValid()
        {
            CommandParser parser = new CommandParser();
            List<Loan> Loans = new List<Loan>();
            List<Payment> Payments = new List<Payment>();
            LoanHelper loanHelper = new LoanHelper();

            CommandExecutor commandExecutor = new CommandExecutor(Loans, Payments, loanHelper);


            string command = "LOAN IDIDI Dale 5000 1 6";
            var parseResult = parser.Parse(command);

            var result = commandExecutor.ExecuteLoan(parseResult);

            string commandPayment = "PAYMENT IDIDI Dale 1000 5";
            var parseResultPayment = parser.Parse(commandPayment);

            var resultPayment = commandExecutor.ExecutePayment(parseResultPayment);


            Assert.IsTrue(resultPayment.Status);
        }

        [Test]
        public void ExecutePaymentBalanceInValidEMI()
        {
            CommandParser parser = new CommandParser();
            List<Loan> Loans = new List<Loan>();
            List<Payment> Payments = new List<Payment>();
            LoanHelper loanHelper = new LoanHelper();

            CommandExecutor commandExecutor = new CommandExecutor(Loans, Payments, loanHelper);


            string command = "LOAN IDIDI Dale 5000 1 6";
            var parseResult = parser.Parse(command);

            var result = commandExecutor.ExecuteLoan(parseResult);

            string commandPayment = "PAYMENT IDIDI Dale 1000 5";
            var parseResultPayment = parser.Parse(commandPayment);

            var resultPayment = commandExecutor.ExecutePayment(parseResultPayment);

            string commandBalance = "BALANCE IDIDI Dale 12";
            var parseResultBalance = parser.Parse(commandBalance);

            var resultBalance = commandExecutor.ExecuteBalance(parseResultBalance);


            Assert.IsTrue(resultBalance.Status==false);
        }


        [Test]
        public void TestLoanHelperLoanFacts()
        {
            CommandParser parser = new CommandParser();
            List<Loan> Loans = new List<Loan>();
            List<Payment> Payments = new List<Payment>();
            LoanHelper loanHelper = new LoanHelper();

            CommandExecutor commandExecutor = new CommandExecutor(Loans, Payments, loanHelper);


            string command = "LOAN IDIDI Dale 5000 1 6";
            var parseResult = parser.Parse(command);

            var result = commandExecutor.ExecuteLoan(parseResult);

            var theLoan = Loans.FirstOrDefault();

            var loanFacts = loanHelper.getLoanFacts(theLoan);

            Assert.IsTrue(loanFacts.InitialNumberOfPayments == 12);
        }

        [Test]
        public void TestLoanHelperLoanStatus()
        {
            CommandParser parser = new CommandParser();
            List<Loan> Loans = new List<Loan>();
            List<Payment> Payments = new List<Payment>();
            LoanHelper loanHelper = new LoanHelper();

            CommandExecutor commandExecutor = new CommandExecutor(Loans, Payments, loanHelper);


            string command = "LOAN IDIDI Dale 10000 5 4";
            var parseResult = parser.Parse(command);

            var result = commandExecutor.ExecuteLoan(parseResult);

            var theLoan = Loans.FirstOrDefault();

            var loanFacts = loanHelper.getLoanFacts(theLoan);

            var loanStatus = loanHelper.getLoanStatus(loanFacts, Payments, 5);

            Assert.IsTrue(loanStatus.TotalPaid == 1000);
        }



        [Test]
        public void FileServiceThrow()
        {
            FileService fs = new FileService();

            string absFileAddress = "c:\nothingfound";
            bool foundfile = true;

            try
            {
                fs.GetFileStream(absFileAddress);
            }
            catch
            {
                foundfile = false;
            }
             
            Assert.IsFalse(foundfile);
        }

        [Test]
        public void CommandHelperThrow()
        {
            CommandHelper commandHelper = new CommandHelper();
            FileStream emptyFileStream = null;

            bool foundfile = true;

            try
            {
                var result = commandHelper.GetCommandLines(emptyFileStream);
            }
            catch
            {
                foundfile = false;
            }

            Assert.IsFalse(foundfile);
        }



        [Test]
        public void TestCommandProcessor()
        {
            CommandParser parser = new CommandParser();
            List<Loan> Loans = new List<Loan>();
            List<Payment> Payments = new List<Payment>();
            LoanHelper loanHelper = new LoanHelper();

            CommandExecutor commandExecutor = new CommandExecutor(Loans, Payments, loanHelper);
            CommandProcessor commandProcessor = new CommandProcessor(parser, commandExecutor);

            string command = "LOAN IDIDI Dale 5000 1 6";
            var result = commandProcessor.ProcessCommand(command);
 

            Assert.IsTrue(result.Status);
        }

    }
}