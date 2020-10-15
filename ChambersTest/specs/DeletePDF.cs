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
    [Trait("Delete the PDF", "")]
    public class DeletePDF : TestBase
    {
        [Fact(DisplayName = "Delete PDF in list")]
        public void Delete_PDF_In_List()
        {
            var storeCalls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.DeleteFile("delete.pdf")).Returns(true).Callback(() => { storeCalls++; });
            var _filesController = new FilesController(_configuration, _store.Object);

            var result = _filesController.Delete("delete.pdf");
            Assert.IsAssignableFrom<OkResult>(result);
            Assert.True(storeCalls == 1);
        }

        [Fact(DisplayName = "Delete PDF not in list")]
        public void Delete_PDF_Not_In_List()
        {
            var storeCalls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.DeleteFile("delete.pdf")).Returns(false).Callback(() => { storeCalls++; });
            var _filesController = new FilesController(_configuration, _store.Object);

            var result = _filesController.Delete("delete.pdf");
            Assert.IsAssignableFrom<NotFoundResult>(result);
            Assert.True(storeCalls == 1);
        }
    }
}
