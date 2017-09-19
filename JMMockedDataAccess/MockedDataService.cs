using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMDataServiceInterface;

namespace JMMockedDataAccess
{

    public class MockedDataService : IDataService, IDisposable
    {

        public void CommitTransaction(Boolean closeSession)
        {

        }

        public void RollbackTransaction(Boolean closeSession)
        {

        }

        public void Save(object entity) { }

        public void CreateSession()
        {


        }

        public void BeginTransaction()
        {

        }

        public void CloseSession()
        {

        }

        public void Dispose()
        {

        }

    }
}
