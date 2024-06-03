using ControlingProjectApp.Data.Entities;
using ControlingProjectApp.Data.Repositories;
using ControlingProjectApp.Services.InquiryData;
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
            case "6":
                UpdateProject();
                break;
        }
    }

    private void AddNewProject()
    {
        var sb = new StringBuilder();
        var endMethod = false;

        while (!endMethod)
        {
            sb = sb.Clear();
            sb.AppendLine($"\t\tWprowadzanie danych projektu");
            sb.AppendLine($"\t=======================================================================================\n");
            sb.Append($"\n\tPodaj nazwę projektu:\t\t\t\t");
            UpdateConsole(sb);
            string? inputName = GetStringFromConsole(sb);
            if (inputName == null)
            {
                return;
            }
            ConversionStringFirstCapitalLetterOnly(ref inputName!);
            sb.Append($"{inputName}");

            sb.Append($"\n\tPodaj opis projeku:\t\t\t\t");
            UpdateConsole(sb);
            string? inputDescription = GetStringFromConsole(sb);
            if (inputDescription == null)
            {
                return;
            }
            ConversionStringFirstCapitalLetterOnly(ref inputDescription!);
            sb.Append($"{inputDescription}");

            sb.Append($"\n\tPodaj budżet projektu:\t\t\t\t");
            UpdateConsole(sb);
            decimal? inputBudget = GetDecimalFromConsole(sb);
            if (inputBudget == null)
            {
                return;
            }
            sb.Append($"{inputBudget}");

            sb.Append($"\n\tPodaj datę początkową projektu (dd.mm.rrrr):\t");
            UpdateConsole(sb);
            DateOnly beginDate = GetDateFromConsole(sb);
            if (beginDate == DateOnly.MinValue)
            {
                return;
            }
            sb.Append($"{beginDate}");

            sb.Append($"\n\tPodaj datę końcową projektu (dd.mm.rrrr):\t");
            UpdateConsole(sb);
            DateOnly endDate = GetDateFromConsole(sb);
            if (endDate == DateOnly.MinValue)
            {
                return;
            }
            sb.Append($"{endDate}");

            switch (DisplaySelectionWithData())
            {
                case "Y":
                case "y":
                    var project = new Project
                    {
                        Name = inputName,
                        Description = inputDescription,
                        Budget = (decimal)inputBudget,
                        BeginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0),
                        EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0),
                    };
                    _projectsRepository.Add(project);
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

    private void UpdateProject()
    {
        var sb = new StringBuilder();
        var project = FindItemById(_projectsRepository);
        string? inputName = null;
        string? inputDescription = null;
        decimal? inputBudget;
        int changeIndex;
        var endMethod = false;

        while (!endMethod)
        {
            changeIndex = 0;
            Console.Clear();
            sb = sb.Clear();
            sb.AppendLine($"\t\tModyfikowanie danych projektu");
            sb.AppendLine($"\t=======================================================================================\n");
            sb = sb.AppendLine(project!.ToString());
            UpdateConsole(sb);

            Console.Write($" \n\n Czy chcesz zmienić nazwę projektu?");
            var input = DisplaySelectionUpdateData();
            sb.Append($"\n\tPodaj nazwę projeku:\t\t\t\t");
            if (input == "Y")
            {
                UpdateConsole(sb);
                inputName = GetStringFromConsole(sb);
                if (inputName == null)
                {
                    return;
                }
                ConversionStringFirstCapitalLetterOnly(ref inputName!);
                sb.Append($"{inputName}");
                changeIndex++;
            }
            else
            {
                inputName = project!.Name;
                sb.AppendLine(project!.Name);
                UpdateConsole(sb);
                changeIndex = 0;
            }

            Console.Write($" \n\n Czy chcesz zmienić opis projektu?");
            input = DisplaySelectionUpdateData();
            sb.Append($"\n\tPodaj opis projeku:\t\t\t\t");
            if (input == "Y")
            {
                UpdateConsole(sb);
                inputDescription = GetStringFromConsole(sb);
                if (inputDescription == null)
                {
                    return;
                }
                ConversionStringFirstCapitalLetterOnly(ref inputDescription!);
                sb.Append($"{inputDescription}");
                changeIndex++;
            }
            else
            {
                inputDescription = project!.Description;
                sb.AppendLine(project!.Description);
                UpdateConsole(sb);
            }

            Console.Write($" \n\n Czy chcesz zmienić budżet projektu?");
            input = DisplaySelectionUpdateData();
            sb.Append($"\n\tPodaj budżet projektu:\t\t\t\t");
            if (input == "Y")
            {
                UpdateConsole(sb);
                inputBudget = GetDecimalFromConsole(sb);
                if (inputBudget == null)
                {
                    return;
                }
                sb.Append($"{inputBudget}");
                changeIndex++;
            }
            else
            {
                inputBudget = project!.Budget;
                sb.AppendLine($"{project!.Budget:N2}");
                UpdateConsole(sb);
            }

            Console.Write($" \n\n Czy chcesz zmienić datę początkową projektu?");
            input = DisplaySelectionUpdateData();
            sb.Append($"\n\tPodaj datę początkową projektu (dd.mm.rrrr):\t");
            DateOnly beginDate;
            if (input == "Y")
            {

                UpdateConsole(sb);
                beginDate = GetDateFromConsole(sb);
                if (beginDate == DateOnly.MinValue)
                {
                    return;
                }
                sb.Append($"{beginDate}");
                changeIndex++;
            }
            else
            {
                beginDate = DateOnly.FromDateTime(project.BeginDate);
                sb.AppendLine($"{beginDate}");
                UpdateConsole(sb);
            }

            Console.Write($" \n\n Czy chcesz zmienić datę końcową projektu?");
            input = DisplaySelectionUpdateData();
            sb.Append($"\n\tPodaj datę końcową projektu (dd.mm.rrrr):\t");
            DateOnly endDate;
            if (input == "Y")
            {

                UpdateConsole(sb);
                endDate = GetDateFromConsole(sb);
                if (endDate == DateOnly.MinValue)
                {
                    return;
                }
                sb.Append($"{endDate}");
                changeIndex++;
            }
            else
            {
                endDate = DateOnly.FromDateTime(project.EndDate);
                sb.AppendLine($"{endDate}");
                UpdateConsole(sb);
            }

            if (changeIndex != 0)
            {
                switch (DisplaySelectionWithData())
                {
                    case "Y":
                    case "y":

                        {
                            project!.Name = inputName;
                            project!.Description = inputDescription;
                            project!.Budget = (decimal)inputBudget;
                            project!.BeginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0);
                            project!.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0);
                        };
                        _projectsRepository.Update(project);
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




