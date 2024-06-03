using ControlingProjectApp.Data.Entities;
using ControlingProjectApp.Data.Repositories;
using ControlingProjectApp.Services.InquiryData;
using System.Text;

namespace ControlingProjectApp.Services;

public class EmployeeData : ItemDataBase, IEmployeeData
{

    private readonly IRepository<Employee> _employeesRepository;
    private readonly IInquiryProviderForEmployees _inquiryProviderForEmployees;

    public EmployeeData(IRepository<Employee> employeesRepository, IInquiryProviderForEmployees inquiryProviderForEmployees)
    {
        _employeesRepository = employeesRepository;
        _inquiryProviderForEmployees = inquiryProviderForEmployees;
    }

    public void EmployeeDataHandling(string menuOption)
    {

        switch (menuOption)
        {
            case "1":
                WriteAllToConsole(_employeesRepository);
                break;
            case "2":
                AddNewEmployee();
                break;
            case "3":
                FindItemById(_employeesRepository);
                break;
            case "4":
                RemoveItem(_employeesRepository);
                break;
            case "5":
                _inquiryProviderForEmployees.GetInquiryForEmployess();
                break;
            case "6":
                UpdateEmployee();
                break;
        }
    }

    private void AddNewEmployee()
    {
        var sb = new StringBuilder();
        var endMethod = false;

        while (!endMethod)
        {
            sb = sb.Clear();
            sb.AppendLine($"\t\tWprowadzanie danych pracownika");
            sb.AppendLine($"\t=======================================================================================\n");
            UpdateConsole(sb);

            sb.Append($"\n\tPodaj imię pracownika:\t\t\t\t\t\t\t");
            UpdateConsole(sb);
            string? inputFirstName = GetRestricStringFromConsole(sb);
            if (inputFirstName == null)
            {
                return;
            }
            ConversionStringFirstCapitalLetterOnly(ref inputFirstName!);
            sb.Append($"{inputFirstName}");

            sb.Append($"\n\tPodaj nazwisko pracownika:\t\t\t\t\t\t");
            UpdateConsole(sb);
            string? inputLastName = GetRestricStringFromConsole(sb);
            if (inputLastName == null)
            {
                return;
            }
            ConversionStringFirstCapitalLetterOnly(ref inputLastName!);
            sb.Append($"{inputLastName}");

            sb.Append($"\n\tPodaj stanowisko (1-pracownik, 2-inżynier, 3-kierownik, 4-manager):\t");
            UpdateConsole(sb);
            int? jobposition = GetIntWithLimitsFromConsole(sb, (int)JobPosition.Employee, (int)JobPosition.Supervisor);
            if (jobposition == null)
            {
                return;
            }
            sb.AppendLine($"{jobposition}");

            sb.Append($"\tPodaj datę zatrudnienia (dd.mm.rrrr):\t\t\t\t\t");
            UpdateConsole(sb);
            DateOnly employmentDate = GetDateFromConsole(sb);
            if (employmentDate == DateOnly.MinValue)
            {
                return;
            }
            sb.Append($"{employmentDate}");

            switch (DisplaySelectionWithData())
            {
                case "Y":
                case "y":
                    var employee = new Employee
                    {
                        FirstName = inputFirstName,
                        LastName = inputLastName,
                        JobPositon = (JobPosition)jobposition,
                        EmploymentDate = new DateTime(employmentDate.Year, employmentDate.Month, employmentDate.Day, 0, 0, 0),
                        HourlyRate = SetHourlyRate((JobPosition)jobposition)
                    };
                    _employeesRepository.Add(employee);
                    WaitForKeyPress();
                    return;
                case "N":
                case "n":
                    break;
                default:
                    endMethod = true;
                    break;
            }
        }
    }

