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

    public class EFSysModuletestDateService : EFDataService, ISysModuletestDataService
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public EFSysModuletestDateService()
        {

        }
              
         

        /// <summary>
        /// Get Payment Types
        /// </summary>
        /// <returns></returns>
        public List<SysModule> GetSysModuleList()
        {

            var sysModuleQuery = dbConnection.SysModules.AsQueryable();
            var sysModulexs = (from p in sysModuleQuery.Where(p=> p.ParentId =="0") select p).ToList();

            int numberOfRows = sysModulexs.Count();

            return sysModulexs;

        }

    }
}
