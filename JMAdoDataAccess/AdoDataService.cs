using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMDataServiceInterface;
using System.Data.SqlClient;

namespace JMAdoDataAccess
{
    public class AdoDataService : IDataService, IDisposable
    {

        SqlConnection _connection;
        SqlTransaction _transaction;

        public SqlConnection dbConnection
        {
            get { return _connection; }
        }

        public SqlTransaction dbTransaction
        {
            get { return _transaction; }
        }

        public void CommitTransaction(Boolean closeSession)
        {
            _transaction.Commit();
            if (closeSession == true) dbConnection.Close();
        } 

        public void RollbackTransaction(Boolean closeSession)
        {
            _transaction.Rollback();
            if (closeSession == true) dbConnection.Close();
        }

        public void Save(object entity) { }

        public void CreateSession()
        {

            string connectionString = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["JMDatabase"]);

            _connection = new SqlConnection();
            _connection.ConnectionString = connectionString;
            _connection.Open();

        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void CloseSession()
        {
            _connection.Close();
        }

        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();
        }

    }

}
