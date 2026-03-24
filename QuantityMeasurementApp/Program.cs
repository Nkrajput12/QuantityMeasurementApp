using QuantityMeasurementApp.Controllers;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepoLayer.Repositories;
using QuantityMeasurementRepoLayer.Interfaces;
using QuantityMeasurementRepoLayer.Config;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementRepoLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using QuantityMeasurementRepoLayer.Data;


namespace QuantityMeasurementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //get connection string
            string conn = DatabaseConfig.GetConnectionString();
            var builder = new DbContextOptionsBuilder<MeasurementDbContext>();
            builder.UseSqlServer(conn);
            MeasurementDbContext dbContext = new MeasurementDbContext(builder.Options);
            // Create Repo object
            ICacheRepository cacheRepo = new InMemoryCacheRepository();
            IDatabaseRepository dataRepo = new DatabaseRepository(dbContext);
            
            // Inject Repo into Service
            IQuantityMeasurementService service = new QuantityMeasurementService(cacheRepo,dataRepo);
            
            // Inject Service into Controller
            QuantityMeasurementController controller = new QuantityMeasurementController(service);

            // Run App
            controller.Run();
        }
    }
}