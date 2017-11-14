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
      public class AdoAccountService : AdoDataService, IAccountDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AdoAccountService()
        {

        }


        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public SysUser Login(string username, string pwd)
        {
            SysUser sysUser = new SysUser();
            string sql = "SELECT * FROM SysUser WHERE UserName = '" + username.ToString() + "' and Password ='"+ pwd.ToString() +"'";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                sysUser.UserName = dataReader.GetString("UserName");
                sysUser.Id = dataReader.GetString("Id");
                sysUser.TrueName = dataReader.GetString("TrueName");
                sysUser.State = dataReader.GetBoolean("State");
                
            }
            reader.Close();

            return sysUser;
        }

        ///// <summary>
        ///// Get Payment Type
        ///// </summary>
        ///// <param name="paymentTypeID"></param>
        ///// <returns></returns>
        //public PaymentType GetPaymentType(Guid paymentTypeID)
        //{
        //    PaymentType paymentType = new PaymentType();

        //    string sql = "SELECT * FROM PaymentTypes WHERE PaymentTypeID = '" + paymentTypeID.ToString() + "'";

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.CommandText = sql;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        DataReader dataReader = new DataReader(reader);

        //        paymentType.PaymentTypeID = dataReader.GetGuid("PaymentTypeID");
        //        paymentType.Description = dataReader.GetString("Description");
        //        paymentType.RequiresCreditCard = dataReader.GetInt32("RequiresCreditCard");

        //    }
        //    reader.Close();

        //    return paymentType;

        //}

        ///// <summary>
        ///// Account Inquiry
        ///// </summary>
        ///// <param name="firstName"></param>
        ///// <param name="lastName"></param>
        ///// <param name="paging"></param>
        ///// <returns></returns>
        //public List<AccountInquiry> AccountInquiry(string firstName, string lastName, DataGridPagingInformation paging)
        //{

        //    List<Account> accounts = new List<Account>();

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

        //    sqlBuilder.Append(" SELECT COUNT(*) as total_records FROM Accounts c ");
        //    sqlBuilder.Append(sqlWhere);
        //    sqlBuilder.Append(";");
        //    sqlBuilder.Append(" SELECT * FROM ( ");
        //    sqlBuilder.Append(" SELECT (ROW_NUMBER() OVER (ORDER BY " + paging.SortExpression + " " + paging.SortDirection + ")) as record_number, ");
        //    sqlBuilder.Append(" c.*, p.Description as PaymentTypeDescription ");
        //    sqlBuilder.Append(" FROM Accounts c ");
        //    sqlBuilder.Append(" INNER JOIN PaymentTypes p ON p.PaymentTypeID = c.PaymentTypeID ");
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

        //    List<AccountInquiry> accountList = new List<AccountInquiry>();

        //    while (reader.Read())
        //    {
        //        AccountInquiry account = new AccountInquiry();

        //        DataReader dataReader = new DataReader(reader);

        //        account.AccountID = dataReader.GetGuid("AccountID");
        //        account.FirstName = dataReader.GetString("FirstName");
        //        account.LastName = dataReader.GetString("LastName");
        //        account.EmailAddress = dataReader.GetString("EmailAddress");
        //        account.City = dataReader.GetString("City");
        //        account.Country = dataReader.GetString("Country");
        //        account.PaymentTypeDescription = dataReader.GetString("PaymentTypeDescription");

        //        accountList.Add(account);

        //    }

        //    reader.Close();

        //    return accountList;


        //}

        ///// <summary>
        ///// Delete All Accounts
        ///// </summary>
        //public void DeleteAllAccounts()
        //{

        //    SqlCommand sqlCommand;
        //    string sql = string.Empty;

        //    sql = "DELETE FROM Accounts ";

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

        ///// <summary>
        ///// Get Payment Types
        ///// </summary>
        ///// <returns></returns>
        //public List<PaymentType> GetPaymentTypes()
        //{
        //    List<PaymentType> paymentTypes = new List<PaymentType>();

        //    string sql = "SELECT * FROM PaymentTypes ORDER BY Description";

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.CommandText = sql;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        DataReader dataReader = new DataReader(reader);

        //        PaymentType paymentType = new PaymentType();
        //        paymentType.PaymentTypeID = dataReader.GetGuid("PaymentTypeID");
        //        paymentType.Description = dataReader.GetString("Description");
        //        paymentType.RequiresCreditCard = dataReader.GetInt16("RequiredCreditCard");

        //        paymentTypes.Add(paymentType);

        //    }
        //    reader.Close();
        //    return paymentTypes;

        //}


    }

}
