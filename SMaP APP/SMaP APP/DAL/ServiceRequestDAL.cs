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
        public List<ServiceRequest> ReloadRequestedRequests(Student ContextStudent, int? rowNumber, int? providerTeamID, int? requestState, int? requestType, int? creatorId, int? assigneeID)
        {
            RefreshContext();
            return applicationDbContext.uspGetRequestedRequests(ContextStudent.TeamID, rowNumber, providerTeamID, requestState, requestType, creatorId, assigneeID).ToList();
        }

        public List<ServiceRequest> ReloadProvidedRequestsList(Student ContextStudent,int? rowNumber,int? requesterTeamID,int? requestState, int? requestType,int? creatorId,int? assigneeID)
        {
            RefreshContext();
            return applicationDbContext.uspGetProvidedRequests(ContextStudent.TeamID, rowNumber, requesterTeamID, requestState, requestType, creatorId, assigneeID).ToList();
        }

        public List<ServiceRequest> ReloadAllServiceRequests(int SessionGroupID, int? rowNumber, int? requesterTeamID, int? providerTeamID, int? requestState, int? requestType, int? creatorId, int? assigneeID)
        {
            RefreshContext();
            return applicationDbContext.uspGetAllServiceRequests(SessionGroupID, rowNumber, requesterTeamID, providerTeamID ,requestState, requestType, creatorId, assigneeID).ToList();
        }

        public List<Dictionary> StateListByName()
        {
            DictionaryDAL DD = new DictionaryDAL();
            return DD.DictionaryListByType(5);
        }
    }
}
