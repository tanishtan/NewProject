//using Azure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
//using System.Data.SqlClient;

namespace ConsoleAppADO
{
    internal class LinqOperators
    {
        static List<string> cities = new List<string>
        {
            "Bengaluru", "Chennai", "Hyderabad", "Amaravati", "Panaji", "Thiruvananthapuram", "Mumbai",
            "Gandhinagar", "Jaipur", "Chandigarh", "SHimla", "Dehradun", "Srinagar", "Leh", "Lucknow",
            "Patna", "Raipur", "Bhubaneswar", "Kolkata", "Gangtok", "Dispur", "Itanagar", "Aizwal", "Imphal",
            "Kohima", "Shillong", "Bhopal", "Agartala", "Ranchi", "New Delhi", "Puducherry", "Kavaratti", "Port Blair"
        };

        internal static void Test()
        {
            //BasicQuerying();
            //ProjectionOperator();
            //RestrictionOperators();
            //SortingOperators();
            //ElementOperators();
            // AggregationOperators();
            //PartitionOperators();
            //GroupingOperators();
            DataSetsWithLinq();
        }
        static Action<IEnumerable<string>, string> PrintTheList = (list, header) =>
        {
            Console.WriteLine($"*********** {header} *****************");
            Console.WriteLine();
            foreach (var item in list)
            {
                Console.Write($"{item,-20}");
            }
            Console.WriteLine("\n******************************************************");
            Console.WriteLine();
        };
        static int counter = 1;
        static void BasicQuerying()
        {
            //QUERY SYNTAX
            var q1 = from item in cities
                     select item;
            //defered query - Lazy initialization - query does not hold the data. 
            PrintTheList(q1, $"Test {counter++}. Basic Query with Query Syntax");

            //METHOD SYNTAX 
            var q2 = cities.Select(c => c);
            PrintTheList(q2, $"Test {counter++}. Basic Query with Method Syntax");
        }

        static void ProjectionOperator()
        {
            //Select, SelectMany, Zip 
            var q1 = from c in cities
                     select new
                     {
                         StartsWith = c[0],
                         Length = c.Length,
                         Name = c
                     };
            Console.WriteLine($"*********** Test {counter++} Projection Query Syntax *****************");
            Console.WriteLine();
            foreach (var item in q1)
            {
                Console.WriteLine($"Starts With={item.StartsWith,-2}Length={item.Length:00} Name={item.Name}");
            }
            Console.WriteLine("\n******************************************************");
            Console.WriteLine();

            var q2 = cities.Select(c => new
            {
                StartsWith = c[0],
                Length = c.Length,
                Name = c
            });
            Console.WriteLine($"*********** Test {counter++} Projection Method Syntax *****************");
            Console.WriteLine();
            foreach (var item in q2)
            {
                Console.WriteLine($"Starts With={item.StartsWith,-2}Length={item.Length:00} Name={item.Name}");
            }
            Console.WriteLine("\n******************************************************");
            Console.WriteLine();


            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            var words = new List<string> { "A", "V", "E", "R", "Y" };
            foreach (var zippedItem in numbers.Zip(words))
            {
                Console.WriteLine($"{zippedItem.First} = {zippedItem.Second}");
            }
        }
        static void RestrictionOperators()
        {
            //Where  
            var q1 = from c in cities
                     where c.Length > 10
                     select c;
            PrintTheList(q1, $"Test {counter++}. Filter Length > 10 ");
            //Normal coding 
            /* List<string> tempList = new List<string>();
             foreach (var item in cities)
             {
                 if (item.Length > 10) tempList.Add(item); 
             }
             PrintTheList(tempList)*/
            var q2 = cities.Where(c => c.Contains("na")).Select(c => c);
            PrintTheList(q2, $"Test {counter++}. Filter Name contains 'na'");

            //Complex filters using && and || operators 
            var q3 = from c in cities
                     where c.Length > 8 || c.StartsWith("B")
                     select c;
            PrintTheList(q3, $"Test {counter++}. Filter Name startswith 'B' or length > 8");

            var q4 = from c in cities
                     where c.Length > 8
                     select new
                     {
                         StartsWith = c[0],
                         Length = c.Length,
                         Name = c
                     };
            Console.WriteLine($"*********** Test {counter++}. Projection Query Syntax *****************");
            Console.WriteLine();
            foreach (var item in q4)
            {
                Console.WriteLine($"Starts With={item.StartsWith,-2}Length={item.Length:00} Name={item.Name}");
            }
            Console.WriteLine("\n******************************************************");
            Console.WriteLine();
        }


