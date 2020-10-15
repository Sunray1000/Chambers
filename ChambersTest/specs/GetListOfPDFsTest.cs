using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Chambers.Controllers;
using Chambers.DataStore;
using Chambers.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ChambersTest.specs
{
    [Trait("Get the list of PDFs in the system","")]
    public class GetListOfPDFsTest : TestBase
    {
        [Fact(DisplayName = "Sorted by filename")]
        public void Get_Sorted_By_Name_List_of_PDF()
        {
            var getCalls = 0;
            var _store = new Mock<IStore>();
            var file1 = new DMFileInfo {Location = "loc1", Name = "file3.pdf", Size=1000};
            var file2 = new DMFileInfo {Location = "loc2", Name = "file2.pdf", Size=2000};
            var file3 = new DMFileInfo {Location = "loc3", Name = "file1.pdf", Size=3000};
            var list = new List<DMFileInfo> {file1, file2, file3};
            _store.Setup(f => f.GetFileList()).Returns(new List<DMFileInfo>{file1,file2,file3}).Callback(() => { getCalls++; });
            _store.Setup(f => f.GetTextFile("sortingorderfile")).Returns("filename").Callback(() => { getCalls++; });
            var _filesController = new FilesController(_configuration, _store.Object);

            var result = _filesController.GetFileList();
            var serialisedList = JsonSerializer.Serialize(new List<DMFileInfo> {file3, file2, file1});
            Assert.IsType<string>(result);
            Assert.Equal(result,serialisedList);
            Assert.True(getCalls == 2);
        }

        [Fact(DisplayName = "Sorted by size")]
        public void Get_Sorted_By_Size_List_of_PDF()
        {
            var getCalls = 0;
            var _store = new Mock<IStore>();
            var file1 = new DMFileInfo {Location = "loc1", Name = "file3.pdf", Size=1000};
            var file2 = new DMFileInfo {Location = "loc2", Name = "file2.pdf", Size=3000};
            var file3 = new DMFileInfo {Location = "loc3", Name = "file1.pdf", Size=2000};
            _store.Setup(f => f.GetFileList()).Returns(new List<DMFileInfo>{file1,file2,file3}).Callback(() => { getCalls++; });
            _store.Setup(f => f.GetTextFile("sortingorderfile")).Returns("size").Callback(() => { getCalls++; });

            var _filesController = new FilesController(_configuration, _store.Object);
            var serialisedList = JsonSerializer.Serialize(new List<DMFileInfo> {file1, file3, file2});
            var result = _filesController.GetFileList();
            Assert.IsType<string>(result);
            Assert.Equal(result,serialisedList);
            Assert.True(getCalls == 2);
        }

        [Fact(DisplayName = "Sorted by Location")]
        public void Get_Sorted_By_Location_List_of_PDF()
        {
            var getCalls = 0;
            var _store = new Mock<IStore>();
            var file1 = new DMFileInfo {Location = "loc2", Name = "file1.pdf", Size=1000};
            var file2 = new DMFileInfo {Location = "loc3", Name = "file2.pdf", Size=4000};
            var file3 = new DMFileInfo {Location = "loc1", Name = "file3.pdf", Size=2000};
            _store.Setup(f => f.GetFileList()).Returns(new List<DMFileInfo>{file1,file2,file3}).Callback(() => { getCalls++; });
            _store.Setup(f => f.GetTextFile("sortingorderfile")).Returns("location").Callback(() => { getCalls++; });

            var _filesController = new FilesController(_configuration, _store.Object);
            var serialisedList = JsonSerializer.Serialize(new List<DMFileInfo> {file3, file1, file2});
            var result = _filesController.GetFileList();
            Assert.IsType<string>(result);
            Assert.Equal(result,serialisedList);
            Assert.True(getCalls == 2);
        }
    }
}
