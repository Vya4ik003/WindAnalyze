using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Readers
{
    interface IFileReader
    {
        ReadFileResult GetReadingResult(); 
    }
}
