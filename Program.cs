using ControlingProjectApp.Entities;
using ControlingProjectApp.Repositories;
using ControlingProjectApp.Data;
using System.Text;
using System.Globalization;
using System.Text.Json;

const string logFileName = "log.txt";
const string EmployeesFileName = "Employess.json";
const string ProjectsFileName = "Projects.json";

MainUserInterface();

File.Delete(EmployeesFileName);
File.Delete(ProjectsFileName);
var employessJson = JsonSerializer.Serialize(new SqlRepository<Employee>(new ControlingProjectAppDbContext()).GetAll());
var projectsJson = JsonSerializer.Serialize(new SqlRepository<Project>(new ControlingProjectAppDbContext()).GetAll());
File.WriteAllText(EmployeesFileName, employessJson);
File.WriteAllText(ProjectsFileName, projectsJson);

void MainUserInterface()
{
    var endProgramm = false;
    while (!endProgramm)
    {
        Console.Clear();
        Console.WriteLine();
        Console.Title = "Controling Project";
        Console.WriteLine($"\t      Witamy w programie do kontroli prowadzonych projektów");
        Console.WriteLine($"\t=====================================================================\n");
        Console.WriteLine($"\t1   - Wyświetl zawartość danych\n");
        Console.WriteLine($"\t2   - Dodaj dane\n");
        Console.WriteLine($"\t3   - Szukaj dane \n");
        Console.WriteLine($"\t4   - Usuń dane \n");
        Console.WriteLine($"\tQ/q - Wyjdż z programu\n");
        Console.WriteLine($"\t=====================================================================\n");
        Console.Write($"\tWybierz odpowiednią opcję: ");

        string? menuOption = Console.ReadLine();

        switch (menuOption)
        {
            case "1":
            case "2":
            case "3":
            case "4":
                DisplaySubMenu(menuOption);
                break;
            case "Q":
            case "q":
                endProgramm = true;
                break;
            default:
                Console.Clear();
                break;
        }
    }
}

void DisplaySubMenu(string menuOption)
{
    var endMethod = false;
    while (!endMethod)
    {
        Console.Clear();

        switch (menuOption)
        {
            case "1":
                Console.WriteLine($"\t\t\t Wyświetl dane");
                Console.WriteLine($"\t=====================================================================\n");
                break;
            case "2":
                Console.WriteLine($"\t\t\t Dodaj do danych");
                Console.WriteLine($"\t=====================================================================\n");
                break;
            case "3":
                Console.WriteLine($"\t\t\t Wyszukaj dane");
                Console.WriteLine($"\t=====================================================================\n");
                break;
            case "4":
                Console.WriteLine($"\t\t\t Usuń dane");
                Console.WriteLine($"\t=====================================================================\n");
                break;
            default:
                break;
        }
        Console.WriteLine($"\t1   - Pracowników\n");
        Console.WriteLine($"\t2   - Projektów\n");
        Console.WriteLine($"\tQ/q - Wyjście\n");
        Console.WriteLine($"\t=====================================================================\n");
        Console.Write($"\tWybierz odpowiednią opcję: ");

        string? subMenuOption = Console.ReadLine();

        switch (subMenuOption)
        {
            case "1":
                var employeesRepository = new SqlRepository<Employee>(new ControlingProjectAppDbContext());
                switch (menuOption)
                {
                    case "1":
                        WriteAllToConsole(employeesRepository);
                        WaitForKeyPress();
                        break;
                    case "2":
                        employeesRepository.ItemAdded += RepositoryOnItemAdded;
                        AddNewEmployee(employeesRepository);
                        break;
                    case "3":
                        FindItemById(employeesRepository);
                        break;
                    case "4":
                        employeesRepository.ItemRemoved += RepositoryOnItemRemoved;
                        RemoveItem(employeesRepository);
                        break;
                }
                break;
            case "2":
                var projectsRepository = new SqlRepository<Project>(new ControlingProjectAppDbContext());
                switch (menuOption)
                {
                    case "1":
                        WriteAllToConsole(projectsRepository);
                        WaitForKeyPress();
                        break;
                    case "2":
                        projectsRepository.ItemAdded += RepositoryOnItemAdded;
                        AddNewProject(projectsRepository);
                        break;
                    case "3":
                        FindItemById(projectsRepository);
                        break;
                    case "4":
                        projectsRepository.ItemRemoved += RepositoryOnItemRemoved;
                        RemoveItem(projectsRepository);
                        break;
                }
                break;
            case "Q":
            case "q":
                endMethod = true;
                break;
            default:
                Console.Clear();
                break;
        }
    }
}

