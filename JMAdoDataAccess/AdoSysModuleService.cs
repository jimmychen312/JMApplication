using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMDataServiceInterface;
using System.Data.SqlClient;
using JMModels;

namespace JMAdoDataAccess
{
      public class AdoSysModuleService : AdoDataService, ISysModuleDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AdoSysModuleService()
        {

        }
         

        /// <summary>
        /// Get SysModule By SysModule ID
        /// </summary>
        /// <param name="sysModuleID"></param>
        /// <returns></returns>
        public SysModule GetSysModuleBySysModuleID(string sysModuleID)
        {

            SysModule sysModule = new SysModule();
            //SELECT Id, Name, EnglishName, ParentId, Url, Iconic, Sort, Remark, State, CreatePerson, CreateTime, IsLast, Version FROM SysModule
            string sql = "SELECT * FROM SysModule WHERE Id = '" + sysModuleID.ToString() + "'";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                sysModule.Id = dataReader.GetString("Id");
                sysModule.Name = dataReader.GetString("Name");
                sysModule.EnglishName = dataReader.GetString("EnglishName");
                sysModule.ParentId = dataReader.GetString("ParentId");
                sysModule.Iconic = dataReader.GetString("Iconic");
                sysModule.Sort = dataReader.GetInt32("Sort");
                sysModule.Remark = dataReader.GetString("Remark");
                sysModule.State = dataReader.GetBoolean("State");
                sysModule.CreatePerson = dataReader.GetString("CreatePerson");
                sysModule.CreateTime = dataReader.GetDateTime("CreateTime");
                sysModule.IsLast = dataReader.GetBoolean("IsLast");
                
            }
            reader.Close();

            return sysModule;
        }



        /// <summary>
        /// Get SysModuleInquiry
        /// </summary>
        /// <returns></returns>
        public List<SysModuleInquiry> SysModuleInquiry()
        {
            List<SysModuleInquiry> sysModuleList = new List<SysModuleInquiry>();

            string sql = "SELECT Id, Name, EnglishName, ParentId, Url FROM SysModule ORDER BY Id";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);
                                
                SysModuleInquiry sysModule = new SysModuleInquiry();

                sysModule.Id = dataReader.GetString("Id");
                sysModule.Name = dataReader.GetString("Name");
                sysModule.EnglishName = dataReader.GetString("EnglishName");
                sysModule.ParentId = dataReader.GetString("ParentId");
                sysModule.Url = dataReader.GetString("Url");
                sysModuleList.Add(sysModule);

            }
            reader.Close();
            return sysModuleList;

        }


        /// <summary>
        /// Get SysModuleInquiry By sysModule Id
        /// </summary>
        /// <returns></returns>
        public List<SysModuleInquiry> SysModuleInquiry(string ParentId)
        {
            List<SysModuleInquiry> sysModuleList = new List<SysModuleInquiry>();

            string sql = "SELECT * FROM SysModule Where ParentId ='" + ParentId + "' AND Id <> '0' ORDER BY Id";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                SysModuleInquiry sysModule = new SysModuleInquiry();

                sysModule.Id = dataReader.GetString("Id");
                sysModule.Name = dataReader.GetString("Name");
                sysModule.EnglishName = dataReader.GetString("EnglishName");
                sysModule.ParentId = dataReader.GetString("ParentId");
                sysModule.Url = dataReader.GetString("Url");
                sysModule.Iconic = dataReader.GetString("Iconic");
                sysModule.Sort = dataReader.GetInt32("Sort");
                sysModule.Remark = dataReader.GetString("Remark");
                sysModule.State = dataReader.GetBoolean("State");
                sysModule.CreatePerson = dataReader.GetString("CreatePerson");
                sysModule.CreateTime = dataReader.GetDateTime("CreateTime");
                sysModule.IsLast = dataReader.GetBoolean("IsLast");
                sysModuleList.Add(sysModule);

            }

            reader.Close();
            return sysModuleList;

        }


    }

}
