using ControlingProjectApp.Data.Entities;
using ControlingProjectApp.Services;

namespace ControlingProjectApp.Components.ProviderData;

public abstract class DataProvider : UserSubMenuBase
{

    protected static IEnumerable<Employee> GenerateEmployees()
    {

        var employees = new List<Employee>()
        {
             new ()
             {
                 FirstName = "Łukasz",
                 LastName = "Zielony",
                 JobPositon = JobPosition.Employee,
                 EmploymentDate = new DateTime(1980, 01, 01),
                 HourlyRate = SetHourlyRate(JobPosition.Employee),
             },
             new ()
             {
                  FirstName = "Robert",
                  LastName = "Czerwony",
                  JobPositon = JobPosition.Employee,
                  EmploymentDate = new DateTime(1990, 04, 02),
                  HourlyRate = SetHourlyRate(JobPosition.Employee),
             },
              new ()
             {
                  FirstName = "Hanna",
                  LastName = "Niebieska",
                  JobPositon = JobPosition.Employee,
                  EmploymentDate = new DateTime(1993, 11, 03),
                  HourlyRate = SetHourlyRate(JobPosition.Employee),
             },
              new ()
             {
                  FirstName = "Grzegorz",
                  LastName = "Biały",
                  JobPositon = JobPosition.Engineer,
                  EmploymentDate = new DateTime(1995, 07, 04),
                  HourlyRate = SetHourlyRate(JobPosition.Engineer),
             },
              new ()
             {
                  FirstName = "Katarzyna",
                  LastName = "Purpurowa",
                  JobPositon = JobPosition.Engineer,
                  EmploymentDate = new DateTime(1999, 12, 05),
                  HourlyRate = SetHourlyRate(JobPosition.Engineer),
             },
             new ()
             {
                  FirstName = "Łukasz",
                  LastName = "Seledynowy",
                  JobPositon = JobPosition.Engineer,
                  EmploymentDate = new DateTime(2001, 08, 06),
                  HourlyRate =SetHourlyRate(JobPosition.Engineer),
             },
             new ()
             {
                  FirstName = "Agnieszka",
                  LastName = "Brązowa",
                  JobPositon = JobPosition.Engineer,
                  EmploymentDate = new DateTime(1970, 07, 07),
                  HourlyRate = SetHourlyRate(JobPosition.Engineer),
             },
             new ()
             {
                  FirstName = "Agnieszka",
                  LastName = "Bananowa",
                  JobPositon = JobPosition.Engineer,
                  EmploymentDate = new DateTime(1994, 03, 08),
                  HourlyRate = SetHourlyRate(JobPosition.Engineer),
             },
             new ()
             {
                  FirstName = "Adrian",
                  LastName = "Beżowy",
                  JobPositon = JobPosition.Engineer,
                  EmploymentDate = new DateTime(1994, 08, 09),
                  HourlyRate = SetHourlyRate(JobPosition.Engineer),
             },
             new ()
             {
                  FirstName = "Wojciech",
                  LastName = "Pomarańczowy",
                  JobPositon = JobPosition.Employee,
                  EmploymentDate = new DateTime(2002, 01, 10),
                  HourlyRate =SetHourlyRate(JobPosition.Employee),
             },
             new ()
             {
                  FirstName = "Tomasz",
                  LastName = "Czarny",
                  JobPositon = JobPosition.Manager,
                  EmploymentDate = new DateTime(1986, 07, 11),
                  HourlyRate = SetHourlyRate(JobPosition.Manager),
             },
             new ()
             {
                  FirstName = "Tomasz",
                  LastName = "Szary",
                  JobPositon = JobPosition.Manager,
                  EmploymentDate = new DateTime(1991, 09, 12),
                  HourlyRate = SetHourlyRate(JobPosition.Manager),
             },
              new ()
             {
                  FirstName = "Alicja",
                  LastName = "Lawendowa",
                  JobPositon = JobPosition.Manager,
                  EmploymentDate = new DateTime(1992, 06, 13),
                  HourlyRate = SetHourlyRate(JobPosition.Manager),
             },
             new ()
             {
                  FirstName = "Karol",
                  LastName = "Kawowy",
                  JobPositon = JobPosition.Supervisor,
                  EmploymentDate = new DateTime(1990, 12, 14),
                  HourlyRate = SetHourlyRate(JobPosition.Supervisor),
             },
        };
        return employees;
    }

    protected static IEnumerable<Project> GenerateProjects()
    {
        var projects = new List<Project>()
        {
             new()
             {
                Name = "Projekt1",
                Description = "Opis1",
                Budget = 1_000_000.00m,
                BeginDate = new DateTime(2023, 01, 01),
                EndDate = new DateTime(2026, 01, 01)
              },
             new()
             {
                Name = "Projekt2",
                Description = "Opis2",
                Budget = 200_000.00m,
                BeginDate = new DateTime(2024, 01, 02),
                EndDate = new DateTime(2025, 01, 02)
              },
             new()
             {
                Name = "Projekt3",
                Description = "Opis3",
                Budget = 300_000.00m,
                BeginDate = new DateTime(2023, 11, 03),
                EndDate = new DateTime(2025, 01, 03)
              },
             new()
             {
                Name = "Projekt4",
                Description = "Opis4",
                Budget = 400_000.00m,
                BeginDate = new DateTime(2023, 02, 04),
                EndDate = new DateTime(2025, 09, 04)
              },
             new()
             {
                Name = "Projekt5",
                Description = "Opis5",
                Budget = 50_000.00m,
                BeginDate = new DateTime(2023, 11, 05),
                EndDate = new DateTime(2024, 07, 05)
              },
             new()
             {
                Name = "Projekt6",
                Description = "Opis6",
                Budget = 23_000.00m,
                BeginDate = new DateTime(2024, 02, 06),
                EndDate = new DateTime(2024, 09, 06)
              },
             new()
             {
                Name = "Projekt7",
                Description = "Opis7",
                Budget = 77_000.00m,
                BeginDate = new DateTime(2024, 03, 07),
                EndDate = new DateTime(2024, 12, 07),
              },

        };
        return projects;
    }
}

