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
using System.Windows;

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
            try
            {
                GetContext.RefreshContext();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ismeretlen hiba", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }

        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            try
            {
                IQueryable<TEntity> entities = applicationDbContext.Set<TEntity>();
                if (filterExpression != null)
                {
                    entities = entities.Where(filterExpression);
                }
                return entities.Where(x => !x.Deleted).ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ismeretlen hiba", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return new List<TEntity>();
        }

        public virtual TEntity FindById(int id)
        {
            try
            {
                return applicationDbContext.Set<TEntity>().FirstOrDefault(d => d.ID == id && !d.Deleted);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ismeretlen hiba", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return null;
        }

        public virtual int Create(TEntity entity)
        {
            try
            {
                applicationDbContext.Set<TEntity>().Attach(entity);
                applicationDbContext.Set<TEntity>().Add(entity);
                var itemsToDelete = applicationDbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
                var itemsToDelete2 = applicationDbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Detached);
                var itemsToDelete3 = applicationDbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
                applicationDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ismeretlen hiba", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return 0;
        }

        public void CleanUp()
        {
            try
            {
                var itemsToDelete = applicationDbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
                foreach (var item in itemsToDelete)
                {
                    item.State = EntityState.Detached;
                }
                RefreshContext();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ismeretlen hiba", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        public virtual int Update(TEntity entity)
        {
            try
            {
                applicationDbContext.Set<TEntity>().AddOrUpdate(entity);
                var t = applicationDbContext.Entry(entity).State;
                return applicationDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ismeretlen hiba", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return 0;
        }

        public virtual int PhysicalDelete(TEntity entity)
        {
            try
            {
                applicationDbContext.Set<TEntity>().Attach(entity);
                applicationDbContext.Entry(entity).State = EntityState.Deleted;
                return applicationDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ismeretlen hiba", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return 0;
        }

        public virtual int LogicalDelete(TEntity entity)
        {
            try
            {
                entity.Deleted = true;
                return Update(entity);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ismeretlen hiba", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return 0;
        }
    }
}

