using System;
using System.Data.SqlClient;

namespace VentaPro.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;


        protected SqlConnection GetConnection()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la conexión: " + ex.Message);
            }
        }
    }
}