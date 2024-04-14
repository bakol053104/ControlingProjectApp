using ControlingProjectApp.Data.Entities;
using ControlingProjectApp.Data.Repositories;
using System.Globalization;

namespace ControlingProjectApp.Services.InquiryData;

public class InquiryProviderForEmployees : ItemDataBase, IInquiryProviderForEmployees
{
    private readonly IInquiryDataProviderForEmployees _inquiryDataProviderForEmployees;

    public InquiryProviderForEmployees(IInquiryDataProviderForEmployees inquiryDataProviderForEmployees)
    {
        _inquiryDataProviderForEmployees = inquiryDataProviderForEmployees;
    }

    public void GetInquiryForEmployess()
    {
        var endMethod = false;
        while (!endMethod)
        {
            Console.Clear();
            Console.WriteLine();
            Console.Title = "Controling Project";
            Console.WriteLine($"\t     Zapytania o dane pracowników");
            Console.WriteLine($"\t=====================================================================\n");
            Console.WriteLine($"\t1   - Wyszukaj pracowników wg stanowiska\n");
            Console.WriteLine($"\t2   - Wyszukaj pracowników od podanej daty zatrudnienia\n");
            Console.WriteLine($"\t3   - Wyszukaj pracowników do podanej daty zatrudnienia\n");
            Console.WriteLine($"\t4   - Wyszukaj pracowników wg porządku alfabetycznego\n");
            Console.WriteLine($"\t5   - Wyszukaj pracowników wg odwrotnego porządku alfabetycznego\n");
            Console.WriteLine($"\tQ/q - Wyjście");
            Console.WriteLine($"\t=====================================================================\n");
            Console.Write($"\tWybierz odpowiednią opcję: ");

            string? menuOption = Console.ReadLine();

            switch (menuOption)
            {
                case "1":
                    GetEmployeesWithJobPosition();
                    break;
                case "2":
                    GetEmployeesWithDateEmploymentGreaterThan();
                    break;
                case "3":
                    GetEmployeesWithDateEmploymentLessThan();
                    break;
                case "4":
                    GetOrderByLastNameAndFirstName();
                    break;
                case "5":
                    GetOrderByLastNameAndFirstNameDescending();
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

    private void GetEmployeesWithJobPosition()
    {
        var endMethod = false;
        while (!endMethod)
        {
            Console.Clear();
            Console.Write($"\tPodaj stanowisko do wyszukania (1-pracownik, 2-inżynier, 3-kierownik, 4-manager):\t");
            var input = Console.ReadLine();
            var isNumber = int.TryParse(input, out int jobPosition);
            if (isNumber && jobPosition >= 1 && jobPosition <= 4)
            {
                var items = _inquiryDataProviderForEmployees.EmployeesWithJobPosition(jobPosition);
                var stringjobposition= Enum.GetName((JobPosition) jobPosition);
                Paging($"Pracownicy o stanowisku {stringjobposition}:", 5, items!);
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

    private void GetEmployeesWithDateEmploymentGreaterThan()
    {
        var endMethod = false;
        while (!endMethod)
        {
            Console.Clear();
            Console.Write($"\tPodaj datę zatrudnienia od której zostaną wyszukani pracownicy:\t");
            var inputDate = Console.ReadLine();
            if (DateOnly.TryParseExact(inputDate, "d", CultureInfo.CurrentCulture, 0, out DateOnly employmentDate))
            {
                var employmentDateTime = new DateTime(employmentDate.Year, employmentDate.Month, employmentDate.Day, 0, 0, 0);
                var items = _inquiryDataProviderForEmployees.EmployeesWithDateEmploymentGreaterThan(employmentDateTime);
                Paging($"Pracownicy zatrudnieni od {employmentDateTime:d}:", 5, items);
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

    private void GetEmployeesWithDateEmploymentLessThan()
    {

        var endMethod = false;
        while (!endMethod)
        {
            Console.Clear();
            Console.Write($"\tPodaj datę zatrudnienia do której zostaną wyszukani pracownicy:\t");
            var inputDate = Console.ReadLine();
            if (DateOnly.TryParseExact(inputDate, "d", CultureInfo.CurrentCulture, 0, out DateOnly employmentDate))
            {
                var employmentDateTime = new DateTime(employmentDate.Year, employmentDate.Month, employmentDate.Day, 0, 0, 0);
                var items = _inquiryDataProviderForEmployees.EmployeesWithDateEmploymentLessThan(employmentDateTime);
                Paging($"Pracownicy zatrudnieni do {employmentDateTime:d}:", 5, items);
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

    private void GetOrderByLastNameAndFirstName()
    {
        var items = _inquiryDataProviderForEmployees.OrderByLastNameAndFirstName();
        Paging($"Pracownicy w porządku alfabetycznym:", 5, items);
    }

    private void GetOrderByLastNameAndFirstNameDescending()
    {
        var items = _inquiryDataProviderForEmployees.OrderByLastNameAndFirstNameDescending();
        Paging($"Pracownicy w odwrotnym porządku alfabetycznym:", 5, items);
    }
}





