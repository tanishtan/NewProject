using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppADO.Repositories
{
    public class EmployeeRepository : IRepository<Employee, int>
    {
        EmployeeDbContext DbContext = new EmployeeDbContext();
        public Employee FindById(int id)
        {
            return DbContext.Employees
                //.AsNoTracking()
                .FirstOrDefault(c => c.EmployeeId == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return DbContext.Employees
                //.AsNoTracking()
                .ToList();
        }

        public IEnumerable<Employee> GetByCriteria(string filterCriteria)
        {
            return null;
        }

        public void RemoveById(int id)
        {
            var emp = FindById(id);
            if (emp != null)
            {
                //DbContext.Entry(emp).State = EntityState.Deleted;

                /*DbContext.Employees.Remove(emp);
                DbContext.SaveChanges();*/
                DbContext.Employees.
                    Where(c => c.EmployeeId == id)
                    .ExecuteDelete();
            }

        }

        public void Upsert(Employee entity)
        {
            //insert (if not exists) or update (if exists)
            var emp = FindById(entity.EmployeeId);
            DbContext.ChangeTracker.Clear();
            DbContext.ChangeTracker.DetectChanges();
            
            if (emp is null)
            {
                DbContext.Employees.Add(entity);
                DbContext.SaveChanges();
            }
            else
            {
                /*//DbContext.Entry(entity).State = EntityState.Modified;
                DbContext.Employees.Update(entity);
                //to remove changes at once
                // delete from employees where employeeid between 16 and 19
                for(int i = 16; i < 19; i++)
                {
                    var obj = DbContext.Employees.Find(i);
                    if(obj != null)
                        DbContext.Employees.Remove(obj);
                }*/

                //Firectly execute the update stmt, does not involve the change tracking mechanism
                DbContext.Employees.Where(c=>c.EmployeeId == entity.EmployeeId)
                    .ExecuteUpdate(setters =>
                setters.SetProperty(p => p.FirstName, entity.FirstName)
                .SetProperty(p => p.LastName, entity.LastName)
                .SetProperty(p=>p.HireDate, entity.HireDate));
            }
            DbContext.SaveChanges();
        }
    }
}
