﻿using sistem.manajemen.ppic.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal
{
    public class SqlGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal PPICEntities _context;
        internal DbSet<TEntity> _dbSet;
        public SqlGenericRepository(PPICEntities context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();

        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.Count();
        }

        public virtual TEntity GetByID(object id)
        {
            return _dbSet.Find(id);

        }

        /// <summary>
        /// Gets the by identifier.
        /// !! Custom modif
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        public virtual TEntity GetByID(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }


        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);

        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Detach(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void InsertOrUpdate(TEntity entity)
        {
            if (!Exists(entity))
                Insert(entity);
            else
                Update(entity);
        }

        public void InsertOrUpdate(TEntity entity, Login userLogin, Enums.MenuList menuId)
        {
            if (!Exists(entity))
                Insert(entity);
            else
                Update(entity);
            
            SaveChangesLog(entity, userLogin, menuId);
        }
        public void InsertOrUpdateBulk(IEnumerable<TEntity> entities)
        {
            var entitiesTobeInserted = new List<TEntity>();
            foreach (var entity in entities)
            {
                if (Exists(entity))
                {
                    Update(entity);
                }
                else
                {
                    entitiesTobeInserted.Add(entity);
                }
            }
            //_context.Configuration.AutoDetectChangesEnabled = false;
            //_context.Configuration.ValidateOnSaveEnabled = false;
            _dbSet.AddRange(entitiesTobeInserted);
        }
        public void SaveChangesLog(TEntity entity, Login user, Enums.MenuList menuId)
        {
            if (user != null)
            {
                var isAdded = _context.ChangeTracker.Entries().Any(x => x.State == EntityState.Added);
                var isModified = _context.ChangeTracker.Entries().Any(x => x.State == EntityState.Modified);
                var isDeleted = _context.ChangeTracker.Entries().Any(x => x.State == EntityState.Deleted);

                var action = "";
                if (isAdded)
                {
                    action = "Create";

                }

                if (isModified)
                {
                    action = "Modified";
                }
                _context.SaveChanges();
                ObjectContext objectContext = ((IObjectContextAdapter)_context).ObjectContext;
                ObjectSet<TEntity> set = objectContext.CreateObjectSet<TEntity>();
                IEnumerable<string> keyNames = set.EntitySet.ElementType
                                                            .KeyMembers
                                                            .Select(k => k.Name);
                string id = "";
                if (keyNames.Count() > 0)
                {
                    foreach (var name in keyNames)
                    {
                        id += _context.Entry(entity).CurrentValues[name].ToString();
                    }
                }
               
                _context.CHANGES_HISTORY.Add(new CHANGES_HISTORY()
                {
                    MODUL_ID = (int)menuId,
                    FORM_ID = long.Parse(id),
                    MODIFIED_BY = user.USER_ID,
                    MODIFIED_DATE = DateTime.Now,
                    ACTION = action
                });
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// check if the specified entity exists
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Boolean.</returns>
        public bool Exists(TEntity entity)
        {
            var objContext = ((IObjectContextAdapter)_context).ObjectContext;
            var objSet = objContext.CreateObjectSet<TEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            // TryGetObjectByKey attaches a found entity
            // Detach it here to prevent side-effects
            if (exists)
            {
                objContext.Detach(foundEntity);
            }
            return (exists);
        }


        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicate != null)
            {
                return _dbSet.Where(predicate);
            }
            return _dbSet;
        }

        public void ExecuteSql(string sql)
        {
            _context.Database.ExecuteSqlCommand(sql);
        }

        public void ExecuteQuery(string sql)
        {
            _dbSet.SqlQuery(sql);
        }
    }
}
