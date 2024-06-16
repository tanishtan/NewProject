using ConsoleAppADO.DataAccess;
using ConsoleAppADO.Repositories;

namespace NewProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // WorkingWithADO1.Test();           
            //TestGetCustomersById();
            //InsertIntoCustomers();
            //WorkingWithADONET.Test9();
            //TestGetAllCustomers();
            //WorkingWithDataSet.Test2();
            //LinqOperators.Test();

            //CustomersDBContext db = new CustomersDBContext();

            /*var q1 = from c in db.Customers.AsEnumerable()
                     where c.Country == "USA"
                     orderby c.CompanyName
                     select c;

            q1.ToList().ForEach(c =>

            {
                Console.WriteLine("ID: {0}, Company: {1}", c.CustomerId, c.CompanyName);
                Console.WriteLine("\tContact: {0}, Location: {1}-{2}", c.ContactName, c.City, c.Country);
            });*/

            /*var q2 = db.Customers
             .AsNoTracking()
             //.AsEnumerable()
             .Take(25)
             .Skip(10)
             .Take(3)
             .ToList();
            q2.ToList().ForEach(c =>
            {
                Console.WriteLine("ID: {0}, Company: {1}", c.CustomerId, c.CompanyName);
                Console.WriteLine("\tContact: {0}, Location: {1}-{2}", c.ContactName, c.City, c.Country);
            });*/

            // Add New Object
            /*Customer cust = new Customer
            {
                CustomerId = "66778",
                CompanyName = "66778",
                ContactName = "66778",
                City = "66778",
                Country = "66778"
            };
            db.Customers.Add(cust);
            db.SaveChanges();*/
            /*var list = db.Customers.ToList();
            *//* list.ForEach(c =>
             {
                 Console.WriteLine("ID: {0}, Company: {1}", c.CustomerId, c.CompanyName);
                 Console.WriteLine("\tContact: {0}, Location: {1}-{2}", c.ContactName, c.City, c.Country);
             });*//*

            var item = db.Customers
               .AsNoTracking()
                .FirstOrDefault(c => c.CustomerId == "ALFKI");

            if (item is null)
            {
                Console.WriteLine("Nothing Fetched");
            }
            else
            {
                Console.WriteLine("ID: {0}, Company: {1}", item.CustomerId, item.CompanyName);
                Console.WriteLine("\tContact: {0}, Location: {1}-{2}", item.ContactName, item.City, item.Country);
            }
            //below to execute custom sql
            list = db.Customers
                .FromSql($"select CustomerId,CompanyName,ContactName,City,Country from Customers where country='USA'").ToList();

            list.ForEach(c =>
            {
                Console.WriteLine("ID: {0}, Company: {1}", c.CustomerId, c.CompanyName);
                Console.WriteLine("\tContact: {0}, Location: {1}-{2}", c.ContactName, c.City, c.Country);
            });

            var count = db.Database.SqlQuery<int>($"select count(*) from products")  //querying for non entity
                .ToList().First();
            Console.WriteLine(count);


            list = db.Customers
                .FromSql($"exec sp_getAllCustomers 'na' ")
                .ToList();

            list.ForEach(c =>
            {
                Console.WriteLine("ID: {0}, Company: {1}", c.CustomerId, c.CompanyName);
                Console.WriteLine("\tContact: {0}, Location: {1}-{2}", c.ContactName, c.City, c.Country);
            });*/
            TestEmployeeRepository();
        }

        static void TestEmployeeRepository()
        {
            Action<Employee> PrintDetails = (c) => Console.WriteLine($"{c.EmployeeId} {c.FullName}");
            IRepository<Employee, int> repository = new EmployeeRepository();
            var items = repository.GetAll();
            items.ToList().ForEach(c => PrintDetails(c));
            var emp = repository.FindById(9);
            PrintDetails(emp);
            emp = new Employee
            {
                EmployeeId = 0,
                FirstName = "Harry",
                LastName = "Kane",
                HireDate = DateTime.Now,
            };
            repository.Upsert(emp);
            //refetch to check the insert
            items.ToList().ForEach((c) => PrintDetails(c));
            Console.WriteLine("Updating now");
            emp = repository.FindById(20);
            emp.FirstName = "Hardik";
            emp.HireDate = new DateTime(2020, 02, 02);
            repository.Upsert(emp);

            //refetch the row to check the update operation
            emp = repository.FindById(20);
            PrintDetails(emp);
            Console.WriteLine("Deleting now");
            //remove the row we have inserted

            repository.RemoveById(20);
            Console.WriteLine();
            items = repository.GetAll();
            items.ToList().ForEach(c => PrintDetails(c));
        }
        static void TestGetAllCustomers()
        {
            CustomerDataAccess cda = new CustomerDataAccess();
            var list = cda.GetAllCustomers();
            list.ForEach(c =>
            {
                Console.WriteLine("ID: {0}, Company: {1}", c.CustomerId, c.CompanyName);
                Console.WriteLine("\tContact: {0}, Location: {1}-{2}", c.ContactName, c.City, c.Country);
            });
            Console.WriteLine();

        }

        static void TestGetCustomersById()
        {
            try
            {

                CustomerDataAccess cda = new CustomerDataAccess();
                string Id = Console.ReadLine();
                var list = cda.GetCustomersById(Id);
                if (list is null)
                {
                    Console.WriteLine("Nothing Found");
                }
                else
                {
                    Console.WriteLine("ID: {0}, Company: {1}", list.CustomerId, list.CompanyName);
                    Console.WriteLine("\tContact: {0}, Location: {1}-{2}", list.ContactName, list.City, list.Country);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            Console.WriteLine();
        }

        static void InsertIntoCustomers()
        {
            CustomerDataAccess cda = new CustomerDataAccess();

            Func<string, string> GetInput = (text) =>
            {
                Console.WriteLine($"Enter {text}:");
                var str = Console.ReadLine();
                return str;
            };
            /*Customer cust = new Customer
            {
                CustomerId="ABCDE",
                CompanyName="ADBCDE",
                ContactName="ABDCDE",
                City="ADBCCC",
                Country="asrfll"
            };*/ // --one way
            Customer cust = new Customer
            {
                CustomerId = GetInput("Customer Id"),
                CompanyName = GetInput("Company Name"),
                ContactName = GetInput("Contact Name"),
                City = GetInput("City"),
                Country = GetInput("Country")
            };
            cda.InsertIntoCustomers(cust);//second-wy
        }

        static void UpdateCustomers()
        {
            CustomerDataAccess cda = new CustomerDataAccess();

            Func<string, string> GetInput = (text) =>
            {
                Console.WriteLine($"Enter {text}:");
                var str = Console.ReadLine();
                return str;
            };
            /*Customer cust = new Customer
            {
                CustomerId="ABCDE",
                CompanyName="ADBCDE",
                ContactName="ABDCDE",
                City="ADBCCC",
                Country="asrfll"
            };*/ // --one way
            Customer cust = new Customer
            {
                CustomerId = GetInput("Customer Id"),
                CompanyName = GetInput("Company Name"),
                ContactName = GetInput("Contact Name"),
                City = GetInput("City"),
                Country = GetInput("Country")
            };
            cda.UpadteNewCustomer(cust);//second-wy
        }
    }
}
