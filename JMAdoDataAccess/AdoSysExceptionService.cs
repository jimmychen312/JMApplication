using JMDataServiceInterface;
using JMModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMAdoDataAccess
{
    public class AdoSysExceptionService : AdoDataService, ISysExceptionDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AdoSysExceptionService()
        {

        }
        
        /// <summary>
        /// SysException Inquiry
        /// </summary>
        /// <returns></returns>
        public List<SysExceptionInquiry> SysExceptionInquiry(string queryStr, DataGridPagingInformation paging)
        {
            List<SysException> sysExceptions = new List<SysException>();

            string sortExpression = paging.SortExpression;

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

            sqlBuilder.Append(" SELECT COUNT(*) as total_records FROM SysException c ");
            sqlBuilder.Append(sqlWhere);
            sqlBuilder.Append(";");
            sqlBuilder.Append(" SELECT* FROM(");
            sqlBuilder.Append(" SELECT (ROW_NUMBER() OVER (ORDER BY " + paging.SortExpression + " " + paging.SortDirection + ")) as record_number,");
            sqlBuilder.Append(" c.*");
            sqlBuilder.Append(" FROM SysException c");
            sqlBuilder.Append(sqlWhere);
            sqlBuilder.Append(" ) Rows");
            sqlBuilder.Append(" where record_number between " + minRowNumber + " and " + maxRowNumber);

            string sql = sqlBuilder.ToString();
                        
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

            List<SysExceptionInquiry> sysExceptionList = new List<SysExceptionInquiry>();
                        
            while (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                SysExceptionInquiry sysException = new SysExceptionInquiry();

                sysException.Id = dataReader.GetString("Id");
                sysException.HelpLink = dataReader.GetString("HelpLink");
                sysException.Message = dataReader.GetString("Message");
                sysException.Source = dataReader.GetString("Source");
                sysException.StackTrace = dataReader.GetString("StackTrace");
                sysException.TargetSite = dataReader.GetString("TargetSite");
                sysException.Data = dataReader.GetString("Data");
                sysException.CreateTime = dataReader.GetDateTime("CreateTime");

                sysExceptionList.Add(sysException);

            }
            reader.Close();
            return sysExceptionList;

        }
        


        /// <summary>
        /// Create SysException
        /// </summary>
        /// <param name="sysException"></param>
        public void CreateSysException(SysException sysException)
        {

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.Transaction = dbTransaction;

            DateTime dateCreated = System.DateTime.Now;
                        
            sysException.CreateTime = dateCreated;

            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append(" INSERT INTO SysException (Id, HelpLink, Message, Source, StackTrace, TargetSite, Data, CreateTime)");
            sqlBuilder.Append(" VALUES ( @Id, @HelpLink, @Message, @Source, @StackTrace, @TargetSite, @Data, @CreateTime)");
           
            sqlCommand.CommandText = sqlBuilder.ToString();
            sqlCommand.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@HelpLink", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Message", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Source", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@StackTrace", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@TargetSite", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Data", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@CreateTime", System.Data.SqlDbType.DateTime);
      
            sqlCommand.Parameters["@Id"].Value = Utilities.GetString(sysException.Id);
            sqlCommand.Parameters["@HelpLink"].Value = Utilities.GetString(sysException.HelpLink);
            sqlCommand.Parameters["@Message"].Value = Utilities.GetString(sysException.Message);
            sqlCommand.Parameters["@Source"].Value = Utilities.GetString(sysException.Source);
            sqlCommand.Parameters["@StackTrace"].Value = Utilities.GetString(sysException.StackTrace);
            sqlCommand.Parameters["@TargetSite"].Value = Utilities.GetString(sysException.TargetSite);
            sqlCommand.Parameters["@Data"].Value = Utilities.GetString(sysException.Data);
            sqlCommand.Parameters["@CreateTime"].Value = sysException.CreateTime;
            
            sqlCommand.ExecuteNonQuery();

        }
                

        /// <summary>
        /// Delete All SysExceptions
        /// </summary>
        public void DeleteSysExceptionById(string Id)
        {
            SqlCommand sqlCommand;
            string sql = string.Empty;

            sql = "DELETE FROM SysException Where Id ='"+Id.ToString()+"'";

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.Transaction = dbTransaction;
            sqlCommand.CommandText = sql;
            sqlCommand.ExecuteNonQuery();

        }

         

        /// <summary>
        /// Get SysException By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SysException GetSysExceptionById(string Id)
        {

            SysException sysException = new SysException();
           
            string sql = "SELECT * FROM SysException WHERE Id = '" + Id.ToString() + "'";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                sysException.Id = dataReader.GetString("Id");
                sysException.HelpLink = dataReader.GetString("HelpLink");
                sysException.Message = dataReader.GetString("Message");
                sysException.Source = dataReader.GetString("Source");
                sysException.StackTrace = dataReader.GetString("StackTrace");
                sysException.TargetSite = dataReader.GetString("TargetSite");
                sysException.Data = dataReader.GetString("Data");
                sysException.CreateTime = dataReader.GetDateTime("CreateTime");
                
            }
            reader.Close();

            return sysException;
        }
        
        //public void Dispose()
        //{

        //}
    }
         

 

}
