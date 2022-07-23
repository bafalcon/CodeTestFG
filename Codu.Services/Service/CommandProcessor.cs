using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Codu.Services.Parser;
using Codu.Services.Service;

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

        public string ProcessCommand(string command)
        {
            string output = string.Empty;

            try
            {
                var parsedCommand = _commandParser.Parse(command);
                CommandExecutor.CommandResult result = new CommandExecutor.CommandResult();
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

                // maybe overkill
                if (result.Status)
                {
                    output = result.Message;
                }
                else
                {
                    output = result.ErrorMessage;
                }
            }
            catch
            {
                output = "ERROR PROCESSING COMMAND";
            }
            return output;
        }
    }
}
