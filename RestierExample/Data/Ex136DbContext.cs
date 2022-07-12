using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestierExample.Data
{
    public class RestierExampleDbContext : DbContext
    {
        public RestierExampleDbContext(DbContextOptions<RestierExampleDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
    }
}
