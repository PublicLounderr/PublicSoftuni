using EFCoreIntroduction.Data;
using System;
using System.Text;

namespace EFCoreIntroduction
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();
            Console.WriteLine(GetEmployeesInPeriod(context));
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context
                .Employees
                .Where(e => e.Projects
                    .Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
                .Take(10);

            StringBuilder output = new StringBuilder();
            foreach (var employee in employees)
            {
                output.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.Manager!.FirstName} {employee.Manager.LastName}");

                foreach (var project in employee.Projects)
                {
                    output.AppendLine($"--{project.Name} - {project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - {project.EndDate!.Value.ToString("yyyy-MM-dd hh:mm:ss")}");
                }
            }



            return output.ToString();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Data.Models.Address { TownId = 4, AddressText = "Vitoshka 15" };
            context.Addresses.Add(address);
            var nakov = context.Employees.Where(e => e.LastName == "Nakov").First();
            address.Employees.Add(nakov);

            context.SaveChanges();

            return String.Join(Environment.NewLine, context.Addresses.OrderByDescending(x => x.AddressId).Take(10).Select(x=> x.AddressText));
        }
    }
}