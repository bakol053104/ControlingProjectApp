namespace ControlingProjectApp.Services;

public class UserMenu : IUserMenu
{
    private readonly IUserSubMenu _userSubMenu;
    private readonly IUserCarsMenu _userCarsMenu;
    private readonly IEventSet _eventSet;

    public UserMenu(IUserSubMenu userSubMenu, IUserCarsMenu userCarsMenu, IEventSet eventSet)
    {
        _userSubMenu = userSubMenu;
        _userCarsMenu = userCarsMenu;
        _eventSet = eventSet;
    }

    public void MainUserMenuSelect()
    {
        _eventSet.SavetoEventSet();

        var endProgramm = false;
        while (!endProgramm)
        {
            Console.Clear();
            Console.WriteLine();
            Console.Title = "Controling Project";
            Console.WriteLine($"\t      Witamy w programie do kontroli prowadzonych projektów");
            Console.WriteLine($"\t=====================================================================\n");
            Console.WriteLine($"\t1   - Wyświetl zawartość danych\n");
            Console.WriteLine($"\t2   - Dodaj dane\n");
            Console.WriteLine($"\t3   - Szukaj dane \n");
            Console.WriteLine($"\t4   - Usuń dane \n");
            Console.WriteLine($"\t5   - Zapytania o dane \n");
            Console.WriteLine($"\t6   - Zadania moduł 4 (samochody) \n");
            Console.WriteLine($"\tQ/q - Wyjdż z programu\n");
            Console.WriteLine($"\t=====================================================================\n");
            Console.Write($"\tWybierz odpowiednią opcję: ");

            string? menuOption = Console.ReadLine();

            switch (menuOption)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                    _userSubMenu.UserSubMenuSelect(menuOption);
                    break;
                case "6":
                    _userCarsMenu.UserCarsMenuSelect();
                    break;
                case "Q":
                case "q":
                    endProgramm = true;
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }
}

