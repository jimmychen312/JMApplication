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

    public class EFPaymentTypeService : EFDataService, IPaymentTypeDataService
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public EFPaymentTypeService()
        {

        }

         

        ///// <summary>
        ///// Get Payment Types
        ///// </summary>
        ///// <returns></returns>
        //public List<PaymentType> GetPaymentTypes()
        //{

        //    var paymentTypesQuery = dbConnection.PaymentTypes.AsQueryable();
        //    var paymentTypes = (from p in paymentTypesQuery.Where(p=> p.RequiresCreditCard ==1).OrderBy("Description") select p).ToList();

        //    int numberOfRows = paymentTypes.Count();

        //    return paymentTypes;

        //}

        /// <summary>
        /// Get Payment Types
        /// </summary>
        /// <returns></returns>
        public List<PaymentType> GetPaymentTypes()
        {

            var paymentTypesQuery = dbConnection.PaymentTypes.AsQueryable();
            var paymentTypes = (from p in paymentTypesQuery.Where(p=> p.RequiresCreditCard ==1).OrderBy("Description") select p).ToList();

            int numberOfRows = paymentTypes.Count();

            return paymentTypes;

        }

    }
}
