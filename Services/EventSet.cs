using ControlingProjectApp.Data.Entities;
using ControlingProjectApp.Data.Repositories;

namespace ControlingProjectApp.Services;

public class EventSet : IEventSet
{
    private readonly IRepository<Employee> _employeesRepository;
    private readonly IRepository<Project> _projectsRepository;

    const string logFileName = "log.txt";

    public EventSet(IRepository<Employee> employessRepository, IRepository<Project> projectsRepository)
    {
        _employeesRepository = employessRepository;
        _projectsRepository = projectsRepository;
    }

    public void SavetoEventSet()
    {
        _employeesRepository.ItemAdded += RepositoryOnItemAdded;
        _projectsRepository.ItemAdded += RepositoryOnItemAdded;
        _employeesRepository.ItemRemoved += RepositoryOnItemRemoved;
        _projectsRepository.ItemRemoved += RepositoryOnItemRemoved;
        _employeesRepository.ItemUpdated += RepositoryOnItemUpdated;
        _projectsRepository.ItemUpdated += RepositoryOnItemUpdated;
    }

    private static void RepositoryOnItemAdded<T>(object? sender, T? e)
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
    }
    private static void RepositoryOnItemRemoved<T>(object? sender, T? e)
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
    }

    private static void RepositoryOnItemUpdated<T>(object? sender, T? e)
    {
        Type entityType = typeof(T);
        string infoMessage = "";
        Console.Clear();
        if (entityType.Name == "Employee")
        {
            infoMessage = ($"\nPracownik:\n {e} \nzostał zmodyfikowany");
        }
        else if (entityType.Name == "Project")
        {
            infoMessage = ($"\nProjekt:\n {e} \nzostał zmodyfikowany");
        }
        Console.WriteLine(infoMessage);
        AddToLogFile(infoMessage);
    }

    private static void AddToLogFile(string infoMessage)
    {
        using var writer = File.AppendText(logFileName);
        writer.WriteLine($"{infoMessage} {DateTime.Now}");
    }
}