        static void SortingOperators()
        {
            // Orderby, OrderByDescending, ThenBy, ThenbyDescending, Reverse

            var q1 = from c in cities
                     orderby c[0] ascending
                     select c;
            PrintTheList(q1, $"Test {counter++}. Filter Name c[0].D & c[1]A");

            var q2 = cities
                .OrderBy(c => c[0]) // it will usually 1
                .ThenByDescending(c => c.Length) // any number of ThenByDescending
                .ThenByDescending(c => c[1])
                .Select(c => c);
            PrintTheList(q2, $"Test {counter++}. Filter Name c[0].D & c[1]A");

            var q3 = cities.Reverse<string>();
            PrintTheList(q3, $"Test {counter++}. Filter Name Reverse");
        }

        static void ElementOperators()
        {
            // First, Last, Single, ElementAt

            var first = cities.First();
            var last = cities.Last();
            // FirstOrDefault is written to avoid the exception and no data appears
            var firstMatching = cities.FirstOrDefault(c => c.Length > 20);
            // LastOrDefault is written to avoid the exception and no data appears
            var lastMatching = cities.LastOrDefault(c => c.Length < 10);

            Console.WriteLine($"Test {counter++}. First Last Operations");
            Console.WriteLine($"First : {first}\t:Last : {last}");
            Console.WriteLine($"Test {counter++}. First Last Matching Operations");
            Console.WriteLine($"FirstMatching : {firstMatching}\t:LastMatching : {lastMatching}");
        }

        static void AggregationOperators()
        {
            // Sum, Max, Min, Count,Averge
            // forces the immediate execution , others were deffered execution

            var sum = cities.Sum(c => c.Length);
            var avg = cities.Average(c => c.Length);
            var min = cities.Min(c => c.Length);
            var max = cities.Max(c => c.Length);
            var count = cities.Count();

            Console.WriteLine($"Test {counter++}. Aggregation Operators");
            Console.WriteLine($"Sum : {sum}");
            Console.WriteLine($"AVG : {avg}");
            Console.WriteLine($"Max : {max}");
            Console.WriteLine($"Min : {min}");
            Console.WriteLine($"count : {count}");
        }

        static void PartitionOperators()
        {
            var q1 = cities.Take(10);
            var q2 = cities.Skip(20);
            PrintTheList(q1, $"Test {counter++}. Take 10 items");
            PrintTheList(q2, $"Test {counter++}. TSkip 20 items");

            var q3 = cities.Skip(5).Take(25).Skip(15).Take(7).Skip(2);
            PrintTheList(q3, $"Test {counter++}. cities.Skip(5).Take(25).Skip(15).Take(7).Skip(2)");

            // Chunk Operator - divide the set into smaller parts
            var q4 = cities.Chunk(11);
            int i = 1;
            foreach( var sec in q4)
            {
                Console.WriteLine($"Chunk {i++}");
                foreach(var item in sec)
                {
                    Console.Write($"{item,-20}");
                }
                Console.WriteLine();
            }

            //TakeWHile -> takes the rows as long as the condition is true, stops when the condition is false 
            //Skip Hile -> skips the rows as long as the condition is true, starts when the condition becomes false

            var q5 = cities.TakeWhile(c => c.Length < 10);
            var q6 = cities.SkipWhile(c => c.Length < 10); 
            PrintTheList(q5, $"Test {counter++}. Take While Length < 10");
            PrintTheList(q6, $"Test {counter++}. Skip While Length < 10");
        }

        static void GroupingOperators()
        {
            var q1 = from c in cities
                     group c by c[0] into item
                     select item;
            //Console.WriteLine(q1);
            Console.WriteLine($"Cities grouped by first letter");
            foreach ( var grp in q1)
            {
                Console.WriteLine($"Group {grp.Key}");
                grp.ToList().ForEach(x => Console.WriteLine($"{x}"));
                Console.WriteLine();
            }
        }

        static string connStr =
    @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;
           multipleactiveresultsets=true";
        static void DataSetsWithLinq()
        {
            DataSet ds = new DataSet();
            var sqlText1 = "SELECT CategoryId, CategoryName, Description FROM Categories;";
            SqlConnection con = new SqlConnection(connStr);
            con.StateChange += (sender, args) =>
            {
                Console.WriteLine($"State changed to: {args.CurrentState} from {args.OriginalState}");
            };
            SqlDataAdapter adapter = new SqlDataAdapter(
                selectCommandText: sqlText1,
                selectConnection: con);
            adapter.Fill(ds, "Categories");
            var q = from row in ds.Tables["Categories"].AsEnumerable()
                    where row.Field<int>("CategoryId") < 5
                    select row;
            foreach (var item in q)
            {
                Console.WriteLine($"{item[0]} - {item[1]}");
            }
        }
    }
}