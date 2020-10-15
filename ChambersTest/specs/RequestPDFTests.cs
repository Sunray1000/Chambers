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
    [Trait("Get the list of PDFs","")]
    public class GetPDF : TestBase
    {
        [Fact(DisplayName = "Uploaded PDF is returned")]
        public void Is_PDF_In_List()
        {
            var getCalls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.GetFile(It.IsAny<string>())).Returns(new byte[]{1,2,3,4,5,6}).Callback(() => { getCalls++; });
            var _filesController = new FilesController(_configuration, _store.Object);

            var result = _filesController.GetFile(_workingPDF);
            Assert.IsAssignableFrom<FileContentResult>(result);
            Assert.True(getCalls == 1);
        }
    }
}
