using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    public static class GetContext
    {
        private static SMaPEntities applicationDbContext = null;
        public static SMaPEntities getContext()
        {
            if (applicationDbContext == null)
            {
                applicationDbContext=new SMaPEntities();
            }
            return applicationDbContext;
        }
        public static void RefreshContext()
        {
            var context = ((IObjectContextAdapter)getContext()).ObjectContext;
            var refreshableObjects = applicationDbContext.ChangeTracker.Entries().Select(c => c.Entity).ToList();
            context.Refresh(RefreshMode.StoreWins, refreshableObjects);
        }
    }
}
