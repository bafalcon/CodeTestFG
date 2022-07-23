using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Codu.Services.Parser;
using Codu.Services.Service;
using Codu.Services.Classes;

namespace Codu.Services.Service
{
    public class CommandProcessor
    {
        private CommandParser _commandParser;
        private CommandExecutor _commandExecutor;
        public CommandProcessor(CommandParser commandParser, CommandExecutor commandExecutor)
        {
            _commandParser = commandParser;
            _commandExecutor = commandExecutor;
        }

        public CommandResult ProcessCommand(string command)
        {
            CommandResult result = new CommandResult();

            try
            {
                var parsedCommand = _commandParser.Parse(command);
                switch (parsedCommand.Type)
                {
                    case Enums.CommandType.Loan:
                        result = _commandExecutor.ExecuteLoan(parsedCommand);
                        break;
                    case Enums.CommandType.Payment:
                        result = _commandExecutor.ExecutePayment(parsedCommand);
                        break;
                    case Enums.CommandType.Balance:
                        result = _commandExecutor.ExecuteBalance(parsedCommand);
                        break;

                }                
            }
            catch
            {
                result.ErrorMessage = "ERROR PROCESSING COMMAND";
            }
            return result;
        }
    }
}
