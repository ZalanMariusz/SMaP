using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    class ServiceStoreParamsDAL:GenericDAL<ServiceStoreParams>
    {
        public List<ServiceStoreParams> ReloadInputFieldList(ServiceStore Service)
        {
            RefreshContext();
            return FindAll(x => x.ServiceStore == Service && x.InOut);
        }
        public List<ServiceStoreParams> ReloadOutputFieldList(ServiceStore Service)
        {
            RefreshContext();
            return FindAll(x => x.ServiceStore == Service && !x.InOut);
        }
    }
}
