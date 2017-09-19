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
      public class AdoCustomerService : AdoDataService, ICustomerDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AdoCustomerService()
        {

        }

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="customer"></param>
        public void CreateCustomer(Customer customer)
        {

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.Transaction = dbTransaction;

            DateTime dateCreated = System.DateTime.Now;

            customer.CustomerID = Guid.NewGuid();
            customer.DateCreated = dateCreated;
            customer.DateUpdated = dateCreated;

            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append("INSERT INTO CUSTOMERS (CustomerID, FirstName,LastName, EmailAddress,");
            sqlBuilder.Append(" Address, City, Region, PostalCode, Country, PhoneNumber, CreditCardNumber,");
            sqlBuilder.Append(" PaymentTypeID, CreditCardExpirationDate, CreditCardSecurityCode, CreditLimit,");
            sqlBuilder.Append(" ApprovalStatus, DateCreated, DateUpdated) VALUES (");
            sqlBuilder.Append(" @CustomerID, @FirstName, @LastName, @EmailAddress,");
            sqlBuilder.Append(" @Address, @City, @Region, @PostalCode, @Country, @PhoneNumber, @CreditCardNumber,");
            sqlBuilder.Append(" @PaymentTypeID, @CreditCardExpirationDate, @CreditCardSecurityCode, @CreditLimit,");
            sqlBuilder.Append(" @ApprovalStatus, @DateCreated, @DateUpdated)");

            sqlCommand.CommandText = sqlBuilder.ToString();

            sqlCommand.Parameters.Add("@CustomerID", System.Data.SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@EmailAddress", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@Address", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@City", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@Region", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PostalCode", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@Country", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PhoneNumber", System.Data.SqlDbType.VarChar);
            
            sqlCommand.Parameters.Add("@CreditCardNumber", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PaymentTypeID", System.Data.SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CreditCardExpirationDate", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@CreditCardSecurityCode", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@CreditLimit", System.Data.SqlDbType.Decimal);
            sqlCommand.Parameters.Add("@ApprovalStatus", System.Data.SqlDbType.Int);
            sqlCommand.Parameters.Add("@DateCreated", System.Data.SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@DateUpdated", System.Data.SqlDbType.DateTime);

            sqlCommand.Parameters["@CustomerID"].Value = customer.CustomerID;

            sqlCommand.Parameters["@FirstName"].Value = Utilities.GetString(customer.FirstName);
            sqlCommand.Parameters["@LastName"].Value = Utilities.GetString(customer.LastName);
            sqlCommand.Parameters["@EmailAddress"].Value = Utilities.GetString(customer.EmailAddress);
            sqlCommand.Parameters["@Address"].Value = Utilities.GetString(customer.Address);
            sqlCommand.Parameters["@City"].Value = Utilities.GetString(customer.City);
            sqlCommand.Parameters["@Region"].Value = Utilities.GetString(customer.Region);
            sqlCommand.Parameters["@PostalCode"].Value = Utilities.GetString(customer.PostalCode);
            sqlCommand.Parameters["@Country"].Value = Utilities.GetString(customer.Country);
            sqlCommand.Parameters["@PhoneNumber"].Value = Utilities.GetString(customer.PhoneNumber);
            
            sqlCommand.Parameters["@CreditCardNumber"].Value = Utilities.GetString(customer.CreditCardNumber);
            sqlCommand.Parameters["@PaymentTypeID"].Value = customer.PaymentTypeID;

            if (customer.CreditCardExpirationDate != DateTime.MinValue && customer.CreditCardExpirationDate != null)
                sqlCommand.Parameters["@CreditCardExpirationDate"].Value = customer.CreditCardExpirationDate;
            else
                sqlCommand.Parameters["@CreditCardExpirationDate"].Value = DBNull.Value;

            sqlCommand.Parameters["@CreditCardSecurityCode"].Value = Utilities.GetString(customer.CreditCardSecurityCode);
            sqlCommand.Parameters["@CreditLimit"].Value = customer.CreditLimit;
            sqlCommand.Parameters["@ApprovalStatus"].Value = customer.ApprovalStatus;
            sqlCommand.Parameters["@DateCreated"].Value = customer.DateCreated;
            sqlCommand.Parameters["@DateUpdated"].Value = customer.DateUpdated;


            sqlCommand.ExecuteNonQuery();

        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateCustomer(Customer customer)
        {
            DateTime dateUpdated = System.DateTime.Now;
            customer.DateUpdated = dateUpdated;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.Transaction = dbTransaction;

            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append("UPDATE CUSTOMERS SET FirstName=@FirstName, LastName=@LastName, EmailAddress=@EmailAddress,");
            sqlBuilder.Append(" Address=@Address, City=@City, Region=@Region, PostalCode=@PostalCode, Country=@Country, PhoneNumber=@PhoneNumber, CreditCardNumber=@CreditCardNumber,");
            sqlBuilder.Append(" PaymentTypeID=@PaymentTypeID, CreditCardExpirationDate=@CreditCardExpirationDate, CreditCardSecurityCode=@CreditCardSecurityCode, CreditLimit=@CreditLimit,");
            sqlBuilder.Append(" ApprovalStatus=@ApprovalStatus, DateCreated=@DateCreated, DateUpdated=@DateUpdated ");
            sqlBuilder.Append(" WHERE CustomerID = @CustomerID");

            sqlCommand.CommandText = sqlBuilder.ToString();

            sqlCommand.Parameters.Add("@CustomerID", System.Data.SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@EmailAddress", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@Address", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@City", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@Region", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PostalCode", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@Country", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PhoneNumber", System.Data.SqlDbType.VarChar);
            
            sqlCommand.Parameters.Add("@CreditCardNumber", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PaymentTypeID", System.Data.SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CreditCardExpirationDate", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@CreditCardSecurityCode", System.Data.SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@CreditLimit", System.Data.SqlDbType.Decimal);
            sqlCommand.Parameters.Add("@ApprovalStatus", System.Data.SqlDbType.Int);
            sqlCommand.Parameters.Add("@DateCreated", System.Data.SqlDbType.DateTime2);
            sqlCommand.Parameters.Add("@DateUpdated", System.Data.SqlDbType.DateTime2);

            sqlCommand.Parameters["@CustomerID"].Value = customer.CustomerID;
            sqlCommand.Parameters["@FirstName"].Value = Utilities.GetString(customer.FirstName);
            sqlCommand.Parameters["@LastName"].Value = Utilities.GetString(customer.LastName);
            sqlCommand.Parameters["@EmailAddress"].Value = Utilities.GetString(customer.EmailAddress);
            sqlCommand.Parameters["@Address"].Value = Utilities.GetString(customer.Address);
            sqlCommand.Parameters["@City"].Value = Utilities.GetString(customer.City);
            sqlCommand.Parameters["@Region"].Value = Utilities.GetString(customer.Region);
            sqlCommand.Parameters["@PostalCode"].Value = Utilities.GetString(customer.PostalCode);
            sqlCommand.Parameters["@Country"].Value = Utilities.GetString(customer.Country);
            sqlCommand.Parameters["@PhoneNumber"].Value = Utilities.GetString(customer.PhoneNumber);
            
            sqlCommand.Parameters["@CreditCardNumber"].Value = Utilities.GetString(customer.CreditCardNumber);
            sqlCommand.Parameters["@PaymentTypeID"].Value = customer.PaymentTypeID;

            if (customer.CreditCardExpirationDate != DateTime.MinValue && customer.CreditCardExpirationDate != null)
                sqlCommand.Parameters["@CreditCardExpirationDate"].Value = customer.CreditCardExpirationDate;
            else
                sqlCommand.Parameters["@CreditCardExpirationDate"].Value = DBNull.Value;

            sqlCommand.Parameters["@CreditCardSecurityCode"].Value = Utilities.GetString(customer.CreditCardSecurityCode);
            sqlCommand.Parameters["@CreditLimit"].Value = customer.CreditLimit;
            sqlCommand.Parameters["@ApprovalStatus"].Value = customer.ApprovalStatus;
            sqlCommand.Parameters["@DateCreated"].Value = customer.DateCreated;
            sqlCommand.Parameters["@DateUpdated"].Value = customer.DateUpdated;

            sqlCommand.ExecuteNonQuery();

        }

        /// <summary>
        /// Get Customer By Customer ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer GetCustomerByCustomerID(Guid customerID)
        {

            Customer customer = new Customer();
            string sql = "SELECT * FROM CUSTOMERS WHERE CustomerID = '" + customerID.ToString() + "'";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                customer.CustomerID = dataReader.GetGuid("CustomerID");
                customer.FirstName = dataReader.GetString("FirstName");
                customer.LastName = dataReader.GetString("LastName");
                customer.EmailAddress = dataReader.GetString("EmailAddress");
                customer.Address = dataReader.GetString("Address");
                customer.City = dataReader.GetString("City");
                customer.Region = dataReader.GetString("Region");
                customer.PostalCode = dataReader.GetString("PostalCode");
                customer.Country = dataReader.GetString("Country");
                customer.PhoneNumber = dataReader.GetString("PhoneNumber");
                
                customer.CreditCardNumber = dataReader.GetString("CreditCardNumber");
                customer.PaymentTypeID = dataReader.GetGuid("PaymentTypeID");

                if (dataReader.GetDateTime("CreditCardExpirationDate") != DateTime.MinValue)
                    customer.CreditCardExpirationDate = dataReader.GetDateTime("CreditCardExpirationDate");

                customer.CreditCardSecurityCode = dataReader.GetString("CreditCardSecurityCode");
                customer.CreditLimit = dataReader.GetDecimal("CreditLimit");
                customer.DateApproved = dataReader.GetDateTime("DateApproved");
                customer.ApprovalStatus = dataReader.GetInt32("ApprovalStatus");
                customer.DateCreated = dataReader.GetDateTime("DateCreated");
                customer.DateUpdated = dataReader.GetDateTime("DateUpdated");

            }
            reader.Close();

            return customer;
        }

        /// <summary>
        /// Get Payment Type
        /// </summary>
        /// <param name="paymentTypeID"></param>
        /// <returns></returns>
        public PaymentType GetPaymentType(Guid paymentTypeID)
        {
            PaymentType paymentType = new PaymentType();

            string sql = "SELECT * FROM PaymentTypes WHERE PaymentTypeID = '" + paymentTypeID.ToString() + "'";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                paymentType.PaymentTypeID = dataReader.GetGuid("PaymentTypeID");
                paymentType.Description = dataReader.GetString("Description");
                paymentType.RequiresCreditCard = dataReader.GetInt32("RequiresCreditCard");

            }
            reader.Close();

            return paymentType;

        }

        /// <summary>
        /// Customer Inquiry
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public List<CustomerInquiry> CustomerInquiry(string firstName, string lastName, DataGridPagingInformation paging)
        {

            List<Customer> customers = new List<Customer>();

            string sortExpression = paging.SortExpression;

            int maxRowNumber;
            int minRowNumber;

            minRowNumber = (paging.PageSize * (paging.CurrentPageNumber - 1)) + 1;
            maxRowNumber = paging.PageSize * paging.CurrentPageNumber;

            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder sqlWhereBuilder = new StringBuilder();

            string sqlWhere = string.Empty;

            if (firstName != null && firstName.Trim().Length > 0)
                sqlWhereBuilder.Append(" c.FirstName LIKE @FirstName AND ");

            if (lastName != null && lastName.Trim().Length > 0)
                sqlWhereBuilder.Append(" c.LastName LIKE @LastName AND ");

            if (sqlWhereBuilder.Length > 0)
                sqlWhere = " WHERE " + sqlWhereBuilder.ToString().Substring(0, sqlWhereBuilder.Length - 4);

            sqlBuilder.Append(" SELECT COUNT(*) as total_records FROM Customers c ");
            sqlBuilder.Append(sqlWhere);
            sqlBuilder.Append(";");
            sqlBuilder.Append(" SELECT * FROM ( ");
            sqlBuilder.Append(" SELECT (ROW_NUMBER() OVER (ORDER BY " + paging.SortExpression + " " + paging.SortDirection + ")) as record_number, ");
            sqlBuilder.Append(" c.*, p.Description as PaymentTypeDescription ");
            sqlBuilder.Append(" FROM Customers c ");
            sqlBuilder.Append(" INNER JOIN PaymentTypes p ON p.PaymentTypeID = c.PaymentTypeID ");
            sqlBuilder.Append(sqlWhere);
            sqlBuilder.Append(" ) Rows ");
            sqlBuilder.Append(" where record_number between " + minRowNumber + " and " + maxRowNumber);

            string sql = sqlBuilder.ToString();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = sql;
            sqlCommand.Connection = dbConnection;

            if (firstName != null && firstName.Trim().Length > 0)
            {
                sqlCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@FirstName"].Value = firstName + "%";
            }

            if (lastName != null && lastName.Trim().Length > 0)
            {
                sqlCommand.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@LastName"].Value = lastName + "%";
            }

            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            paging.TotalRows = Convert.ToInt32(reader["Total_Records"]);
            paging.TotalPages = Utilities.CalculateTotalPages(paging.TotalRows, paging.PageSize);

            reader.NextResult();

            List<CustomerInquiry> customerList = new List<CustomerInquiry>();

            while (reader.Read())
            {
                CustomerInquiry customer = new CustomerInquiry();

                DataReader dataReader = new DataReader(reader);

                customer.CustomerID = dataReader.GetGuid("CustomerID");
                customer.FirstName = dataReader.GetString("FirstName");
                customer.LastName = dataReader.GetString("LastName");
                customer.EmailAddress = dataReader.GetString("EmailAddress");
                customer.City = dataReader.GetString("City");
                customer.Country = dataReader.GetString("Country");
                customer.PaymentTypeDescription = dataReader.GetString("PaymentTypeDescription");

                customerList.Add(customer);

            }

            reader.Close();

            return customerList;


        }

        /// <summary>
        /// Delete All Customers
        /// </summary>
        public void DeleteAllCustomers()
        {

            SqlCommand sqlCommand;
            string sql = string.Empty;

            sql = "DELETE FROM CUSTOMERS ";

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.Transaction = dbTransaction;
            sqlCommand.CommandText = sql;
            sqlCommand.ExecuteNonQuery();

        }

        //收缩数据库
        private void ShrinkDatabase()
        {
          
            AdoDataService dataService = new AdoDataService();

            dataService.CreateSession();

            SqlCommand sqlCommand;
            string sql = string.Empty;

            sql = "DBCC SHRINKDATABASE (JMDatabase,10)";

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = dataService.dbConnection;
            sqlCommand.CommandText = sql;
            sqlCommand.ExecuteNonQuery();

            dataService.CloseSession();
        }

        /// <summary>
        /// Get Payment Types
        /// </summary>
        /// <returns></returns>
        public List<PaymentType> GetPaymentTypes()
        {
            List<PaymentType> paymentTypes = new List<PaymentType>();

            string sql = "SELECT * FROM PaymentTypes ORDER BY Description";

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                PaymentType paymentType = new PaymentType();
                paymentType.PaymentTypeID = dataReader.GetGuid("PaymentTypeID");
                paymentType.Description = dataReader.GetString("Description");
                paymentType.RequiresCreditCard = dataReader.GetInt16("RequiredCreditCard");

                paymentTypes.Add(paymentType);

            }
            reader.Close();
            return paymentTypes;

        }


    }

}
