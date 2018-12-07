using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    class RequestMessageDAL : GenericDAL<RequestMessage>
    {
        public List<RequestMessage> MessagesByRequest(int RequestID)
        {
            RefreshContext();
            return FindAll(x => x.RequestID == RequestID).OrderByDescending(x => x.Created).ToList();
        }
    }
}
