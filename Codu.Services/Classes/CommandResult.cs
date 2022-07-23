using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codu.Services.Classes
{
    public class CommandResult
    {
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }

        public CommandResult()
        {
            ErrorMessage = "INVALID COMMAND";
        }
    }
}
