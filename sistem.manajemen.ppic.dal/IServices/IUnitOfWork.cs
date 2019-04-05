using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetGenericRepository<T>()
           where T : class;
        void Dispose();
        /// <summary>
        /// Saves current context changes.
        /// </summary>
        void SaveChanges();
        Task SaveAsync();
        void RevertChanges();
    }
}
