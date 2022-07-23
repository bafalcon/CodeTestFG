using System;
using System.Collections.Generic;


using Codu.Services.Service;
using Codu.Services.Helper;
using Codu.Services.Parser;
using Codu.Services.Model;

namespace Codu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileService fileService = new FileService();
            CommandHelper commandHelper = new CommandHelper();
            CommandParser commandParser = new CommandParser();
            List<Loan> Loans = new List<Loan>();
            List<Payment> Payments = new List<Payment>();
            CommandExecutor commandExecutor = new CommandExecutor(Loans, Payments);
            CommandProcessor commandProcessor = new CommandProcessor(commandParser, commandExecutor);

            try
            {
                var fs = fileService.GetFileStream(args[0]);
                if (fs != null)
                {
                    var commands = commandHelper.GetCommandLines(fs);

                    foreach (var cmd in commands)
                    {
                        var result = commandProcessor.ProcessCommand(cmd);
                        if (result.Status)
                        {
                            if (!string.IsNullOrEmpty(result.Message))
                            {
                                Console.WriteLine(result.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine(result.ErrorMessage);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
