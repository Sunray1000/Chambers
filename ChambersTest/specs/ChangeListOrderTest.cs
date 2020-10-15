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

    [Trait("Change the list order", "")]
    public class ChangeListOrderTest : TestBase
    {
        [Fact(DisplayName = "Change the list order ")]
        public void Store_List_Order_Ok()
        {
            var storeCalls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.AddTextFile("sortingorderfile","filename")).Returns(true).Callback(() => { storeCalls++; });
            var _filesController = new FilesController(_configuration, _store.Object);
            var result = _filesController.SetSortOrder("filename");
            Assert.IsAssignableFrom<AcceptedResult>(result);
            Assert.True(storeCalls == 1);
        }

        [Fact(DisplayName = "Return list order changed fail")]
        public void Return_List_Order_Update_Fail()
        {
            var storeCalls = 0;
            var _store = new Mock<IStore>();
            _store.Setup(f => f.AddTextFile("sortingorderfile","xxx")).Returns(true).Callback(() => { storeCalls++; });
            var _filesController = new FilesController(_configuration, _store.Object);
            var result = _filesController.SetSortOrder("yyy");
            Assert.IsAssignableFrom<NotFoundResult>(result);
            Assert.True(storeCalls == 0);
        }
    }
}
