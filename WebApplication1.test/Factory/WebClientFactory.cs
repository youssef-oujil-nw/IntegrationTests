using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Repositories.Context;

namespace WebApplication1.test.Factory;

public class WebClientFactory:WebApplicationFactory<Program> {
    private WeatherDbContext _dbContext;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var service = services.SingleOrDefault(
                serviceDescriptor => serviceDescriptor.ServiceType ==
                                     typeof(DbContextOptions<WeatherDbContext>));
            if (service != null)
            {
                services.Remove(service);
            } // Add an in-memory database.

            services.AddDbContext<WeatherDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDb");
            });

            // Build the service provider.
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            // context (ApplicationDbContext).
            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            _dbContext = scopedServices.GetRequiredService<WeatherDbContext>();

            // Ensure the database is created.
            ResetDatabase();
        }).UseEnvironment("Development");
    }

    private void ResetDatabase()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }
    
}