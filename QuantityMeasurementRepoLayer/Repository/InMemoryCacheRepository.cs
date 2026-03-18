using System.Collections.Generic;
using QuantityMeasurementRepoLayer.Interfaces;
using QuantityMeasurementModelLayer.DTOs;

namespace QuantityMeasurementRepoLayer.Repositories
{
    public class InMemoryCacheRepository : ICacheRepository
    {
        private readonly List<CacheRecordDto> _cache = new List<CacheRecordDto>();

        public void SaveToCache(CacheRecordDto record)
        {
            _cache.Add(record);
        }

        public IEnumerable<CacheRecordDto> GetAllHistory()
        {
            return _cache.AsReadOnly();
        }

        public void ClearCache()
        {
            _cache.Clear();
        }
    }
}