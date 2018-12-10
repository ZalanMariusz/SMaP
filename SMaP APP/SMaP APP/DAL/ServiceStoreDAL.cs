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
            return FindAll(x => x.ProviderTeamID == TeamID).OrderBy(x => x.ServiceNumber).ToList();
        }

        public List<ServiceStore> RequestedServices(int TeamID)
        {
            RefreshContext();
            ServiceStoreUserTeamsDAL serviceStoreUserTeamsDal = new ServiceStoreUserTeamsDAL();
            return FindAll(x=>x.ProviderTeamID!=TeamID).Join(serviceStoreUserTeamsDal.FindAll(x => x.RequesterTeamID == TeamID),
                    ServiceStore => ServiceStore.ID, 
                    Request => Request.ServiceID, 
                    (ServiceStore, Request) => ServiceStore).OrderBy(x=>x.ServiceNumber).ToList();
        }

        public List<ServiceStore> AllServicesForSessionGroup(int SessionGroupID)
        {
            RefreshContext();
            TeamDAL td = new TeamDAL();
            return FindAll().Join(td.FindAll(x => x.SessionGroupID == SessionGroupID),
                    ServiceStore => ServiceStore.ProviderTeamID,
                    Team => Team.ID,
                    (ServiceStore, Team) => ServiceStore).OrderBy(x => x.ServiceNumber).ToList();
        }



        //public List<ServiceStore> RequestedServices(int TeamID)
        //{
        //    RefreshContext();
        //    return FindAll(x => x.RequesterTeamID == TeamID && x.RequesterTeamID!=x.ProviderTeamID);
        //}
    }
}
