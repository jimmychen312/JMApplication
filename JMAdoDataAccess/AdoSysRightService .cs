using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMDataServiceInterface;
using System.Data.SqlClient;
using JMModels;
using System.Data;

namespace JMAdoDataAccess
{
      public class AdoSysRightService : AdoDataService, ISysRightDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AdoSysRightService()
        {

        }


        /// <summary>
        /// 取角色模块的操作权限，用于权限控制
        /// </summary>
        ///<param name="accountid"></param>
        ///<param name="controller"></param>
        /// <returns></returns>
        public List<Permission> GetPermission(string accountid, string controller)
        {
            List<Permission> sysRights = new List<Permission>();
            
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText  = "P_Sys_GetRightOperate";

            sqlCommand.Parameters.Add("@accountid", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters["@accountid"].Value = accountid;
            sqlCommand.Parameters.Add("@controller", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters["@controller"].Value = controller;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                Permission sysRight = new Permission();

                sysRight.KeyCode = dataReader.GetString("KeyCode");
                sysRight.IsValid = dataReader.GetBoolean("IsValid");

                sysRights.Add(sysRight);

            }

            reader.Close();
            return sysRights;

        }


    }

}