static void WriteAllToConsole<T>(IRepository<T> repository) where T : class, IEntity
{

    Type entityType = typeof(T);

    Console.Clear();
    if (entityType.Name == "Employee")
    {
        Console.WriteLine($"\tLista pracowników:");
    }
    else if (entityType.Name == "Project")
    {
        Console.WriteLine($"\tLista pojektów");
    }
   
    var items = repository.GetAll();

    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

void AddNewEmployee(IRepository<Employee> employeesRepository)
{
    var emp = GetDataEmployeeFromUser();
    if (emp == null)
    {
        return;
    }
    else
    {
        employeesRepository.Add(emp);
    }

}
void AddNewProject(IRepository<Project> employeesRepository)
{
    var emp = GetDataProjectFromUser();
    if (emp == null)
    {
        return;
    }
    else
    {
        employeesRepository.Add(emp);
    }

}

static Employee? GetDataEmployeeFromUser()
{
    var sb = new StringBuilder();
    string? inputFirstName = null;
    string? inputLastName = null;
    int jobposition = 0;
    var employmentDate = new DateOnly();

    int index = 0;
    var endMethod = false;

    while (!endMethod)
    {
        while (index < 1)
        {
            Console.Clear();
            sb = sb.Clear(); ;
            sb.AppendLine($"\t\tWprowadzanie danych pracownika");
            sb.AppendLine($"\t=======================================================================================\n");

            sb.Append($"\tPodaj imię pracownika:\t\t\t\t\t\t\t");
            Console.Clear();
            Console.Write(sb);
            var input = Console.ReadLine();
            if (IsInputStringValid(input))
            {
                ConversionStringFirstCapitalLetterOnly(ref input);
                inputFirstName = input;
                sb.AppendLine($"{inputFirstName}");
                index++;
            }

            else
            {
                switch (DisplaySelectionWithInvalidData())
                {
                    case "Q":
                    case "q":
                        return null;
                    default:
                        break;
                }
            }
        }
        sb.Append($"\tPodaj nazwisko pracownika:\t\t\t\t\t\t");
        while (index < 2)
        {
            Console.Clear();
            Console.Write(sb);
            var input = Console.ReadLine();
            if (IsInputStringValid(input))
            {
                ConversionStringFirstCapitalLetterOnly(ref input);
                inputLastName = input;
                sb.AppendLine($"{inputLastName}");
                index++;
            }

            else
            {
                switch (DisplaySelectionWithInvalidData())
                {
                    case "Q":
                    case "q":
                        return null;
                    default:
                        break;
                }
            }
        }
        sb.Append($"\tPodaj stanowisko (1-pracownik, 2-inżynier, 3-kierownik, 4-manager):\t");
        while (index < 3)
        {
            Console.Clear();
            Console.Write(sb);
            var input = Console.ReadLine();

            var isNumber = int.TryParse(input, out jobposition);
            if (isNumber && jobposition >= 1 && jobposition <= 4)
            {
                sb.AppendLine($"{jobposition}");
                index++;
            }

            else
            {
                switch (DisplaySelectionWithInvalidData())
                {
                    case "Q":
                    case "q":
                        return null;
                    default:
                        break;
                }
            }
        }
        sb.Append($"\tPodaj datę zatrudnienia (dd.mm.rrrr):\t\t\t\t\t");
        while (index < 4)
        {
            Console.Clear();
            Console.Write(sb);
            var inputDate = Console.ReadLine();

            if (DateOnly.TryParseExact(inputDate, "d", CultureInfo.CurrentCulture, 0, out employmentDate))
            {
                sb.Append($"{employmentDate}");
                index++;
            }

            else
            {
                switch (DisplaySelectionWithInvalidData())
                {
                    case "Q":
                    case "q":
                        return null;
                    default:
                        break;
                }
            }
        }

        switch (DisplaySelectionWithData())
        {
            case "Y":
            case "y":
                var emp1 = new Employee
                {
                    FirstName = inputFirstName,
                    LastName = inputLastName,
                    JobPositon = (JobPosition)jobposition,
                    EmploymentDate = new DateTime(employmentDate.Year, employmentDate.Month, employmentDate.Day, 0, 0, 0),
                    HourlyRate = SetHourlyRate((JobPosition)jobposition)
                };
                return emp1;
            case "N":
            case "n":
                index = 0;
                break;
            default:
                endMethod = true;
                break;
        }
    }
    return null;
}

static Project? GetDataProjectFromUser()
{
    var sb = new StringBuilder();
    string? inputName = null;
    string? inputDescription = null;
    decimal inputBudget = 0.0m;
    var beginDate = new DateOnly();
    var endDate = new DateOnly();

    int index = 0;
    var endMethod = false;

    while (!endMethod)
    {
        while (index < 1)
        {
            Console.Clear();
            sb = sb.Clear(); ;
            sb.AppendLine($"\t\tWprowadzanie danych projektu");
            sb.AppendLine($"\t=======================================================================================\n");

            sb.Append($"\tPodaj nazwę projektu:\t\t\t\t");
            Console.Clear();
            Console.Write(sb);
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                ConversionStringFirstCapitalLetterOnly(ref input);
                inputName = input;
                sb.AppendLine($"{inputName}");
                index++;
            }
            else
            {
                switch (DisplaySelectionWithInvalidData())
                {
                    case "Q":
                    case "q":
                        return null;
                    default:
                        break;
                }
            }
        }
        sb.Append($"\tPodaj opis projeku:\t\t\t\t");
        while (index < 2)
        {
            Console.Clear();
            Console.Write(sb);
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                ConversionStringFirstCapitalLetterOnly(ref input);
                inputDescription = input;
                sb.AppendLine($"{inputDescription}");
                index++;
            }
            else
            {
                switch (DisplaySelectionWithInvalidData())
                {
                    case "Q":
                    case "q":
                        return null;
                    default:
                        break;
                }
            }
        }

        sb.Append($"\tPodaj budżet projektu:\t\t\t\t");

        while (index < 3)
        {
            Console.Clear();
            Console.Write(sb);
            var input = Console.ReadLine();
            if (Decimal.TryParse(input, out var result))
            {
                inputBudget = result;
                sb.AppendLine($"{inputBudget}");
                index++;
            }

            else
            {
                switch (DisplaySelectionWithInvalidData())
                {
                    case "Q":
                    case "q":
                        return null;
                    default:
                        break;
                }
            }
        }
        sb.Append($"\tPodaj datę początkową projektu (dd.mm.rrrr):\t");
        while (index < 4)
        {
            Console.Clear();
            Console.Write(sb);
            var inputDate = Console.ReadLine();

            if (DateOnly.TryParseExact(inputDate, "d", CultureInfo.CurrentCulture, 0, out beginDate))
            {
                sb.AppendLine($"{beginDate}");
                index++;
            }

            else
            {
                switch (DisplaySelectionWithInvalidData())
                {
                    case "Q":
                    case "q":
                        return null;
                    default:
                        break;
                }
            }
        }
        sb.Append($"\tPodaj datę końcową projektu (dd.mm.rrrr):\t");
        while (index < 5)
        {
            Console.Clear();
            Console.Write(sb);
            var inputDate = Console.ReadLine();

            if (DateOnly.TryParseExact(inputDate, "d", CultureInfo.CurrentCulture, 0, out endDate))
            {
                sb.AppendLine($"{endDate}");
                index++;
            }

            else
            {
                switch (DisplaySelectionWithInvalidData())
                {
                    case "Q":
                    case "q":
                        return null;
                    default:
                        break;
                }
            }
        }
        switch (DisplaySelectionWithData())
        {
            case "Y":
            case "y":
                var proj = new Project
                {
                    Name = inputName,
                    Description = inputDescription,
                    Budget = inputBudget,
                    BeginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0),
                    EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0),
                };
                return proj;
            case "N":
            case "n":
                index = 0;
                break;
            default:
                endMethod = true;
                break;
        }
    }
    return null;
}

