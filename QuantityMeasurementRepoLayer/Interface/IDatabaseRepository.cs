using System.Collections.Generic;
using QuantityMeasurementModelLayer.DTOs;

namespace QuantityMeasurementRepoLayer.Interfaces;
public interface IDatabaseRepository
    {
        void SaveToDatabase(CacheRecordDto record);
        IEnumerable<CacheRecordDto> GetAllFromDatabase();
        void ClearDatabase();
    }