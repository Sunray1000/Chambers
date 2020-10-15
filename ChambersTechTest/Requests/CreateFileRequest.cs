using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chambers.Requests
{
    public class CreateFileRequest
    {
        public string FileName { get; set; }
        public string FileContent { get; set; }
        public int FileSize { get; set; }
    }
}
