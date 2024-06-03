using ControlingProjectApp.Components.Models;
using ControlingProjectApp.Components.CsvReader;

namespace ControlingProjectApp.Services;

public class UserCarsMenu : ItemDataBase, IUserCarsMenu
{
    private readonly string fuelFilePath = "Resources\\Files\\fuel.csv";

    private readonly string manufacturerFilePath = "Resources\\Files\\manufacturers.csv";

    private readonly string xmlFilePath = "Resources\\Files\\fuel.xml";

    private readonly string xmlTaskFilePath = "Resources\\Files\\task.xml";

    private readonly ICsvReader _csvReader;

    private readonly IXmlFilesService _xmlFilesService;

    private readonly List<Car> cars;

    private readonly List<Manufacturer> manufacturers;


    public UserCarsMenu(ICsvReader csvReader, IXmlFilesService xmlFilesService)
    {
        _csvReader = csvReader;
        _xmlFilesService = xmlFilesService;
        this.cars = _csvReader.ProcessCar(fuelFilePath);
        this.manufacturers = _csvReader.ProcessManufacturers(manufacturerFilePath);
    }
    public void UserCarsMenuSelect()
    {
        var endMethod = false;
        while (!endMethod)
        {
            Console.Clear();
            Console.WriteLine();
            Console.Title = "Controling Project";
            Console.WriteLine($"\t     Zapytania o dane do plików");
            Console.WriteLine($"\t=====================================================================\n");
            Console.WriteLine($"\t1   - Zapytanie z wykorzytaniem GroupBy\n");
            Console.WriteLine($"\t2   - Zapytanie z wykorzytaniem Join\n");
            Console.WriteLine($"\t3   - Zapytanie z wykorzytaniem Join i GroupBy\n");
            Console.WriteLine($"\t4   - Tworzenie pliku xml\n");
            Console.WriteLine($"\t5   - Odczytanie pliku xml\n");
            Console.WriteLine($"\t6   - Zadanie z plików xml\n");
            Console.WriteLine($"\tQ/q - Wyjście");
            Console.WriteLine($"\t=====================================================================\n");
            Console.Write($"\tWybierz odpowiednią opcję: ");

            string? menuOption = Console.ReadLine();

            switch (menuOption)
            {
                case "1":
                    InquiryGroupBy();
                    break;
                case "2":
                    InquiryJoin();
                    break;
                case "3":
                    InquiryJoinAndGroupBy();
                    break;
                case "4":
                    CreateXmlFile();
                    break;
                case "5":
                    QueryXmlFile();
                    break;
                case "6":
                    InquiryTaskXmlFile();
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

    private void InquiryGroupBy()
    {
        var groups = cars.GroupBy(c => c.Manufacturer)
             .Select(g => new
             {
                 Produent = g.Key,
                 Max = g.Max(c => c.Combined),
                 Min = g.Min(c => c.Combined),
                 Average = Math.Round(g.Average(c => c.Combined), 2),
             }).OrderByDescending(x => x.Max);

        Paging($"Wynik zapytania GroupBy wg maksymalnej liczby wyprodukowanych aut:\n", 50, groups);
    }

    private void InquiryJoin()
    {
        var carsInCountry = cars.Join(
            manufacturers,
            x => x.Manufacturer,
            x => x.Name,
            (car, manufacturer) => new
            {
                manufacturer.Country,
                car.Name,
                car.Combined,
            }
        ).OrderByDescending(x => x.Combined)
        .ThenBy(x => x.Name);

        Paging($"Wynik zapytania Join wg ilości wyprodukowanych aut:\n", 50, carsInCountry);
    }

    private void InquiryJoinAndGroupBy()
    {
        var groupsJoin = manufacturers.GroupJoin(
            cars,
            manufacturer => manufacturer.Name,
            car => car.Manufacturer,
            (m, g) =>
            new
            {
                Manufacturer = m,
                Cars = g,
            }
            )
            .OrderBy(x => x.Manufacturer.Name);

        Console.Clear();
        foreach (var item in groupsJoin)
        {
            Console.WriteLine($"Manufacturer: {item.Manufacturer.Name}");
            Console.WriteLine($"\t Cars: {item.Cars.Count()}");
            Console.WriteLine($"\t Max: {item.Cars.Max(x => x.Combined)}");
            Console.WriteLine($"\t Min: {item.Cars.Min(x => x.Combined)}");
            Console.WriteLine($"\t Avg: {Math.Round(item.Cars.Average(x => x.Combined), 2)}");
            Console.WriteLine();
        }
        WaitForKeyPress();
    }

    private void CreateXmlFile()
    {
        Console.Clear();
        Console.WriteLine($"Tworzenie pliku fuel.xml");
        _xmlFilesService.CreateXml(fuelFilePath, xmlFilePath);
        WaitForKeyPress() ;
    }
    private void QueryXmlFile()
    {
        Console.Clear();   
        Paging($"Odczytanie pliku fuel.xml typów producenta BMW", 25, _xmlFilesService.QueryXml(xmlFilePath));
    }

    private void InquiryTaskXmlFile()
    {
        Console.Clear();
        Console.WriteLine($"Tworzenie pliku task.xml");
        _xmlFilesService.InquiryXmlTask(fuelFilePath, manufacturerFilePath, xmlTaskFilePath);
        WaitForKeyPress();
    }


}




