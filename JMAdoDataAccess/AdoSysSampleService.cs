﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMDataServiceInterface;
using System.Data.SqlClient;
using JMModels;

namespace JMAdoDataAccess
{
      public class AdoSysSampleService : AdoDataService, ISysSampleDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AdoSysSampleService()
        {

        }


        /// <summary>
        /// Create SysSample
        /// </summary>
        /// <param name="sysSample"></param>
        public void CreateSysSample(SysSample sysSample)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.Transaction = dbTransaction;

            DateTime dateCreated = System.DateTime.Now;

            sysSample.CreateTime = dateCreated;

            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append(" INSERT INTO SysSample (Id, Name,Age, Bir,Photo, Note, CreateTime)");
            sqlBuilder.Append(" VALUES ( @Id, @Name, @Age, @Bir,@Photo, @Note, @CreateTime)");

            sqlCommand.CommandText = sqlBuilder.ToString();
            sqlCommand.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Age", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Bir", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Photo", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Note", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@CreateTime", System.Data.SqlDbType.DateTime);

            sqlCommand.Parameters["@Id"].Value = Utilities.GetString(sysSample.Id);
            sqlCommand.Parameters["@Name"].Value = Utilities.GetString(sysSample.Name);
            sqlCommand.Parameters["@Age"].Value = Utilities.GetString(sysSample.Age);
            sqlCommand.Parameters["@Bir"].Value = Utilities.GetString(sysSample.Bir);
            sqlCommand.Parameters["@Photo"].Value = Utilities.GetString(sysSample.Photo);
            sqlCommand.Parameters["@Note"].Value = Utilities.GetString(sysSample.Note);
            sqlCommand.Parameters["@CreateTime"].Value = sysSample.CreateTime;

            sqlCommand.ExecuteNonQuery();

        }


        /// <summary>
        /// Update SysSample
        /// </summary>
        /// <param name="sysSample"></param>
        public void UpdateSysSample(SysSample sysSample)
        {
            DateTime dateUpdated = System.DateTime.Now;
            sysSample.CreateTime = dateUpdated;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.Transaction = dbTransaction;

            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append(" UPDATE SysSample SET Name=@Name, Age=@Age, Bir=@Bir,");
            sqlBuilder.Append(" Photo=@Photo, Note=@Note, CreateTime=@CreateTime");            
            sqlBuilder.Append(" WHERE Id = @Id");

            sqlCommand.CommandText = sqlBuilder.ToString();

            sqlCommand.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Age", System.Data.SqlDbType.Int);
            sqlCommand.Parameters.Add("@Bir", System.Data.SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@Photo", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Note", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@CreateTime", System.Data.SqlDbType.DateTime);
          
            sqlCommand.Parameters["@Id"].Value = sysSample.Id;
            sqlCommand.Parameters["@Name"].Value = Utilities.GetString(sysSample.Name);
            sqlCommand.Parameters["@Age"].Value =sysSample.Age;
            sqlCommand.Parameters["@Bir"].Value =sysSample.Bir;
            sqlCommand.Parameters["@Photo"].Value = Utilities.GetString(sysSample.Photo);
            sqlCommand.Parameters["@Note"].Value = Utilities.GetString(sysSample.Note);
            sqlCommand.Parameters["@CreateTime"].Value = sysSample.CreateTime;
            
            sqlCommand.ExecuteNonQuery();

        }

        /// <summary>
        /// Get SysSample By SysSample ID
        /// </summary>
        /// <param name="sysSampleID"></param>
        /// <returns></returns>
        public SysSample GetSysSampleById(string sysSampleID)
        {

            SysSample sysSample = new SysSample();
            
            string sql = "SELECT * FROM SysSample WHERE Id = '" + sysSampleID.ToString() + "'";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                sysSample.Id = dataReader.GetString("Id");
                sysSample.Name = dataReader.GetString("Name");
                sysSample.Age = dataReader.GetString("Age");
                sysSample.Bir = dataReader.GetString("Bir");
                sysSample.Photo = dataReader.GetString("Photo");
                sysSample.Note = dataReader.GetString("Note");
                sysSample.CreateTime = dataReader.GetDateTime("CreateTime");
                               
            }
            reader.Close();

            return sysSample;
        }
        

        /// <summary>
        /// Get SysSampleInquiry
        /// </summary>
        /// <returns></returns>
        public List<SysSampleInquiry> SysSampleInquiry(string queryStr ,DataGridPagingInformation paging)
        {
            List<SysSample> sysSamples = new List<SysSample>();

            //string sortExpression = paging.SortExpression;

            int maxRowNumber;
            int minRowNumber;

            minRowNumber = (paging.PageSize * (paging.CurrentPageNumber - 1)) + 1;
            maxRowNumber = paging.PageSize * paging.CurrentPageNumber;

            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder sqlWhereBuilder = new StringBuilder();

            string sqlWhere = string.Empty;

            if (queryStr != null && queryStr.Trim().Length > 0)
                sqlWhereBuilder.Append(" c.Id LIKE @Id AND ");
                        

            if (sqlWhereBuilder.Length > 0)
                sqlWhere = " WHERE " + sqlWhereBuilder.ToString().Substring(0, sqlWhereBuilder.Length - 4);

            sqlBuilder.Append(" SELECT COUNT(*) as total_records FROM SysSample c ");
            sqlBuilder.Append(sqlWhere);
            sqlBuilder.Append(";");
            sqlBuilder.Append(" SELECT* FROM(");
            sqlBuilder.Append(" SELECT (ROW_NUMBER() OVER (ORDER BY " + paging.SortExpression + " " + paging.SortDirection + ")) as record_number,");
            sqlBuilder.Append(" c.*");
            sqlBuilder.Append(" FROM SysSample c");
            sqlBuilder.Append(sqlWhere);
            sqlBuilder.Append(" ) Rows");
            sqlBuilder.Append(" where record_number between " + minRowNumber + " and " + maxRowNumber);

            string sql = sqlBuilder.ToString();
                        
            //string sql = "SELECT * FROM SysSample ORDER BY Id";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            if (queryStr != null && queryStr.Trim().Length > 0)
            {
                sqlCommand.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar);
                sqlCommand.Parameters["@Id"].Value = queryStr + "%";
            }
            
            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            paging.TotalRows = Convert.ToInt32(reader["Total_Records"]);
            paging.TotalPages = Utilities.CalculateTotalPages(paging.TotalRows, paging.PageSize);

            reader.NextResult();

            List <SysSampleInquiry> sysSampleList = new List<SysSampleInquiry>();
            
            //SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);
                                
                SysSampleInquiry sysSample = new SysSampleInquiry();

                sysSample.Id = dataReader.GetString("Id");
                sysSample.Name = dataReader.GetString("Name");
                sysSample.Age = dataReader.GetString("Age");
                sysSample.Bir = dataReader.GetString("Bir");
                sysSample.Photo = dataReader.GetString("Photo");
                sysSample.Note = dataReader.GetString("Note");
                sysSample.CreateTime = dataReader.GetDateTime("CreateTime");

                sysSampleList.Add(sysSample);

            }
            reader.Close();
            return sysSampleList;
            
        }

        /// <summary>
        /// Delete SysSample By Id
        /// </summary>
        public void DeleteSysSampleById(string Id)
        {
            SqlCommand sqlCommand;
            string sql = string.Empty;

            sql = "DELETE FROM SysSample Where Id ='" + Id.ToString() + "'";

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.Transaction = dbTransaction;
            sqlCommand.CommandText = sql;
            sqlCommand.ExecuteNonQuery();

        }
        
    }

}
