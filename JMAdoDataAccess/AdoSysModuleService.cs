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
         

        ///// <summary>
        ///// Get SysModule By SysModule ID
        ///// </summary>
        ///// <param name="sysModuleID"></param>
        ///// <returns></returns>
        //public SysModule GetSysModuleBySysModuleID(string personId, string sysModuleID)
        //{

        //    SysModule sysModule = new SysModule();
        //    //SELECT Id, Name, EnglishName, ParentId, Url, Iconic, Sort, Remark, State, CreatePerson, CreateTime, IsLast, Version FROM SysModule
        //    string sql = "SELECT * FROM SysModule WHERE Id = '" + sysModuleID.ToString() + "'";

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.CommandText = sql;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        DataReader dataReader = new DataReader(reader);

        //        sysModule.Id = dataReader.GetString("Id");
        //        sysModule.Name = dataReader.GetString("Name");
        //        sysModule.EnglishName = dataReader.GetString("EnglishName");
        //        sysModule.ParentId = dataReader.GetString("ParentId");
        //        sysModule.Iconic = dataReader.GetString("Iconic");
        //        sysModule.Sort = dataReader.GetInt32("Sort");
        //        sysModule.Remark = dataReader.GetString("Remark");
        //        sysModule.State = dataReader.GetBoolean("State");
        //        sysModule.CreatePerson = dataReader.GetString("CreatePerson");
        //        sysModule.CreateTime = dataReader.GetDateTime("CreateTime");
        //        sysModule.IsLast = dataReader.GetBoolean("IsLast");
                
        //    }
        //    reader.Close();

        //    return sysModule;
        //}
        

        ///// <summary>
        ///// Get SysModuleInquiry
        ///// </summary>
        ///// <returns></returns>
        //public List<SysModuleInquiry> SysModuleInquiry(string personId)
        //{
        //    List<SysModuleInquiry> sysModuleList = new List<SysModuleInquiry>();

        //    //string sql = "SELECT Id, Name, EnglishName, ParentId, Url FROM SysModule ORDER BY Id";

        //    StringBuilder sqlBuilder = new StringBuilder();

        //    sqlBuilder.Append(" SELECT SysModule.Id, SysModule.Name, SysModule.EnglishName, SysModule.ParentId, SysModule.Url, SysModule.Iconic, ");
        //    sqlBuilder.Append(" SysModule.Sort, SysModule.Remark, SysModule.State, SysModule.CreatePerson, SysModule.CreateTime,  ");
        //    sqlBuilder.Append(" SysModule.IsLast, SysModule.Version ");
        //    sqlBuilder.Append(" FROM SysModule INNER JOIN ");
        //    sqlBuilder.Append(" SysRight ON SysModule.Id = SysRight.ModuleId INNER JOIN ");
        //    sqlBuilder.Append(" SysRole ON SysRight.RoleId = SysRole.Id INNER JOIN ");
        //    sqlBuilder.Append(" SysRoleSysUser ON SysRole.Id = SysRoleSysUser.SysRoleId INNER JOIN ");
        //    sqlBuilder.Append(" SysUser ON SysRoleSysUser.SysUserId = SysUser.Id ");
        //    sqlBuilder.Append(" WHERE(SysUser.Id =@Id) AND(SysRight.Rightflag = 'True') AND(SysModule.Id <> '0') ");
        //    sqlBuilder.Append(" ORDER BY SysModule.Sort ");

        //    string sql = sqlBuilder.ToString();

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.CommandText = sql;

        //    sqlCommand.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters["@Id"].Value = personId ;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        DataReader dataReader = new DataReader(reader);
                                
        //        SysModuleInquiry sysModule = new SysModuleInquiry();

        //        sysModule.Id = dataReader.GetString("Id");
        //        sysModule.Name = dataReader.GetString("Name");
        //        sysModule.EnglishName = dataReader.GetString("EnglishName");
        //        sysModule.ParentId = dataReader.GetString("ParentId");
        //        sysModule.Url = dataReader.GetString("Url");
        //        sysModuleList.Add(sysModule);

        //    }
        //    reader.Close();
        //    return sysModuleList;
        //}


        /// <summary>
        /// Get SysModuleInquiry By sysModule Id
        /// </summary>
        /// <returns></returns>
        public List<SysModuleInquiry> GetMenuByPersonId(string personId,string ParentId)
        {
            List<SysModuleInquiry> sysModuleList = new List<SysModuleInquiry>();

            //string sql = "SELECT * FROM SysModule Where ParentId ='" + ParentId + "' AND Id <> '0' ORDER BY Id";

            //SqlCommand sqlCommand = new SqlCommand();
            //sqlCommand.Connection = dbConnection;
            //sqlCommand.CommandText = sql;

            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append(" SELECT SysModule.Id, SysModule.Name, SysModule.EnglishName, SysModule.ParentId, SysModule.Url, SysModule.Iconic, ");
            sqlBuilder.Append(" SysModule.Sort, SysModule.Remark, SysModule.State, SysModule.CreatePerson, SysModule.CreateTime,  ");
            sqlBuilder.Append(" SysModule.IsLast, SysModule.Version ");
            sqlBuilder.Append(" FROM SysModule INNER JOIN ");
            sqlBuilder.Append(" SysRight ON SysModule.Id = SysRight.ModuleId INNER JOIN ");
            sqlBuilder.Append(" SysRole ON SysRight.RoleId = SysRole.Id INNER JOIN ");
            sqlBuilder.Append(" SysRoleSysUser ON SysRole.Id = SysRoleSysUser.SysRoleId INNER JOIN ");
            sqlBuilder.Append(" SysUser ON SysRoleSysUser.SysUserId = SysUser.Id ");
            sqlBuilder.Append(" WHERE(SysUser.Id =@Id) AND(SysRight.Rightflag = 'True') AND (SysModule.ParentId = @ParentId) AND(SysModule.Id <> '0') ");
            sqlBuilder.Append(" ORDER BY SysModule.Sort ");

            string sql = sqlBuilder.ToString();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            sqlCommand.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters["@Id"].Value = personId;
            sqlCommand.Parameters.Add("@ParentId", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters["@ParentId"].Value = ParentId;

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
