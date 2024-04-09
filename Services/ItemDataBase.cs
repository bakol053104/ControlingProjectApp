using ControlingProjectApp.Data.Repositories;
using ControlingProjectApp.Entities;

namespace ControlingProjectApp.Services;

public abstract class ItemDataBase : UserSubMenuBase
{

    protected static void WriteAllToConsole<T>(IRepository<T> repository) where T : class, IEntity
    {
        var numberposition = 5;
        Type entityType = typeof(T);
        string titlestring = "";

        if (entityType.Name == "Employee")
        {
            titlestring = "Lista pracowników:";
        }
        else if (entityType.Name == "Project")
        {
            titlestring = "Lista projektów:";
        }
        Paging(titlestring, numberposition, repository.GetAll());
    }

    protected static T? FindItemById<T>(IRepository<T> repository) where T : class, IEntity
    {
        var endMethod = false;
        Type entityType = typeof(T);

        while (!endMethod)
        {
            Console.Clear();
            if (entityType.Name == "Employee")
            {
                Console.WriteLine($"\t\tWyszukiwanie Pracownika");
                DisplayDescriptionSeparator();
            }
            else if (entityType.Name == "Project")
            {
                Console.WriteLine($"\t\tWyszukiwanie Projektu");
                DisplayDescriptionSeparator();
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
                    Console.WriteLine($"\n\tBrak podanego Id w bazie");
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

    protected static void RemoveItem<T>(IRepository<T> repository) where T : class, IEntity
    {
        var endMethod = false;
        Type entityType = typeof(T);

        while (!endMethod)

        {
            Console.Clear();
            if (entityType.Name == "Employee")
            {
                Console.WriteLine($"\t\tUsuwanie Pracownika");
                DisplayDescriptionSeparator();
            }
            else if (entityType.Name == "Project")
            {
                Console.WriteLine($"\t\tUsuwanie Projektu");
                DisplayDescriptionSeparator();
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
                    WaitForKeyPress();
                    return;
                }
                else
                {
                    Console.WriteLine($"\n\tBrak podanego Id w bazie");
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

    protected static void Paging<T>(string titlestring, int numberposition, IEnumerable<T> list) where T : class, IEntity
    {
        var quantity = list.Count();
        var index = 0;
        var position = 1;
        List<T> items;
        do
        {
            Console.Clear();
            Console.WriteLine($"\t{titlestring}");
            items = list.Skip(index).Take(numberposition).ToList();
            foreach (var item in items)
            {
                Console.WriteLine($"{position}.{item}");
                position++;
            }
            WaitForKeyPress();
            index += numberposition;
        } while (index < quantity);
    }
}



