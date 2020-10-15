using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chambers.Models;

namespace Chambers.Interfaces
{
    public interface IFileManager
    {
        byte[] GetFile(string fileName);
        bool SetSortOrder(string order);
        string UploadFile(string filename, byte[] data);
        List<DMFileInfo> GetFileList();
    }
}
