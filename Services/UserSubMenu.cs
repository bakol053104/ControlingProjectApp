namespace ControlingProjectApp.Services;

public class UserSubMenu : UserSubMenuBase, IUserSubMenu
{
    private readonly IEmployeeData _employeeData;
    private readonly IProjectData _projectData;

    public UserSubMenu(IEmployeeData employeeData, IProjectData projectData)
    {
        _employeeData = employeeData;
        _projectData = projectData;
    }

    public void UserSubMenuSelect(string menuOption)
    {
        var endMethod = false;
        while (!endMethod)
        {
            Console.Clear();

            switch (menuOption)
            {
                case "1":
                    Console.WriteLine($"\t\t\t Wyświetl dane");
                    DisplayDescriptionSeparator();
                    break;
                case "2":
                    Console.WriteLine($"\t\t\t Dodaj do danych");
                    DisplayDescriptionSeparator();
                    break;
                case "3":
                    Console.WriteLine($"\t\t\t Wyszukaj dane");
                    DisplayDescriptionSeparator();
                    break;
                case "4":
                    Console.WriteLine($"\t\t\t Usuń dane");
                    DisplayDescriptionSeparator();
                    break;
                case "5":
                    Console.WriteLine($"\t\t\t Zapytania o dane");
                    DisplayDescriptionSeparator();
                    break;
                default:
                    break;
            }
            Console.WriteLine($"\t1   - Pracowników\n");
            Console.WriteLine($"\t2   - Projektów\n");
            Console.WriteLine($"\tQ/q - Wyjście\n");
            DisplayDescriptionSeparator();
            Console.Write($"\tWybierz odpowiednią opcję: ");

            string? subMenuOption = Console.ReadLine();

            switch (subMenuOption)
            {
                case "1":
                    switch (menuOption)
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                            _employeeData.EmployeeDataHandling(menuOption);
                            break;
                    }
                    break;
                case "2":
                    switch (menuOption)
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                            _projectData.ProjectDataHandling(menuOption);
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
}



