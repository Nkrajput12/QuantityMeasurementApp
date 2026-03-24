using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementRepoLayer.Interfaces;
using QuantityMeasurementRepoLayer.Data;
using Microsoft.Extensions.Configuration;
using QuantityMeasurementRepoLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace QuantityMeasurementRepoLayer
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly MeasurementDbContext context;

        public DatabaseRepository(MeasurementDbContext cont)
        {
            context = cont;
        }

        public void SaveToDatabase(CacheRecordDto record)
        {
            var newEntry = new MeasurementHistory
            {
                OperationType = record.OperationType,
                InputDetails = record.InputDetails,
                Result = record.Result,
                Timestamp = record.Timestamp
            };

            context.MeasurementHistories.Add(newEntry);
            context.SaveChanges();
        }

        public IEnumerable<CacheRecordDto> GetAllFromDatabase()
        {
            var list = context.MeasurementHistories.OrderByDescending(m => m.Timestamp).Select(m => new CacheRecordDto
            {
                OperationType = m.OperationType,
                InputDetails = m.InputDetails,
                Result = m.Result,
                Timestamp = m.Timestamp
            }).ToList();

            return list;
        }

        public void ClearDatabase()
        {
            context.MeasurementHistories.ExecuteDelete();
        }
    }
}