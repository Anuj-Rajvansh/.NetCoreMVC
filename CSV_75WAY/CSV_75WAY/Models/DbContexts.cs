using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSV_75WAY.Models
{
    public class DbContexts: DbContext
    {
        public DbContexts(DbContextOptions<DbContexts> options)
           : base(options)
        {
        }
        public DbSet<CSV_75WAY.Models.Building> Buildings { get; set; }
    }
}
