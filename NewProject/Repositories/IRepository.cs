using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppADO.Repositories
{
    public interface IRepository<TEntity, TIdentity>
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetByCriteria(string filterCriteria);
        TEntity FindById(TIdentity id);
        void Upsert(TEntity entity);
        void RemoveById(TIdentity id);
    }
    
    /*public interface ICRUDRepository<TEntity> { }
    public interface IOrderPorcessRepository<T,U>:IRepository<T,U>
    {
        void MakePayment();
        void ProcessOrder();
    }
    public class OrderRepository : IOrderPorcessRepository<Employee,int>
    {
        public Employee FindById(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Employee> GetAll()
        {
            throw new Exception();
        }
        public IEnumerable<Employee> GetByCriteria(string filterCriterial)
        {
            throw new NotSupportedException();
        }
    }
    */
}
