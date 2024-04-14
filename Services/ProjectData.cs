using ControlingProjectApp.Data.Entities;
using ControlingProjectApp.Data.Repositories;
using ControlingProjectApp.Services.InquiryData;
using System.Globalization;
using System.Text;


namespace ControlingProjectApp.Services;

public class ProjectData : ItemDataBase, IProjectData
{

    private readonly IRepository<Project> _projectsRepository;
    private readonly IInquiryProviderForProjects _inquiryProviderForProjects;

    public ProjectData(IRepository<Project> projectsRepository, IInquiryProviderForProjects inquiryProviderForProjects)
    {
        _projectsRepository = projectsRepository;
        _inquiryProviderForProjects = inquiryProviderForProjects;
    }

    public void ProjectDataHandling(string menuOption)
    {

        switch (menuOption)
        {
            case "1":
                WriteAllToConsole(_projectsRepository);
                break;
            case "2":
                AddNewProject();
                break;
            case "3":
                FindItemById(_projectsRepository);
                break;
            case "4":
                RemoveItem(_projectsRepository);
                break;
            case "5":
                _inquiryProviderForProjects.GetInquiryForProjects();
                break;
        }
    }

    private void AddNewProject()
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
                            return;
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
                            return;
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
                            return;
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
                            return;
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
                    var project = new Project
                    {
                        Name = inputName,
                        Description = inputDescription,
                        Budget = inputBudget,
                        BeginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0),
                        EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0),
                    };
                    _projectsRepository.Add(project);
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
}


