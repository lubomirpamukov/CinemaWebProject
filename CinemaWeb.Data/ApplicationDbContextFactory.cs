using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<CinemaDbContext>
{
    public CinemaDbContext CreateDbContext(string[] args) 
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<CinemaDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new CinemaDbContext(optionsBuilder.Options);
    }
}
