using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IO.FilesFormat
{
    interface IFile
    {
        IOResult ReadFile(FileStream stream, ReadData data);
    }
}