T? FindItemById<T>(IRepository<T> repository) where T : class, IEntity
{
    var endMethod = false;
    Type entityType = typeof(T);

    while (!endMethod)
    {
        Console.Clear();
        if (entityType.Name == "Employee")
        {
            Console.WriteLine($"\t\tWyszukiwanie Pracownika");
            Console.WriteLine($"\t=======================================================================================\n");
        }
        else if (entityType.Name == "Project")
        {
            Console.WriteLine($"\t\tWyszukiwanie Projektu");
            Console.WriteLine($"\t=======================================================================================\n");
        }

        Console.Write($"\tPodaj numer Id:\t");
        var input = Console.ReadLine();
        var isNumber = int.TryParse(input, out var inputId);
        if (isNumber)
        {
            var item = repository.GetById(inputId);
            if (item != null)
            {
                Console.Write(item.ToString());
                WaitForKeyPress();
                return item;
            }
            else
            {
                Console.WriteLine($"\tBrak podanego Id w bazie");
                WaitForKeyPress();
            }
        }
        else
        {
            switch (DisplaySelectionWithInvalidData())
            {
                case "Q":
                case "q":
                    return null;
                default:
                    break;
            }
        }
    }
    return null;
}

void RemoveItem<T>(IRepository<T> repository) where T : class, IEntity
{
    var endMethod = false;
    Type entityType = typeof(T);

    while (!endMethod)

    {
        Console.Clear();
        if (entityType.Name == "Employee")
        {
            Console.WriteLine($"\t\tUsuwanie Pracownika");
            Console.WriteLine($"\t=======================================================================================\n");
        }
        else if (entityType.Name == "Project")
        {
            Console.WriteLine($"\t\tUsuwanie Projektu");
            Console.WriteLine($"\t=======================================================================================\n");
        }
        Console.Write($"\tPodaj numer Id:\t");
        var input = Console.ReadLine();

        var isNumber = int.TryParse(input, out var inputId);
        if (isNumber)
        {
            var item = repository.GetById(inputId);
            if (item != null)
            {
                repository.Remove(item);
                return;
            }
            else
            {
                Console.WriteLine($"\tBrak podanego Id w bazie");
                WaitForKeyPress();
            }
        }
        else
        {
            switch (DisplaySelectionWithInvalidData())
            {
                case "Q":
                case "q":
                    endMethod = true;
                    break;
                default:
                    break;
            }
        }
    }
}

