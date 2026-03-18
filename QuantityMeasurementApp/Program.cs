using QuantityMeasurementApp.Controllers;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepoLayer.Repositories;
using QuantityMeasurementRepoLayer.Interfaces;

namespace QuantityMeasurementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 1. Create Repo
            ICacheRepository cacheRepo = new InMemoryCacheRepository();
            
            // 2. Inject Repo into Service
            IQuantityMeasurementService service = new QuantityMeasurementService(cacheRepo);
            
            // 3. Inject Service into Controller
            QuantityMeasurementController controller = new QuantityMeasurementController(service);

            // 4. Run App
            controller.Run();
        }
    }
}