using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Chambers.Controllers;
using Chambers.DataStore;
using Chambers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ChambersTest.specs
{
    public class TestBase
    {
        protected readonly string _workingPDF;
        protected readonly string _tooLargePDF;
        protected readonly string _notPDF;
        protected readonly Mock<IStore> _store;
        protected readonly FilesController _filesController;
        protected readonly IConfiguration _configuration;

        public TestBase()
        {
            var configuration = new Mock<IConfiguration>();
            
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value).Returns("d:\\store\\");
            configuration.Setup(a => a.GetSection("filelocation")).Returns(configurationSection.Object);
            //_store = new Mock<IStore>();
            //_store.Setup(f => f.AddFile(It.IsAny<string>(), It.IsAny<byte[]>()));
            //_store.Setup(f => f.GetFile(It.IsAny<string>())).Returns(new byte[] {0, 0, 0, 0});
            //_store.Setup(f => f.DeleteFile("stored.pdf")).Returns(true);
            //_store.Setup(f => f.DeleteFile("notstored.pdf")).Returns(false);

            //_filesController = new FilesController(configuration.Object, _store.Object);

            _configuration = configuration.Object;
            _workingPDF = CreateFileOfSize(5000, "pdf");
            _tooLargePDF = CreateFileOfSize(10000000, "pdf");
            _notPDF = CreateFileOfSize(5000, "txt");
        }
        protected Mock<IFormFile> GetFormFileMock(string file)
        {
            var iFormFileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream

            var content = File.ReadAllText(file);
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            iFormFileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            iFormFileMock.Setup(x => x.FileName).Returns(file);
            iFormFileMock.Setup(x => x.Length).Returns(content.Length);

            return iFormFileMock;
        }

        protected string CreateFileOfSize(int size, string ext)
        {
            string fileName = Path.GetTempFileName();
            string newFileName = string.Empty;

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.SetLength(size);
            }

            newFileName = Path.ChangeExtension(fileName, ext);
            File.Move(fileName, newFileName);

            return newFileName;
        }
    }
}
