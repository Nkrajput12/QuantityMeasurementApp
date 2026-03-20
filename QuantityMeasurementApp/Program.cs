using QuantityMeasurementApp.Controllers;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepoLayer.Repositories;
using QuantityMeasurementRepoLayer.Interfaces;
using QuantityMeasurementRepoLayer.Config;
using QuantityMeasurementRepoLayer;

namespace QuantityMeasurementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //get connection string
            string conn = DatabaseConfig.GetConnectionString();
            // Create Repo object
            ICacheRepository cacheRepo = new InMemoryCacheRepository();
            IDatabaseRepository dataRepo = new DatabaseRepository(conn);
            
            // Inject Repo into Service
            IQuantityMeasurementService service = new QuantityMeasurementService(cacheRepo,dataRepo);
            
            // Inject Service into Controller
            QuantityMeasurementController controller = new QuantityMeasurementController(service);

            // Run App
            controller.Run();
        }
    }
}