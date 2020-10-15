using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Chambers.Models;
using Microsoft.Extensions.Configuration;

namespace Chambers.DataStore
{
    public class Store : IStore
    {
        private readonly IConfiguration _configuration;

        public Store(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool AddFile(string filename, byte[] fileData)
        {
            var storeFile = $"{_configuration["filelocation"]}/{Path.GetFileName(filename)}";

            if (File.Exists(storeFile))
            {
                return false;
            }
            

            File.WriteAllBytes(storeFile, fileData);
            return true;
        }

        public bool AddTextFile(string filename, string text)
        {
            var storeFile = $"{_configuration["filelocation"]}/{filename}";

            if (File.Exists(storeFile))
            {
                return false;
            }

            File.WriteAllText(storeFile, text);
            return true;
        }

        public byte[] GetFile(string filename)
        {
            var storeFile = $"{_configuration["filelocation"]}/{filename}";
            if (File.Exists(storeFile))
            {
                return File.ReadAllBytes(storeFile);
            }

            return null;
        }

        public string GetTextFile(string filename)
        {
            var storeFile = $"{_configuration["filelocation"]}/{filename}";

            if (File.Exists(storeFile))
            {
                return File.ReadAllText(storeFile);
            }

            return string.Empty;
        }

        public bool DeleteFile(string filename)
        {
            var storeFile = $"{_configuration["filelocation"]}/{filename}";
            if (File.Exists(storeFile))
            {
                File.Delete(storeFile);
                return true;
            }

            return false;
        }

        public List<DMFileInfo> GetFileList()
        {
            var storePath = $"{_configuration["filelocation"]}";
            var resultList = new List<DMFileInfo>();

            foreach (var file in Directory.EnumerateFiles(storePath))
            {
                var data = new FileInfo(file);
                var fileInfo = new DMFileInfo{Name = data.Name, Location = data.DirectoryName, Size = data.Length};
                resultList.Add(fileInfo);
            }

            return resultList;
        }

    }
}
