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


        ///// <summary>
        ///// Get Payment Types
        ///// </summary>
        ///// <returns></returns>
        //public List<Permission> GetPermissions()
        //{
        //    List<Permission> permissions = new List<Permission>();

        //    string sql = "SELECT Id ,Name,KeyCode,IsValid FROM SysModuleOperate";

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.CommandText = sql;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        DataReader dataReader = new DataReader(reader);

        //        Permission permission = new Permission();
        //        //permission.Id = dataReader.GetString("Id");
        //        //permission.Name = dataReader.GetString("Name");
        //        permission.KeyCode = dataReader.GetString("KeyCode");
        //        permission.IsValid = dataReader.GetBoolean("IsValid");

        //        permissions.Add(permission);

        //    }
        //    reader.Close();
        //    return permissions;

        //}


        ///// <summary>
        ///// Get Payment Types
        ///// </summary>
        ///// <returns></returns>
        //public List<Permission> GetPermissions()
        //{
        //    List<Permission> permissions = new List<Permission>();

        //    StringBuilder sqlBuilder = new StringBuilder();

        //    sqlBuilder.Append  (" SELECT DISTINCT KeyCode, IsValid ");
        //    sqlBuilder.Append(" FROM SysRightOperate          ");
        //    sqlBuilder.Append(" WHERE(RightId IN");
        //    sqlBuilder.Append(" (SELECT   a.Id");
        //    sqlBuilder.Append(" FROM      SysRight AS a INNER JOIN");
        //    sqlBuilder.Append(" SysModule AS b ON a.ModuleId = b.Id");
        //    sqlBuilder.Append(" WHERE(a.RoleId IN");
        //    sqlBuilder.Append(" (SELECT   SysRoleId");
        //    sqlBuilder.Append(" FROM      SysRoleSysUser");
        //    sqlBuilder.Append(" WHERE(SysUserId = 'admin'))) AND(b.Url = 'SysSample'))) AND(IsValid = 1)");

        //    string sql = sqlBuilder.ToString();

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.CommandText = sql;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        DataReader dataReader = new DataReader(reader);

        //        Permission permission = new Permission();
        //        //permission.Id = dataReader.GetString("Id");
        //        //permission.Name = dataReader.GetString("Name");
        //        permission.KeyCode = dataReader.GetString("KeyCode");
        //        permission.IsValid = dataReader.GetBoolean("IsValid");

        //        permissions.Add(permission);

        //    }
        //    reader.Close();
        //    return permissions;

        //}




        /// <summary>
        /// Get Payment Types
        /// </summary>
        /// <returns></returns>
        public List<Permission> GetPermissions(string accountId, string controller)
        {
            List<Permission> permissions = new List<Permission>();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            string sql = "P_Sys_GetRightOperate";
            sqlCommand.CommandText = sql;
            sqlCommand.CommandType = CommandType.StoredProcedure;
           
            SqlParameter UserIdParameter = new SqlParameter("@userId", SqlDbType.NVarChar);
            UserIdParameter.Direction = System.Data.ParameterDirection.Input;//设置此项参数的类型为输入参数
            UserIdParameter.Value = accountId;//给输入参数赋值
            sqlCommand.Parameters.Add(UserIdParameter);

            SqlParameter UrlParameter = new SqlParameter("@url", SqlDbType.NVarChar);
            UrlParameter.Direction = System.Data.ParameterDirection.Input;//设置此项参数的类型为输入参数
            UrlParameter.Value = controller;//给输入参数赋值
            sqlCommand.Parameters.Add(UrlParameter);


            //sqlCommand.Parameters.Add("@userId", System.Data.SqlDbType.NVarChar);
            //sqlCommand.Parameters.Add("@url", System.Data.SqlDbType.NVarChar);
            //sqlCommand.Parameters["@userId"].Value = "admin";
            //sqlCommand.Parameters["@url"].Value = "SysSample";

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                Permission permission = new Permission();
                //permission.Id = dataReader.GetString("Id");
                //permission.Name = dataReader.GetString("Name");
                permission.KeyCode = dataReader.GetString("KeyCode");
                permission.IsValid = dataReader.GetBoolean("IsValid");

                permissions.Add(permission);

            }
            reader.Close();
            return permissions;

        }
        

        ///// <summary>
        ///// Get Payment Type
        ///// </summary>
        ///// <param name="paymentTypeID"></param>
        ///// <returns></returns>
        //public Permission GetPermission(Guid paymentTypeID)
        //{
        //    Permission paymentType = new Permission();

        //    string sql = "SELECT * FROM Permissions WHERE PermissionID = '" + paymentTypeID.ToString() + "'";

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.CommandText = sql;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        DataReader dataReader = new DataReader(reader);

        //        paymentType.PermissionID = dataReader.GetGuid("PermissionID");
        //        paymentType.Description = dataReader.GetString("Description");
        //        paymentType.RequiresCreditCard = dataReader.GetInt32("RequiresCreditCard");

        //    }
        //    reader.Close();

        //    return paymentType;

        //}

        ///// <summary>
        ///// SysRight Inquiry
        ///// </summary>
        ///// <param name="firstName"></param>
        ///// <param name="lastName"></param>
        ///// <param name="paging"></param>
        ///// <returns></returns>
        //public List<SysRightInquiry> SysRightInquiry(string firstName, string lastName, DataGridPagingInformation paging)
        //{

        //    List<SysRight> customers = new List<SysRight>();

        //    string sortExpression = paging.SortExpression;

        //    int maxRowNumber;
        //    int minRowNumber;

        //    minRowNumber = (paging.PageSize * (paging.CurrentPageNumber - 1)) + 1;
        //    maxRowNumber = paging.PageSize * paging.CurrentPageNumber;

        //    StringBuilder sqlBuilder = new StringBuilder();
        //    StringBuilder sqlWhereBuilder = new StringBuilder();

        //    string sqlWhere = string.Empty;

        //    if (firstName != null && firstName.Trim().Length > 0)
        //        sqlWhereBuilder.Append(" c.FirstName LIKE @FirstName AND ");

        //    if (lastName != null && lastName.Trim().Length > 0)
        //        sqlWhereBuilder.Append(" c.LastName LIKE @LastName AND ");

        //    if (sqlWhereBuilder.Length > 0)
        //        sqlWhere = " WHERE " + sqlWhereBuilder.ToString().Substring(0, sqlWhereBuilder.Length - 4);

        //    sqlBuilder.Append(" SELECT COUNT(*) as total_records FROM SysRights c ");
        //    sqlBuilder.Append(sqlWhere);
        //    sqlBuilder.Append(";");
        //    sqlBuilder.Append(" SELECT * FROM ( ");
        //    sqlBuilder.Append(" SELECT (ROW_NUMBER() OVER (ORDER BY " + paging.SortExpression + " " + paging.SortDirection + ")) as record_number, ");
        //    sqlBuilder.Append(" c.*, p.Description as PermissionDescription ");
        //    sqlBuilder.Append(" FROM SysRights c ");
        //    sqlBuilder.Append(" INNER JOIN Permissions p ON p.PermissionID = c.PermissionID ");
        //    sqlBuilder.Append(sqlWhere);
        //    sqlBuilder.Append(" ) Rows ");
        //    sqlBuilder.Append(" where record_number between " + minRowNumber + " and " + maxRowNumber);

        //    string sql = sqlBuilder.ToString();

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.CommandText = sql;
        //    sqlCommand.Connection = dbConnection;

        //    if (firstName != null && firstName.Trim().Length > 0)
        //    {
        //        sqlCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar);
        //        sqlCommand.Parameters["@FirstName"].Value = firstName + "%";
        //    }

        //    if (lastName != null && lastName.Trim().Length > 0)
        //    {
        //        sqlCommand.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar);
        //        sqlCommand.Parameters["@LastName"].Value = lastName + "%";
        //    }

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    reader.Read();
        //    paging.TotalRows = Convert.ToInt32(reader["Total_Records"]);
        //    paging.TotalPages = Utilities.CalculateTotalPages(paging.TotalRows, paging.PageSize);

        //    reader.NextResult();

        //    List<SysRightInquiry> customerList = new List<SysRightInquiry>();

        //    while (reader.Read())
        //    {
        //        SysRightInquiry customer = new SysRightInquiry();

        //        DataReader dataReader = new DataReader(reader);

        //        customer.SysRightID = dataReader.GetGuid("SysRightID");
        //        customer.FirstName = dataReader.GetString("FirstName");
        //        customer.LastName = dataReader.GetString("LastName");
        //        customer.EmailAddress = dataReader.GetString("EmailAddress");
        //        customer.City = dataReader.GetString("City");
        //        customer.Country = dataReader.GetString("Country");
        //        customer.PermissionDescription = dataReader.GetString("PermissionDescription");

        //        customerList.Add(customer);

        //    }

        //    reader.Close();

        //    return customerList;


        //}

        ///// <summary>
        ///// Delete All SysRights
        ///// </summary>
        //public void DeleteAllSysRights()
        //{

        //    SqlCommand sqlCommand;
        //    string sql = string.Empty;

        //    sql = "DELETE FROM SysRights ";

        //    sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.Transaction = dbTransaction;
        //    sqlCommand.CommandText = sql;
        //    sqlCommand.ExecuteNonQuery();

        //}

        ////收缩数据库
        //private void ShrinkDatabase()
        //{

        //    AdoDataService dataService = new AdoDataService();

        //    dataService.CreateSession();

        //    SqlCommand sqlCommand;
        //    string sql = string.Empty;

        //    sql = "DBCC SHRINKDATABASE (JMDatabase,10)";

        //    sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dataService.dbConnection;
        //    sqlCommand.CommandText = sql;
        //    sqlCommand.ExecuteNonQuery();

        //    dataService.CloseSession();
        //}



    }

}
