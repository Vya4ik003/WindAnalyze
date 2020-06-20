using IO.Information;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IO.FilesFormat
{
    abstract class BaseFile
    {
        public abstract IOResult ReadFile(FileStream stream, ReadData data);
    }
}