    private void UpdateEmployee()
    {
        var sb = new StringBuilder();
        var employee = FindItemById(_employeesRepository);
        string? inputFirstName = null;
        string? inputLastName = null;
        int changeIndex;
        var endMethod = false;

        while (!endMethod)
        {
            changeIndex = 0;
            Console.Clear();
            sb = sb.Clear();
            sb.AppendLine($"\t\tModyfikowanie danych pracownika");
            sb.AppendLine($"\t=======================================================================================\n");
            sb = sb.AppendLine(employee!.ToString());
            UpdateConsole(sb);

            Console.Write($" \n\n Czy chcesz zmienić imię?");
            var input = DisplaySelectionUpdateData();
            sb.Append($"\n\tPodaj imię pracownika:\t\t\t\t\t\t\t");
            if (input == "Y")
            {
                UpdateConsole(sb);
                inputFirstName = GetRestricStringFromConsole(sb);
                if (inputFirstName == null)
                {
                    return;
                }
                ConversionStringFirstCapitalLetterOnly(ref inputFirstName!);
                sb.Append($"{inputFirstName}");
                changeIndex++;
            }
            else
            {
                inputFirstName = employee!.FirstName;
                sb.AppendLine(employee!.FirstName);
                UpdateConsole(sb);
            }

            Console.Write($"\n  Czy chcesz zmienić nazwisko?");
            input = DisplaySelectionUpdateData();
            sb.Append($"\n\tPodaj nazwisko pracownika:\t\t\t\t\t\t");
            if (input == "Y")
            {
                UpdateConsole(sb);
                inputLastName = GetRestricStringFromConsole(sb);
                if (inputLastName == null)
                {
                    return;
                }
                ConversionStringFirstCapitalLetterOnly(ref inputLastName!);
                sb.Append($"{inputLastName}");
                changeIndex++;
            }
            else
            {
                inputLastName = employee!.LastName;
                sb.AppendLine(employee!.LastName);
                UpdateConsole(sb);
            }

            Console.Write($"\n\n  Czy chcesz zmienić stanowisko?");
            input = DisplaySelectionUpdateData();
            sb.Append($"\n\tPodaj stanowisko (1-pracownik, 2-inżynier, 3-kierownik, 4-manager):\t");
            int? jobposition;
            if (input == "Y")
            {
                UpdateConsole(sb);
                jobposition = GetIntWithLimitsFromConsole(sb, (int)JobPosition.Employee, (int)JobPosition.Supervisor);
                if (jobposition == null)
                {
                    return;
                }
                sb.AppendLine($"{jobposition}");
                changeIndex++;
            }
            else
            {
                jobposition = (int)employee.JobPositon;
                sb.AppendLine($"{employee.JobPositon}");
                UpdateConsole(sb);
            }

            Console.Write($"\n\n  Czy chcesz zmienić datę zatrudnienia?");
            input = DisplaySelectionUpdateData();
            sb.Append($"\n\tPodaj datę zatrudnienia (dd.mm.rrrr):\t\t\t\t\t");
            DateOnly employmentDate;
            if (input == "Y")
            {
                UpdateConsole(sb);
                employmentDate = GetDateFromConsole(sb);
                if (employmentDate == DateOnly.MinValue)
                {
                    return;
                }
                sb.Append($"{employmentDate}");
                changeIndex++;
            }
            else
            {
                employmentDate = DateOnly.FromDateTime(employee.EmploymentDate);
                sb.AppendLine($"{employmentDate}");
                UpdateConsole(sb);
            }

            if (changeIndex != 0)
            {
                switch (DisplaySelectionWithData())
                {
                    case "Y":
                    case "y":

                        {
                            employee!.FirstName = inputFirstName;
                            employee!.LastName = inputLastName;
                            employee!.JobPositon = (JobPosition)jobposition;
                            employee!.EmploymentDate = new DateTime(employmentDate.Year, employmentDate.Month, employmentDate.Day, 0, 0, 0);
                            employee!.HourlyRate = SetHourlyRate((JobPosition)jobposition);
                        };
                        _employeesRepository.Update(employee);
                        WaitForKeyPress();
                        return;
                    case "N":
                    case "n":
                        break;
                    default:
                        endMethod = true;
                        break;
                }
            }
            else
            {
                endMethod = true;
            }
        }
    }

}