void RepositoryOnItemAdded<T>(object? sender, T? e)
{
    Type entityType = typeof(T);
    string infoMessage = "";
    Console.Clear();
    if (entityType.Name == "Employee")
    {
        infoMessage = ($"\nPracownik:\n {e} \nzostał dodany do bazy");
    }
    else if (entityType.Name == "Project")
    {
        infoMessage = ($"\nProjekt:\n {e} \nzostał dodany do bazy");
    }
    Console.WriteLine(infoMessage);
    AddToLogFile(infoMessage);
    WaitForKeyPress();
}
void RepositoryOnItemRemoved<T>(object? sender, T? e)
{
    Type entityType = typeof(T);
    string infoMessage = "";
    Console.Clear();
    if (entityType.Name == "Employee")
    {
        infoMessage = ($"\nPracownik:\n {e} \nzostał usunięty z bazy");
    }
    else if (entityType.Name == "Project")
    {
        infoMessage = ($"\nProjekt:\n {e} \nzostał usunięty z bazy");
    }
    Console.WriteLine(infoMessage);
    AddToLogFile(infoMessage);
    WaitForKeyPress();
}


void AddToLogFile(string infoMessage)
{
    using var writer = File.AppendText(logFileName);
    writer.WriteLine($"{infoMessage} {DateTime.Now}");
}


static void ConversionStringFirstCapitalLetterOnly(ref string inputstring)
{
    inputstring = char.ToUpper(inputstring[0]) + inputstring[1..].ToLower();
}

static string? DisplaySelectionWithData()
{
    Console.Write($"\n\n\t(Y/y - potwierdź, N/n- wprowadzanie nowego, Wyjście- dowolny klawisz): ");
    return Console.ReadLine();
}

static string? DisplaySelectionWithInvalidData()
{
    Console.Write($"\n\tNiewłaściwe dane (Ponowne wprowadzenie- dowolny klawisz, Q/q- rezygnacja): ");
    return Console.ReadLine();
}

static bool IsStringWithPolishLettersOnly(string inputstring)
{
    foreach (char c in inputstring)
    {
        if (!((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c >= 260 && c <= 263) || (c >= 321 && c <= 324) ||

            (c >= 377 && c <= 380) || (c == 211) || (c >= 280 && c <= 281) || (c >= 346 && c <= 347) || (c == 243)))
        {
            return false;
        }
    }
    return true;
}

static bool IsInputStringValid(string inputstring)
{
    return IsStringWithPolishLettersOnly(inputstring) && !string.IsNullOrEmpty(inputstring);
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

void WaitForKeyPress()
{
    Console.Write($"\n\tNaciśnij dowolny klawisz ");
    Console.ReadKey();
}
