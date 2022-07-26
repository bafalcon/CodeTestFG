﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Codu.Services.Enums;

namespace Codu.Services.Helper
{
    public class CommandHelper
    {
        
        public CommandHelper() { }

        public List<string> GetCommandLines(FileStream fileStream)
        {
            List<string> output = new List<string>();
            string line = string.Empty;

            try
            {

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        var items = line.Split(' ');
                        if (items != null)
                        {
                            if (items.Length > 0)
                            {
                                output.Add(line);
                            }
                        }
                    }
                }

            }
            //  catch (Exception ex)// avoid warning
            catch
            {
                // TODO: if we had a dto to pass back a results class
                throw;
            }

            return output;
        }



    }
}
