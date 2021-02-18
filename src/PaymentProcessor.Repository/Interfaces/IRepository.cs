using PaymentProcessor.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Repository.Interfaces
{
    public interface IRepository: IAutoDependencyRepository
    {
        void Add<T>(T entity) where T: class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChanges();
    }
}
