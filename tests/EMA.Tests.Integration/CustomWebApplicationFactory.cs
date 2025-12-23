using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EMA.Data;
using EMA.Models.Entities;

namespace EMA.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();

            if (!db.Employees.Any())
            {
                db.Employees.AddRange(
                    new Employee { Id = Guid.NewGuid(), Name = "Seed A", Email = "a@a.com", Phone = "1", Gender = "M", Salary = 100 },
                    new Employee { Id = Guid.NewGuid(), Name = "Seed B", Email = "b@b.com", Phone = "2", Gender = "F", Salary = 200 }
                );
                db.SaveChanges();
            }
        });
    }
}
