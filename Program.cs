using ControlingProjectApp.Entities;
using ControlingProjectApp.Repositories;
using ControlingProjectApp.Data;
using ControlingProjectApp;

var employeesRepository = new SqlRepository<Employee>(new ControlingProjectAppDbContext());
var projectsRepository = new SqlRepository<Project>(new ControlingProjectAppDbContext());

AddEmployees(employeesRepository);
AddProjects(projectsRepository);

Console.WriteLine($"Lista pracowników:");
WriteAllToConsole(employeesRepository);
Console.ReadKey();
Console.Clear();
Console.WriteLine($"Lista projektów:");
WriteAllToConsole(projectsRepository);
Console.ReadKey();

static void AddEmployees(IRepository<Employee> employeesrepository)
{
    employeesrepository.Add(new Employee
    {
        FirstName = "Imie1",
        LastName = "Nazwisko1",
        JobPositon = JobPosition.Employee,
        EmploymentDate = new DateOnly(1980, 01, 04),
        HourlyRate = SetHourlyRate(JobPosition.Employee)
    });
    employeesrepository.Add(new Employee
    {
        FirstName = "Imie2",
        LastName = "Nazwisko2",
        JobPositon = JobPosition.Engineer,
        EmploymentDate = new DateOnly(1992, 02, 02),
        HourlyRate = SetHourlyRate(JobPosition.Engineer)
    });
    employeesrepository.Add(new Employee
    {
        FirstName = "Imie3",
        LastName = "Nazwisko3",
        JobPositon = JobPosition.Manager,
        EmploymentDate = new DateOnly(1993, 03, 03),
        HourlyRate = SetHourlyRate(JobPosition.Manager)
    });
    employeesrepository.Add(new Employee
    {
        FirstName = "Imie4",
        LastName = "Nazwisko4",
        JobPositon = JobPosition.Supervisor,
        EmploymentDate = new DateOnly(1994, 04, 04),
        HourlyRate = SetHourlyRate(JobPosition.Supervisor)
    });
    employeesrepository.Add(new Employee
    {
        FirstName = "Imie5",
        LastName = "Nazwisko5",
        JobPositon = JobPosition.Engineer,
        EmploymentDate = new DateOnly(1995, 05, 05),
        HourlyRate = SetHourlyRate(JobPosition.Engineer)
    });
    employeesrepository.Save();
}

static void AddProjects(IRepository<Project> projectsrepository)
{
    projectsrepository.Add(new Project
    {
        Name = "Projekt1",
        Description = "Opis1",
        Budget = 1000000.00m,
        BeginDate = new DateOnly(1980, 01, 04),
        EndDate = new DateOnly(1982, 01, 04)
    });
    projectsrepository.Add(new Project
    {
        Name = "Projekt2",
        Description = "Opis2",
        Budget = 200000.00m,
        BeginDate = new DateOnly(1992, 02, 02),
        EndDate = new DateOnly(1993, 01, 03)
    });
    projectsrepository.Add(new Project
    {
        Name = "Projekt3",
        Description = "Opis3",
        Budget = 300000.00m,
        BeginDate = new DateOnly(1993, 03, 03),
        EndDate = new DateOnly(1995, 03, 03)
    });
    projectsrepository.Save();
}

static void WriteAllToConsole(IReadRepository<EntityBase> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }

}

static decimal SetHourlyRate(JobPosition jobposition)
{
    var hourly = 0.0m; ;
    switch (jobposition)
    {
        case JobPosition.Employee:
            hourly = 100.0m;
            break;
        case JobPosition.Engineer:
            hourly = 200.0m;
            break;
        case JobPosition.Manager:
            hourly = 300.0m;
            break;
        case JobPosition.Supervisor:
            hourly = 400.0m;
            break;
    }
    return hourly;
}