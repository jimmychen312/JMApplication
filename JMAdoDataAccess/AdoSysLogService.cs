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
    public class AdoSysLogService : AdoDataService, ISysLogDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AdoSysLogService()
        {

        }



        /// <summary>
        /// SysLog Inquiry
        /// </summary>
        /// <returns></returns>
        public List<SysLogInquiry> SysLogInquiry(string queryStr, DataGridPagingInformation paging)
        {
            List<SysLog> sysLogs = new List<SysLog>();

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

            sqlBuilder.Append(" SELECT COUNT(*) as total_records FROM SysLog c ");
            sqlBuilder.Append(sqlWhere);
            sqlBuilder.Append(";");
            sqlBuilder.Append(" SELECT* FROM(");
            sqlBuilder.Append(" SELECT (ROW_NUMBER() OVER (ORDER BY " + paging.SortExpression + " " + paging.SortDirection + ")) as record_number,");
            sqlBuilder.Append(" c.*");
            sqlBuilder.Append(" FROM SysLog c");
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

            List<SysLogInquiry> sysLogList = new List<SysLogInquiry>();
                        
            while (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                SysLogInquiry sysLog = new SysLogInquiry();

                sysLog.Id = dataReader.GetString("Id");
                sysLog.Operator = dataReader.GetString("Operator");
                sysLog.Message = dataReader.GetString("Message");
                sysLog.Result = dataReader.GetString("Result");
                sysLog.Type = dataReader.GetString("Type");
                sysLog.Module = dataReader.GetString("Module");
                sysLog.CreateTime = dataReader.GetDateTime("CreateTime");

                sysLogList.Add(sysLog);

            }
            reader.Close();
            return sysLogList;

        }

        ///// <summary>
        ///// 获取集合
        ///// </summary>
        ///// <returns>集合</returns>
        ////public IQueryable<SysLog> GetList()
        //public List<SysLogList> GetSysLogList()

        //{
        //    List<SysLogList> sysLogList = new List<SysLogList>();

        //    string sql = "SELECT * FROM SysLog ORDER BY Id";

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.CommandText = sql;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        DataReader dataReader = new DataReader(reader);

        //        SysLogList sysLog = new SysLogList();

        //        sysLog.Id = dataReader.GetString("Id");
        //        sysLog.Operator = dataReader.GetString("Operator");
        //        sysLog.Message = dataReader.GetString("Message");
        //        sysLog.Result = dataReader.GetString("Result");
        //        sysLog.Type = dataReader.GetString("Type");
        //        sysLog.Module = dataReader.GetString("Module");
        //        sysLog.CreateTime = dataReader.GetDateTime("CreateTime");
        //        sysLogList.Add(sysLog);

        //    }
        //    reader.Close();
        //    return sysLogList;

        //    //IQueryable<SysLog> list = db.SysLog.AsQueryable();
        //    //return list;
        //}

        ///// <summary>
        ///// 创建一个对象
        ///// </summary>
        ///// <param name="db">数据库</param>
        ///// <param name="entity">实体</param>
        //public int Create(SysLog entity)
        //{
        //    using (DBContainer db = new DBContainer())
        //    {
        //        db.SysLog.AddObject(entity);
        //        return db.SaveChanges();
        //    }

        //}

         


        /// <summary>
        /// Create SysLog
        /// </summary>
        /// <param name="sysLog"></param>
        public void CreateSysLog(SysLog sysLog)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.Transaction = dbTransaction;

            DateTime dateCreated = System.DateTime.Now;
                        
            sysLog.CreateTime = dateCreated;

            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append(" INSERT INTO SysLog (Id, Operator,Message, Result,Type, Module, CreateTime)");
            sqlBuilder.Append(" VALUES ( @Id, @Operator, @Message, @Result,@Type, @Module, @CreateTime)");
           
            sqlCommand.CommandText = sqlBuilder.ToString();
            sqlCommand.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Operator", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Message", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Result", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Type", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@Module", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@CreateTime", System.Data.SqlDbType.DateTime);
      
            sqlCommand.Parameters["@Id"].Value = Utilities.GetString(sysLog.Id);
            sqlCommand.Parameters["@Operator"].Value = Utilities.GetString(sysLog.Operator);
            sqlCommand.Parameters["@Message"].Value = Utilities.GetString(sysLog.Message);
            sqlCommand.Parameters["@Result"].Value = Utilities.GetString(sysLog.Result);
            sqlCommand.Parameters["@Type"].Value = Utilities.GetString(sysLog.Type);
            sqlCommand.Parameters["@Module"].Value = Utilities.GetString(sysLog.Module);
            sqlCommand.Parameters["@CreateTime"].Value = sysLog.CreateTime;
            
            sqlCommand.ExecuteNonQuery();

        }


        ///// <summary>
        ///// 删除对象集合
        ///// </summary>
        ///// <param name="db">数据库</param>
        ///// <param name="deleteCollection">集合</param>
        //public void Delete(DBContainer db, string[] deleteCollection)
        //{
        //    IQueryable<SysLog> collection = from f in db.SysLog
        //                                    where deleteCollection.Contains(f.Id)
        //                                    select f;
        //    foreach (var deleteItem in collection)
        //    {
        //        db.SysLog.DeleteObject(deleteItem);
        //    }
        //}


        /// <summary>
        /// Delete All SysLogs
        /// </summary>
        public void DeleteSysLogById(string Id)
        {
            SqlCommand sqlCommand;
            string sql = string.Empty;

            sql = "DELETE FROM SysLog Where Id ='"+Id.ToString()+"'";

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.Transaction = dbTransaction;
            sqlCommand.CommandText = sql;
            sqlCommand.ExecuteNonQuery();

        }


        ///// <summary>
        ///// 根据ID获取一个实体
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public SysLog GetById(string id)
        //{
        //    using (DBContainer db = new DBContainer())
        //    {
        //        return db.SysLog.SingleOrDefault(a => a.Id == id);
        //    }
        //}


        /// <summary>
        /// Get SysLog By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SysLog GetSysLogById(string Id)
        {

            SysLog sysLog = new SysLog();
           
            string sql = "SELECT * FROM SysLog WHERE Id = '" + Id.ToString() + "'";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                sysLog.Id = dataReader.GetString("Id");
                sysLog.Operator = dataReader.GetString("Operator");
                sysLog.Message = dataReader.GetString("Message");
                sysLog.Result = dataReader.GetString("Result");
                sysLog.Type = dataReader.GetString("Type");
                sysLog.Module = dataReader.GetString("Module");
                sysLog.CreateTime = dataReader.GetDateTime("CreateTime");
                
            }
            reader.Close();

            return sysLog;
        }
        
        //public void Dispose()
        //{

        //}
    }
         

 

}
