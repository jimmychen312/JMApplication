using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMDataServiceInterface;
using JMEFDataAccess;
using JMModels;
using System.Linq.Dynamic;

namespace JMEFDataAccess
{

    public class EFSysLogService : EFDataService, ISysLogDataService
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public EFSysLogService()
        {

        }

        /// <summary>
        /// Create SysLog
        /// </summary>
        /// <param name="sysLog"></param>
        public void CreateSysLog(SysLog sysLog)
        {
            DateTime dateCreated = System.DateTime.Now;
            sysLog.Id = Guid.NewGuid().ToString();
            sysLog.CreateTime = dateCreated;
            dbConnection.SysLog.Add(sysLog);
        }

        /// <summary>
        /// Update SysLog
        /// </summary>
        /// <param name="sysLog"></param>
        public void UpdateSysLog(SysLog sysLog)
        {
            DateTime dateUpdated = System.DateTime.Now;
            sysLog.CreateTime = dateUpdated;
        }

        /// <summary>
        /// Get SysLog By SysLog ID
        /// </summary>
        /// <param name="sysLogID"></param>
        /// <returns></returns>
        public SysLog GetSysLogById(string sysLogID)
        {
            var sysLogInformation = dbConnection.SysLog.First(c => c.Id == sysLogID);
            SysLog sysLog = sysLogInformation as SysLog;
            return sysLog;
        }

        ///// <summary>
        ///// Get Payment Type
        ///// </summary>
        ///// <param name="paymentTypeID"></param>
        ///// <returns></returns>
        //public PaymentType GetPaymentType(Guid paymentTypeID)
        //{
        //    var paymentInformation = dbConnection.PaymentTypes.First(p => p.PaymentTypeID == paymentTypeID);
        //    PaymentType paymentType = paymentInformation as PaymentType;
        //    return paymentInformation;
        //}

        /// <summary>
        /// SysLog Inquiry
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public List<SysLogInquiry> SysLogInquiry(string queryStr, DataGridPagingInformation paging)
        {

            string sortExpression = paging.SortExpression;

            if (paging.SortDirection != string.Empty)
                sortExpression = sortExpression + " " + paging.SortDirection;

            var sysLogQuery = dbConnection.SysLog.AsQueryable();

            if (queryStr != null && queryStr.Trim().Length > 0)
            {
                sysLogQuery = sysLogQuery.Where(c => c.Id.StartsWith(queryStr));
            }

            //if (lastName != null && lastName.Trim().Length > 0)
            //{
            //    sysLogQuery = sysLogQuery.Where(c => c.LastName.StartsWith(lastName));
            //}

            var sysLogs = from c in sysLogQuery
                            //join p in dbConnection.PaymentTypes on c.PaymentTypeID equals p.PaymentTypeID
                            select new { c.Id, c.Operator, c.Message, c.Result, c.Type, c.Module, c.CreateTime };

            int numberOfRows = sysLogs.Count();

            sysLogs = sysLogs.OrderBy(sortExpression);

            var sysLogList = sysLogs.Skip((paging.CurrentPageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            paging.TotalRows = numberOfRows;
            paging.TotalPages = Utilities.CalculateTotalPages(numberOfRows, paging.PageSize);

            List<SysLogInquiry> sysLogInquiry = new List<SysLogInquiry>();

            foreach (var sysLog in sysLogList)
            {
                SysLogInquiry sysLogData = new SysLogInquiry();
                sysLogData.Id = sysLog.Id;
                sysLogData.Operator = sysLog.Operator;
                sysLogData.Message = sysLog.Message;
                sysLogData.Result = sysLog.Result;
                sysLogData.Type = sysLog.Type;
                sysLogData.Module = sysLog.Module;
                sysLogData.CreateTime = sysLog.CreateTime;
                sysLogInquiry.Add(sysLogData);
            }

            return sysLogInquiry;


        }

        ///// <summary>
        ///// Get Payment Types
        ///// </summary>
        ///// <returns></returns>
        //public List<PaymentType> GetPaymentTypes()
        //{

        //    var paymentTypesQuery = dbConnection.PaymentTypes.AsQueryable();
        //    var paymentTypes = (from p in paymentTypesQuery.OrderBy("Description") select p).ToList();

        //    int numberOfRows = paymentTypes.Count();

        //    return paymentTypes;

        //}

        /// <summary>
        /// Delete All SysLogs
        /// </summary>
        public void DeleteSysLogById( string Id)
        {
            dbConnection.Database.ExecuteSqlCommand("Delete from SysLogs where Id ='"+ Id +"'");
        }

    }
}
