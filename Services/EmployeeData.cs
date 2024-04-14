using ControlingProjectApp.Data.Entities;
using ControlingProjectApp.Data.Repositories;
using ControlingProjectApp.Services.InquiryData;
using System.Globalization;
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
        }
    }

    private void AddNewEmployee()
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
                var input = Console.ReadLine()!;
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
                            return;
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
                var input = Console.ReadLine()!;
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
                            return;
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
                            return;
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
                            return;
                        default:
                            break;
                    }
                }
            }

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
                    index = 0;
                    break;
                default:
                    endMethod = true;
                    break;
            }
        }
    }

    private static bool IsStringWithPolishLettersOnly(string inputstring)
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

    private static bool IsInputStringValid(string inputstring)
    {
        return IsStringWithPolishLettersOnly(inputstring) && !string.IsNullOrEmpty(inputstring);
    }
}