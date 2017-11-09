using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.DAL
{
    public class Repository<T> : IDisposable where T : class
    {
        #region Atributos

        MAGASYSEntities Context = null; 

        #endregion

        #region Propiedades

        private DbSet<T> EntitySet
        {
            get
            {
                return Context.Set<T>();
            }

        }

        #endregion

        #region Constructor

        public Repository()
        {
            Context = new MAGASYSEntities();
        }

        #endregion

        #region Métodos Púbicos

        public T Create(T reg)
        {
            T loResult = null;
            try
            {
                EntitySet.Add(reg);
                Context.SaveChanges();
                loResult = reg;
            }
            catch (Exception)
            {
                throw;
            }

            return loResult;
        }

        public bool Update(T reg)
        {
            bool loResult = false;
            try
            {
                EntitySet.Attach(reg);
                Context.Entry<T>(reg).State = EntityState.Modified;
                loResult = Context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }

            return loResult;
        }

        public bool Delete(T reg)
        {
            bool loResult = false;
            try
            {
                EntitySet.Attach(reg);
                EntitySet.Remove(reg);
                loResult = Context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }

            return loResult;
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }

        public T Find(Expression<Func<T, bool>> expr)
        {
            T loResult = null;
            try
            {
                loResult = EntitySet.FirstOrDefault(expr);
            }
            catch (Exception)
            {
                throw;
            }

            return loResult;
        }

        public List<T> Search(Expression<Func<T, bool>> expr)
        {
            List<T> loResult = null;
            try
            {
                loResult = EntitySet.Where(expr).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return loResult;
        }

        public List<T> FindAll()
        {
            List<T> loResult = null;
            try
            {
                loResult = EntitySet.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return loResult;
        }

        #endregion
    }
}
