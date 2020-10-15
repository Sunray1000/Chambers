using System;
using System.Collections.Generic;
using System.Text;
using Chambers.Controllers;
using Chambers.DataStore;
using Chambers.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ChambersTest.specs
{
    [Trait("A PDF file is bigger than 5Mb is uploaded","")]
    public class UploadTooLargeTest : TestBase
    {
        [Fact(DisplayName = "File fails size validation")]
        public void Upload_File_Too_Large()
        {
            var storeCalls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.AddFile(It.IsAny<string>(), It.IsAny<byte[]>())).Callback(() => { storeCalls++; });
            var _filesController = new FilesController(_configuration, _store.Object);

            Assert.Throws<HttpResponseException>(()=>_filesController.UploadFile(GetFormFileMock(_tooLargePDF).Object));
            Assert.True(storeCalls == 0);
        }
    }
}
