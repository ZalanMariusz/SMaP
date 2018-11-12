using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMaP_APP.Model;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Collections.ObjectModel;

namespace SMaP_APP.DAL
{
    public abstract class GenericDAL<TEntity> where TEntity : class, IBaseModel
    {
        internal SMaPEntities applicationDbContext { get { return GetContext.getContext(); } }

        public GenericDAL()
        {
        }

        public void RefreshContext()
        {
            GetContext.RefreshContext();
        }

        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            //RefreshContext();
            IQueryable<TEntity> entities = applicationDbContext.Set<TEntity>();
            if (filterExpression != null)
            {
                entities = entities.Where(filterExpression);
            }
            return entities.Where(x => !x.Deleted).ToList();
        }

        public virtual TEntity FindById(int id)
        {
            return applicationDbContext.Set<TEntity>().FirstOrDefault(d => d.ID == id && !d.Deleted);
        }

        public virtual int Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            List<EntityState> asd = new List<EntityState>();
            applicationDbContext.Set<TEntity>().Attach(entity);
            applicationDbContext.Set<TEntity>().Add(entity);
            applicationDbContext.SaveChanges();
            if (entity is Users)
            {
                foreach (var item in applicationDbContext.Set<Teacher>().Where(x => !x.Deleted))
                {
                    asd.Add(applicationDbContext.Entry(item).State);
                }
            }
            
            return 1;
        }

        public virtual int Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            applicationDbContext.Set<TEntity>().AddOrUpdate(entity);
            
            var t = applicationDbContext.Entry(entity).State;
            return applicationDbContext.SaveChanges();
        }

        public virtual int PhysicalDelete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            applicationDbContext.Set<TEntity>().Attach(entity);
            applicationDbContext.Entry(entity).State = EntityState.Deleted;
            return applicationDbContext.SaveChanges();
        }

        public virtual int LogicalDelete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.Deleted = true;
            return Update(entity);
        }
    }
}

