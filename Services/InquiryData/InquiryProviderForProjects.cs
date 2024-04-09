namespace ControlingProjectApp.Services.InquiryData;

public class InquiryProviderForProjects : ItemDataBase, IInquiryProviderForProjects
{
    private readonly IInquiryDataProviderForProjects _inquiryDataProviderFoProjects;

    public InquiryProviderForProjects(IInquiryDataProviderForProjects inquiryDataProviderFoProjects)
    {
        _inquiryDataProviderFoProjects = inquiryDataProviderFoProjects;
    }

    public void GetInquiryForProjects()
    {
        var endMethod = false;
        while (!endMethod)
        {
            Console.Clear();
            Console.WriteLine();
            Console.Title = "Controling Project";
            Console.WriteLine($"\t     Zapytania o dane projektów");
            Console.WriteLine($"\t=====================================================================\n");
            Console.WriteLine($"\t1   - Wyszukaj projekty wg porządku alfabetycznego\n");
            Console.WriteLine($"\t2   - Wyszukaj projekty wg odwrotnego porządku alfabetycznego\n");
            Console.WriteLine($"\t3   - Projeky wg najwyższej wartości budżetu\n");
            Console.WriteLine($"\t4   - Projeky wg najniższej wartości budżetu\n");
            Console.WriteLine($"\t5   - Suma wartości wszystkich projektów\n");
            Console.WriteLine($"\tQ/q - Wyjście");
            Console.WriteLine($"\t=====================================================================\n");
            Console.Write($"\tWybierz odpowiednią opcję: ");

            string? menuOption = Console.ReadLine();

            switch (menuOption)
            {
                case "1":
                    GetOrderByName();
                    break;
                case "2":
                    GetOrderByNameDescending();
                    break;
                case "3":
                    GetProjectBudgetMaxValue();
                    break;
                case "4":
                    GetProjectBudgetMinValue();
                    break;
                case "5":
                    GetSumBudgetValue();
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

    private void GetOrderByName()
    {
        var items = _inquiryDataProviderFoProjects.OrderByName();
        Paging($"Projekty w porządku alfabetycznym:", 5, items);
    }

    private void GetOrderByNameDescending()
    {
        var items = _inquiryDataProviderFoProjects.OrderByNameDescending();
        Paging($"Projekty w odwrotnym porządku alfabetycznym:", 5, items);
    }

    private void GetProjectBudgetMaxValue()
    {
        var items = _inquiryDataProviderFoProjects.OrderByMaxValue();
        Paging($"Projekty wg najwyższej wartości:", 5, items);
    }

    private void GetProjectBudgetMinValue()
    {
        var items = _inquiryDataProviderFoProjects.OrderByMinValue();
        Paging($"Projekt wg najniższej wartości:", 5, items);
    }
    private void GetSumBudgetValue()
    {
        var budgetValue = _inquiryDataProviderFoProjects.SumValue();
        Console.Clear();
        Console.WriteLine($"Suma wartości projektów wynosi: {budgetValue:N2}");
        WaitForKeyPress();

    }
}
