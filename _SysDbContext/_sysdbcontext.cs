using crud_operation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_operation._SysDbContext
{
    public class _sysdbcontext:DbContext
    {
        public _sysdbcontext(DbContextOptions<_sysdbcontext> options) : base(options)
        {

        }

        public DbSet<RmpiStuInfoModel> TblRmpiStuInfo { get; set; }
    }
}
