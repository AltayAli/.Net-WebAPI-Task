using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPİ.Data.Entites
{
    public class ProductDBContext:DbContext
    {
        public ProductDBContext(DbContextOptions options):base(options){}
        public DbSet<Product> Products { get; set; }
    }
}
