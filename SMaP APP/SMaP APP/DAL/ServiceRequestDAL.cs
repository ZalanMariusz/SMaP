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
            return FindAll(x => x.RequesterTeamID == ContextStudent.TeamID).OrderBy(x => x.RequestState).ToList();
        }

        public List<ServiceRequest> ReloadProvidedRequestsList(Student ContextStudent)
        {
            RefreshContext();
            int ApprovedID = StateListByName().FirstOrDefault(y => y.ItemName == "Jóváhagyva").ID;
            int DeclineID= StateListByName().FirstOrDefault(y => y.ItemName == "Visszautasítva").ID;
            return FindAll(x => x.ProviderTeamID == ContextStudent.TeamID && x.RequestState != ApprovedID && x.RequestState!=DeclineID).OrderBy(x=>x.RequestState).ToList();
        }
        public List<Dictionary> StateListByName()
        {
            DictionaryDAL DD = new DictionaryDAL();
            return DD.DictionaryListByType("Igény állapota");
        }
    }
}
