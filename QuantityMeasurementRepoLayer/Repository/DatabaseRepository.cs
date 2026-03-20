using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementRepoLayer.Interfaces;

namespace QuantityMeasurementRepoLayer
{
    public class DatabaseRepository : IDatabaseRepository 
    {
        private readonly string _connectionString;

        public DatabaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveToDatabase(CacheRecordDto record)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO MeasurementHistory (OperationType, InputDetails, Result, Timestamp) 
                                VALUES (@OpType, @Input, @Result, @Time)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OpType", record.OperationType);
                    cmd.Parameters.AddWithValue("@Input", record.InputDetails);
                    cmd.Parameters.AddWithValue("@Result", record.Result);
                    cmd.Parameters.AddWithValue("@Time", record.Timestamp);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<CacheRecordDto> GetAllFromDatabase()
        {
            var history = new List<CacheRecordDto>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT OperationType, InputDetails, Result, Timestamp FROM MeasurementHistory ORDER BY Timestamp DESC";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            history.Add(new CacheRecordDto
                            {
                                OperationType = reader.GetString(0),
                                InputDetails = reader.GetString(1),
                                Result = reader.GetString(2),
                                Timestamp = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            return history;
        }

        public void ClearDatabase()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM MeasurementHistory";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}