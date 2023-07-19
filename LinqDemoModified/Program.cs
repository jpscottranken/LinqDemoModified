using LINQLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//
//  Modified LINQ Demo From URL:
//  https://www.c-sharpcorner.com/article/linq-for-beginners/
//
namespace LinqDemoModified
{
    class Program
    {
        public static List<Employee> employees = new List<Employee>();
        public static List<Project> projects   = new List<Project>();

        static void Main(string[] args)
        {
            InitializeEmployees();
            InitializeProjects();

            //WHERE
            var querySyntax1 = from employee in employees
                               where employee.EmployeeName.EndsWith("k")
                               select employee.EmployeeName;

            Console.WriteLine("Where in querySyntax------");
            foreach (var item in querySyntax1)
            {
                Console.WriteLine(item);
            }

            var methodSyntax1 = employees.Where(e => e.EmployeeName.StartsWith("J"));
            Console.WriteLine("\nWhere in methodSyntax-----");
            foreach (var item in methodSyntax1)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine('\n');

            //ORDER BY ASCENDING
            var querySyntax2 = from employee in employees
                               orderby employee.EmployeeName
                               select employee.EmployeeName;


            var methodSyntax2 = employees.OrderBy(e => e.EmployeeName);

            Console.WriteLine("\nOrder by ascending in querySyntax------");
            foreach (var item in querySyntax2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nOrder by ascending in methodSyntax------");
            foreach (var item in methodSyntax2)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine('\n');

            //ORDER BY DESCENDING
            var querySyntax3 = from employee in employees
                               orderby employee.EmployeeName descending
                               select employee.EmployeeName;


            var methodSyntax3 = employees.OrderByDescending(e => e.EmployeeName);

            Console.WriteLine("\nOrder by descending in querySyntax------");
            foreach (var item in querySyntax3)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nOrder by descending in methodSyntax------");
            foreach (var item in methodSyntax3)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine('\n');

            //THEN BY
            var querySyntax4 = from employee in employees
                               orderby employee.ProjectId, employee.EmployeeName descending
                               select employee;


            var methodSyntax4 = employees.OrderBy(e => e.ProjectId).ThenByDescending(e => e.EmployeeName);

            Console.WriteLine("\nThen by in querySyntax------");
            foreach (var item in querySyntax4)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectId);
            }

            Console.WriteLine("\nThen by in methodSyntax------");
            foreach (var item in methodSyntax4)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectId);
            }

            Console.WriteLine('\n');

            //TAKE
            var querySyntax5 = (from employee in employees
                                select employee).Take(2);


            var methodSyntax5 = employees.Take(2);


            Console.WriteLine("\nTake in querySyntax------");
            foreach (var item in querySyntax5)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine("\nTake in methodSyntax------");
            foreach (var item in methodSyntax5)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine('\n');


            //SKIP
            var querySyntax6 = (from employee in employees
                                select employee).Skip(2);


            var methodSyntax6 = employees.Skip(2);

            Console.WriteLine("\nSkip in querySyntax------");
            foreach (var item in querySyntax6)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine("\nSkip in methodSyntax------");
            foreach (var item in methodSyntax6)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine('\n');

            //GROUP
            var querySyntax7 = from employee in employees
                               group employee by employee.ProjectId;


            var methodSyntax7 = employees.GroupBy(e => e.ProjectId);

            Console.WriteLine("\nGroup in querySyntax------");
            foreach (var item in querySyntax7)
            {
                Console.WriteLine(item.Key + ":" + item.Count());
            }

            Console.WriteLine("\nGroup in methodSyntax------");
            foreach (var item in methodSyntax7)
            {
                Console.WriteLine(item.Key + ":" + item.Count());
            }

            Console.WriteLine('\n');

            //FIRST
            var querySyntax8 = (from employee in employees
                                    //where employee.EmployeeName.StartsWith("Q")
                                select employee).First();

            var methodSyntax8 = employees
                                //.Where(e => e.EmployeeName.StartsWith("Q"))                 
                                .First();

            Console.WriteLine("\nFirst in querySyntax------");
            if (querySyntax8 != null)
            {
                Console.WriteLine(querySyntax8.EmployeeName);
            }

            Console.WriteLine("\nFirst in methodSyntax------");
            if (methodSyntax8 != null)
            {
                Console.WriteLine(methodSyntax8.EmployeeName);
            }

            Console.WriteLine('\n');

            //FIRST OR DEFAULT
            var querySyntax9 = (from employee in employees
                                    //where employee.EmployeeName.StartsWith("Q")
                                select employee).FirstOrDefault();

            var methodSyntax9 = employees
                                //.Where(e => e.EmployeeName.StartsWith("Q"))
                                .FirstOrDefault();

            Console.WriteLine("\nFirst or default in querySyntax------");
            if (querySyntax9 != null)
            {
                Console.WriteLine(querySyntax9.EmployeeName);
            }

            Console.WriteLine("\nFirst or default in methodSyntax------");
            if (methodSyntax9 != null)
            {
                Console.WriteLine(methodSyntax9.EmployeeName);
            }

            Console.WriteLine('\n');

            //JOIN
            var querySyntax10 = from employee in employees
                                join project in projects on employee.ProjectId equals project.ProjectId
                                select new { employee.EmployeeName, project.ProjectName };

            var methodSyntax10 = employees.Join(projects,
                                              e => e.ProjectId,
                                              p => p.ProjectId,
                                              (e, p) => new { e.EmployeeName, p.ProjectName });

            Console.WriteLine("\nJoin in querySyntax------");
            foreach (var item in querySyntax10)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectName);
            }


            Console.WriteLine("\nJoin in methodSyntax------");
            foreach (var item in methodSyntax10)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectName);
            }

            Console.WriteLine('\n');

            //LEFT JOIN
            var querySyntax11 = from employee in employees
                                join project in projects on employee.ProjectId equals project.ProjectId into group1
                                from project in group1.DefaultIfEmpty()
                                select new { employee.EmployeeName, ProjectName = project?.ProjectName ?? "NULL" };



            Console.WriteLine("\nLeft Join in querySyntax------");
            foreach (var item in querySyntax11)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectName);
            }

            Console.WriteLine('\n');
            Console.ReadLine();
        }

        public static void InitializeEmployees()
        {
            employees.Add(new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Scott, Jeff",
                ProjectId = 100
            });

            employees.Add(new Employee
            {
                EmployeeId = 2,
                EmployeeName = "Jones, Sally",
                ProjectId = 101
            });

            employees.Add(new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Kramer, Mark",
                ProjectId = 100
            });

            employees.Add(new Employee
            {
                EmployeeId = 4,
                EmployeeName = "Green, Carol",
                ProjectId = 100
            });

            employees.Add(new Employee
            {
                EmployeeId = 5,
                EmployeeName = "Black, Jack",
                ProjectId = 101
            });
            employees.Add(new Employee
            {
                EmployeeId = 6,
                EmployeeName = "Palmer, Martha",
                ProjectId = 101
            });

            employees.Add(new Employee
            {
                EmployeeId = 7,
                EmployeeName = "Kent, Ron",
                ProjectId = 100
            });
        }

        public static void InitializeProjects()
        {
            projects.Add(new Project
            {
                ProjectId = 100,
                ProjectName = "Company Website"

            });

            projects.Add(new Project
            {
                ProjectId = 101,
                ProjectName = "C# Payroll Program"

            });
        }
    }
}
