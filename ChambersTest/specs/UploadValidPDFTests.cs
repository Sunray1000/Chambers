using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Chambers.Controllers;
using Chambers.DataStore;
using Chambers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace ChambersTest.specs
{
    [Trait("Valid pdf are uploaded", "")]
    public class UploadValidPDFTests : TestBase
    {
        public UploadValidPDFTests() : base()
        {
        }

        [Fact(DisplayName = "Upload one file")]
        public void Upload_One_Valid_File_Test()
        {
            var storeCalls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.AddFile(It.IsAny<string>(), It.IsAny<byte[]>())).Callback(() => { storeCalls++; });
            var _filesController = new FilesController(_configuration, _store.Object);

            var result = _filesController.UploadFile(GetFormFileMock(_workingPDF).Object);
            Assert.IsAssignableFrom<CreatedResult>(result);
            Assert.True(storeCalls == 1);
        }

        [Fact(DisplayName = "Upload two files")]
        public void Upload_Two_Valid_File_Test()
        {
            var storeCalls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.AddFile(It.IsAny<string>(), It.IsAny<byte[]>())).Callback(() => { storeCalls++; });
            var _filesController = new FilesController(_configuration, _store.Object);

            var result = _filesController.UploadFile(GetFormFileMock(_workingPDF).Object);
            Assert.IsAssignableFrom<CreatedResult>(result);

            result = _filesController.UploadFile(GetFormFileMock(_workingPDF).Object);
            Assert.IsAssignableFrom<CreatedResult>(result);

            Assert.True(storeCalls == 2);
        }

        //[Fact(DisplayName = "Success notified")]
        //public void User_Is_Notified_Test()
        //{
        //    throw new NotImplementedException("Implement Me");
        //}
    }
}
