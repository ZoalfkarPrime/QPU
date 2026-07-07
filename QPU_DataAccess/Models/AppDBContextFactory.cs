using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace QPU_DataAccess.Models;

public class AppDBContextFactory : IDesignTimeDbContextFactory<AppDBContext>
{
    public AppDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
        var connectionString = Environment.GetEnvironmentVariable("QPU_CONNECTION_STRING")
            ?? "Server=localhost,14333;Database=QPU;User Id=sa;Password=Qpu!Sql2022Pass;TrustServerCertificate=True;Encrypt=False;MultipleActiveResultSets=True";

        optionsBuilder.UseSqlServer(connectionString);

        return new AppDBContext(optionsBuilder.Options);
    }
}