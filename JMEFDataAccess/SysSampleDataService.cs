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

    public class EFSysSampleService : EFDataService,ISysSampleDataService
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public EFSysSampleService()
        {

        }

                 /// <summary>
        /// Create SysSample
        /// </summary>
        /// <param name="sysSample"></param>
        public void CreateSysSample(SysSample sysSample)
        {
            DateTime dateCreated = System.DateTime.Now;
                      
        }

        /// <summary>
        /// Update SysSample
        /// </summary>
        /// <param name="sysSample"></param>
        public void UpdateSysSample(SysSample sysSample)
        {
            DateTime dateUpdated = System.DateTime.Now;
            
        }

        /// <summary>
        /// Get SysSample By SysSample ID
        /// </summary>
        /// <param name="sysSampleID"></param>
        /// <returns></returns>
        public SysSample GetSysSampleById(String sysSampleID)
        {
            var sysSampleInformation = dbConnection.SysSamples.First(c => c.Id == sysSampleID);
            SysSample sysSample = sysSampleInformation as SysSample;
            return sysSample;
        }

        

        /// <summary>
        /// SysSample Inquiry
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public List<SysSampleInquiry> SysSampleInquiry(string queryStr, DataGridPagingInformation paging)
        {

            string sortExpression = paging.SortExpression;

            if (paging.SortDirection != string.Empty)
                sortExpression = sortExpression + " " + paging.SortDirection;

            var sysSampleQuery = dbConnection.SysSamples.AsQueryable();

            if (queryStr != null && queryStr.Trim().Length > 0)
            {
                sysSampleQuery = sysSampleQuery.Where(c => c.Id.StartsWith(queryStr));
            }
                        

            var sysSamples = from c in sysSampleQuery
                            
                            select new { c.Id, c.Name, c.Age, c.Bir, c.Photo, c.Note, c.CreateTime };

            int numberOfRows = sysSamples.Count();

            sysSamples = sysSamples.OrderBy(sortExpression);

            var sysSampleList = sysSamples.Skip((paging.CurrentPageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            paging.TotalRows = numberOfRows;
            paging.TotalPages = Utilities.CalculateTotalPages(numberOfRows, paging.PageSize);

            List<SysSampleInquiry> sysSampleInquiry = new List<SysSampleInquiry>();

            foreach (var sysSample in sysSampleList)
            {
                SysSampleInquiry sysSampleData = new SysSampleInquiry();
                sysSampleData.Id = sysSample.Id;
                sysSampleData.Name = sysSample.Name;
                sysSampleData.Age = sysSample.Age;
                sysSampleData.Bir = sysSample.Bir;
                sysSampleData.Photo = sysSample.Photo;
                sysSampleData.Note = sysSample.Note;
                sysSampleData.CreateTime = sysSample.CreateTime;
                sysSampleInquiry.Add(sysSampleData);
            }

            return sysSampleInquiry;


        }

        ///// <summary>
        ///// Get Payment Types
        ///// </summary>
        ///// <returns></returns>
        //public List<Permission> GetPermissions()
        //{

        //    var permissionsQuery = dbConnection.permission.AsQueryable();
        //    var ermissions = (from p in permissionsQuery.OrderBy("Id") select p).ToList();

        //    int numberOfRows = ermissions.Count();

        //    return ermissions;

        //}

        /// <summary>
        /// Delete All SysSamples
        /// </summary>
        public void DeleteSysSampleById(string Id)
        {
            dbConnection.Database.ExecuteSqlCommand("Delete from SysSamples");
        }

    }
}
