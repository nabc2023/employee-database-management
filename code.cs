using System;
using System.Collections.Generic;
using System.IO;

class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }
}

class EmployeeDatabase
{
    private List<Employee> employees = new List<Employee>();
    private const string DatabaseFile = "employee_database.txt";

    public void AddEmployee(Employee emp)
    {
        employees.Add(emp);
    }

    public void SaveDatabase()
    {
        using (var writer = new StreamWriter(DatabaseFile))
        {
            foreach (var emp in employees)
            {
                writer.WriteLine($"{emp.EmployeeId},{emp.Name},{emp.Salary}");
            }
        }
    }

    public void LoadDatabase()
    {
        if (!File.Exists(DatabaseFile))
            return;

        employees.Clear();

        using (var reader = new StreamReader(DatabaseFile))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var data = line.Split(',');
                if (data.Length == 3 &&
                    int.TryParse(data[0], out int id) &&
                    double.TryParse(data[2], out double salary))
                {
                    employees.Add(new Employee { EmployeeId = id, Name = data[1], Salary = salary });
                }
            }
        }
    }

    public void PrintAllEmployees()
    {
        foreach (var emp in employees)
        {
            Console.WriteLine($"ID: {emp.EmployeeId}, Name: {emp.Name}, Salary: {emp.Salary}");
        }
    }
}

class Program
{
    static void Main()
    {
        var database = new EmployeeDatabase();
        database.LoadDatabase();

        // Add employees and manipulate the database

        database.SaveDatabase();
    }
}
