using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    class ServiceRequestDAL : GenericDAL<ServiceRequest>
    {
        public List<ServiceRequest> ReloadRequestedRequests(Student ContextStudent)
        {
            RefreshContext();
            return FindAll(x => x.RequesterTeamID == ContextStudent.TeamID).ToList();
        }

        public List<ServiceRequest> ReloadProvidedRequestsList(Student ContextStudent)
        {
            RefreshContext();
            int ApprovedID = StateListByName().FirstOrDefault(y => y.ItemName == "Jóváhagyva").ID;
            return FindAll(x => x.ProviderTeamID == ContextStudent.TeamID && x.RequestState != ApprovedID);
        }
        private List<Dictionary> StateListByName()
        {
            DictionaryDAL DD = new DictionaryDAL();
            return DD.DictionaryListByType("Igény állapota");
        }
    }
}
