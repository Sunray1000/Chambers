using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chambers.DataStore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Chambers.Models;
using Chambers.Responses;

namespace Chambers.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private const int FiveMb = 1024 * 1024 * 5;
        private readonly IConfiguration _configuration;
        private readonly IStore _store;

        public FilesController(IConfiguration configuration,IStore store)
        {
            _configuration = configuration;
            _store = store;
        }

        [HttpPut("{order}")]
        public IActionResult SetSortOrder(string order)
        {
            switch (order)
            {
                case "location":
                    break;
                case "filename":
                    break;
                case "length":
                    break;
                default:
                    return NotFound();
            }

            _store.AddTextFile("sortingorderfile", order);
            return Accepted();
        }

        [HttpDelete("{filename}")]
        public IActionResult Delete(string filename)
        {
            if (_store.DeleteFile(filename))
            {
                return Ok();
            }

            return NotFound();
        }


        [HttpGet("{filename}")]
        public IActionResult GetFile(string filename)
        {
            byte[] fileData = _store.GetFile(filename);
            if (fileData == null)
            {
                return NotFound();
            }

            var result = new FileContentResult(fileData, "application/octet-stream") {FileDownloadName = filename};
            return result;
        }

        [HttpPost]
        [RequestSizeLimit(5242880)]
        public IActionResult UploadFile(IFormFile file)
        {
            byte[] fileContents;
            using (Stream sourceStream = file.OpenReadStream())
            {
                fileContents = new byte[sourceStream.Length];
                sourceStream.Read(fileContents, 0, Convert.ToInt32(file.Length));
            }

            if (fileContents.Length > FiveMb)
            {
                var ex = new HttpResponseException();
                ex.Status = 314;
                throw ex;
            }

            if (Path.GetExtension(file.FileName) == ".pdf")
            {
                _store.AddFile(file.FileName, fileContents);
                return Created("",file.FileName);
            }
            return BadRequest();
        }

        [HttpGet("GetFileList")]
        public string GetFileList()
        {
            string sortOrder = _store.GetTextFile("sortingorderfile");
            var list = _store.GetFileList();
            list.RemoveAll(f => f.Name == "sortingorderfile");
            List<DMFileInfo> result = list;

            switch (sortOrder)
            {
                case "location":
                    result = list.OrderBy(o => o.Location).ToList();
                    break;
                case "size":
                    result = list.OrderBy(o => o.Size).ToList();
                    break;
                case "filename":
                    result = list.OrderBy(o => o.Name).ToList();
                    break;
            }

            return JsonSerializer.Serialize(result);
        }
    }
}
