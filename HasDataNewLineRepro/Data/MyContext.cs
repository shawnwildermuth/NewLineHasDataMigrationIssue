using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HasDataNewLineRepro.Data
{
  public class MyContext : DbContext
  {
    private readonly IHostingEnvironment _env;

    public MyContext(DbContextOptions<MyContext> options, IHostingEnvironment env) : base(options)
    {
      _env = env;
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      var arts = new List<Product>();

      arts.Add(new Product()
      {
        Id = 1,
        Title = "Scarf Works",
        Price = 19.99,
        Description = @"
<div>
  <p>This is a scarf and all that goes with that.</p>
</div>"
      });

      // Just Carriage Return Doesn't work (this came from a Linux Line Endings Json File)
      arts.Add(new Product()
      {
        Id = 2,
        Title = "Belt Doesn't",
        Price = 19.99,
        Description = "<div>\n<p>This is a scarf and all that goes with that.</p>\n</div>"
      });

      modelBuilder.Entity<Product>()
        .HasData(arts.ToArray());
    }
  }
}
