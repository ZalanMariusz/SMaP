using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    class ServiceStoreDAL : GenericDAL<ServiceStore>
    {
        public List<ServiceStore> ProvidedServices(int TeamID)
        {
            RefreshContext();
            return FindAll(x=>x.ProviderTeamID==TeamID);
        }

       

        //public List<ServiceStore> RequestedServices(int TeamID)
        //{
        //    RefreshContext();
        //    return FindAll(x => x.RequesterTeamID == TeamID && x.RequesterTeamID!=x.ProviderTeamID);
        //}
    }
}
