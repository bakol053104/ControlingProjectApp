namespace ControlingProjectApp.Services;

public abstract class UserSubMenuBase: CalculationDataBase
{

    protected static void ConversionStringFirstCapitalLetterOnly(ref string inputstring)
    {
        inputstring = char.ToUpper(inputstring[0]) + inputstring[1..].ToLower();
    }

    protected static string? DisplaySelectionWithData()
    {
        Console.Write($"\n\n\t(Y/y - potwierdź, N/n- wprowadzanie nowego, Wyjście- dowolny klawisz): ");
        return Console.ReadLine();
    }

    protected static string? DisplaySelectionWithInvalidData()
    {
        Console.Write($"\n\tNiewłaściwe dane (Ponowne wprowadzenie- dowolny klawisz, Q/q- rezygnacja): ");
        return Console.ReadLine();
    }

    protected static void DisplayDescriptionSeparator()
    {
        Console.WriteLine($"\t=====================================================================\n");
    }

    protected static void WaitForKeyPress()
    {
        Console.Write($"\n\tNaciśnij dowolny klawisz ");
        Console.ReadKey();
    }
}

