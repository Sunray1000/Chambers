using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chambers.Models;

namespace Chambers.DataStore
{
    public interface IStore
    {
        bool AddFile(string name, byte[] fileData);
        bool AddTextFile(string name, string text);
        byte[] GetFile(string filename);
        string GetTextFile(string filename);
        bool DeleteFile(string filename);
        List<DMFileInfo> GetFileList();
    }
}
