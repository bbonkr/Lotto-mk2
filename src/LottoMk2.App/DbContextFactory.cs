using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LottoMk2.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LottoMk2.App;

public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString = DefaultValues.DefaultConnectionString;

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        dbContextOptionsBuilder.UseSqlite(connectionString, sqlServerOptions =>
        {
            sqlServerOptions.MigrationsAssembly(typeof(LottoMk2.Data.Sqlite.PlaceHolder).Assembly.FullName);
        });

        var dbContextOptions = dbContextOptionsBuilder.Options;

        return new AppDbContext(dbContextOptions);
    }
}
