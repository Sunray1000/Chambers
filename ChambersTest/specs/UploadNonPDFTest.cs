using System;
using System.Collections.Generic;
using System.Text;
using Chambers.Controllers;
using Chambers.DataStore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ChambersTest.specs
{
    [Trait("A non-PDF is denied", "")]
    public class UploadNonPDFTest : TestBase
    {
        [Fact(DisplayName = "File fails validation")]
        public void File_Fails_Validation()
        {
            var storeCalls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.AddFile(It.IsAny<string>(), It.IsAny<byte[]>())).Callback(() => { storeCalls++; });
            var _filesController = new FilesController(_configuration, _store.Object);

            var result = _filesController.UploadFile(GetFormFileMock(_notPDF).Object);
            Assert.IsAssignableFrom<BadRequestResult>(result);
            Assert.True(storeCalls == 0);
        }
    }
}
