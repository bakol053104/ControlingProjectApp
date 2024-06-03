using System.Globalization;
using System.Text;

namespace ControlingProjectApp.Services;

public abstract class UserSubMenuBase : CalculationDataBase
{

    protected static void ConversionStringFirstCapitalLetterOnly(ref string inputstring)
    {
        inputstring = char.ToUpper(inputstring[0]) + inputstring[1..].ToLower();
    }

    protected static string? DisplaySelectionWithData()
    {
        Console.Write($"\n\n\t(Y/y - potwierdź, N/n- wprowadzanie nowego, Wyjście- dowolny klawisz): ");
        return Console.ReadLine()?.ToUpper(); 
    }

    protected static string? DisplaySelectionWithInvalidData()
    {
        Console.Write($"\n\tNiewłaściwe dane (Ponowne wprowadzenie- dowolny klawisz, Q/q- rezygnacja): ");
        return Console.ReadLine()?.ToUpper(); 
    }

    protected static string? DisplaySelectionUpdateData()
    {
        Console.Write($"\n\t(Y/y- zmiana danych, Brak zmian- dowolny klawisz): ");
        return Console.ReadLine()?.ToUpper();
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

    protected static void UpdateConsole(StringBuilder sb)
    {
        Console.Clear();
        Console.Write(sb);
    }

    protected static string? GetStringFromConsole(StringBuilder sb)
    {
        var endMethod = false;
        string? input = null; ;
        while (!endMethod)
        {
            UpdateConsole(sb);
            input = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(input))
            {
                endMethod = true;
            }
            else
            {
                if (DisplaySelectionWithInvalidData() == "Q")
                {
                    return null;
                }
            }
        }
        return input;
    }

    protected static string? GetRestricStringFromConsole(StringBuilder sb)
    {
        var endMethod = false;
        string? input = null; ;
        while (!endMethod)
        {
            UpdateConsole(sb);
            input = Console.ReadLine()!;
            if (IsInputStringValid(input))
            {
                endMethod = true;
            }
            else
            {
                if (DisplaySelectionWithInvalidData() == "Q")
                {
                    return null;
                }
            }
        }
        return input;
    }

    protected static  int? GetIntWithLimitsFromConsole(StringBuilder sb, int begin, int end)
    {
        var endMethod = false;
        int number = 0;
        while (!endMethod)
        {
            UpdateConsole(sb);
            string? input = Console.ReadLine()!;
            var isNumber = int.TryParse(input, out number);
            if (isNumber && number >= begin && number <= end)
            {
                endMethod = true;
            }
            else
            {
                if (DisplaySelectionWithInvalidData() == "Q")
                {
                    return null;
                }
            }
        }
        return number;
    }

    protected static decimal? GetDecimalFromConsole(StringBuilder sb)
    {
        var endMethod = false;
        decimal number = 0;
        while (!endMethod)
        {
            UpdateConsole(sb);
            string? input = Console.ReadLine()!;
            var isNumber = decimal.TryParse(input, out number);
            if (isNumber)
            {
                endMethod = true;
            }
            else
            {
                if (DisplaySelectionWithInvalidData() == "Q")
                {
                    return null;
                }
            }
        }
        return number;
    }

    protected static DateOnly GetDateFromConsole(StringBuilder sb)
    {
        var endMethod = false;
        DateOnly date = new();
        while (!endMethod)
        {
            UpdateConsole(sb);
            string? input = Console.ReadLine()!;
            if (DateOnly.TryParseExact(input, "d", CultureInfo.CurrentCulture, 0, out date) && date != DateOnly.MinValue)
            {
                endMethod = true;
            }
            else
            {
                if (DisplaySelectionWithInvalidData() == "Q")
                {
                    return DateOnly.MinValue;
                }
            }
        }
        return date;
    }

    private static bool IsInputStringValid(string inputstring)
    {
        return IsStringWithPolishLettersOnly(inputstring) && !string.IsNullOrEmpty(inputstring);
    }

    private static bool IsStringWithPolishLettersOnly(string inputstring)
    {
        foreach (char c in inputstring)
        {
            if (!((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c >= 260 && c <= 263) || (c >= 321 && c <= 324) ||

                (c >= 377 && c <= 380) || (c == 211) || (c >= 280 && c <= 281) || (c >= 346 && c <= 347) || (c == 243)))
            {
                return false;
            }
        }
        return true;
    }
}

