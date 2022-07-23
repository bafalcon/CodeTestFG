using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Codu.Services.Service
{
    public class FileService
    {
        public FileService()
        {

        }

        public FileStream GetFileStream(string absFilePath)
        {
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(absFilePath, FileMode.Open);
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            return fileStream;
        }

    }
}
