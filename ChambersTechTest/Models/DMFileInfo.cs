using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Chambers.Models
{
    //public class FileDbContext : DbContext
    //{
    //    private readonly IConfiguration _config;

    //    public DbSet<DMFileInfo> FilesDbSet { get; set; }
    //}

    public class DMFileInfo
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public long Size { get; set; }
    }
}
