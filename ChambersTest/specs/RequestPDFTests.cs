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
    [Trait("Get a PDF","")]
    public class GetPDF : TestBase
    {
        [Fact(DisplayName = "Get PDF in list")]
        public void Is_PDF_In_List()
        {
            var calls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.GetFile(It.IsAny<string>())).Returns(new byte[]{1,2,3,4,5,6}).Callback(() => { calls++; });
            var _filesController = new FilesController(_configuration, _store.Object);

            var result = _filesController.GetFile(_workingPDF);
            Assert.IsAssignableFrom<FileContentResult>(result);
            Assert.Equal(1,calls);
        }

        [Fact(DisplayName = "Get PDF not in list")]
        public void Is_PDF_Not_In_List()
        {
            var calls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.GetFile(It.IsAny<string>())).Returns(()=>null).Callback(() => { calls++; });
            var _filesController = new FilesController(_configuration, _store.Object);

            var result = _filesController.GetFile(_workingPDF);
            Assert.IsAssignableFrom<NotFoundResult>(result);
            Assert.Equal(1,calls);
        }
    }
}
