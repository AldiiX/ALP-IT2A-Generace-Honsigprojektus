using Spectre.Console;

namespace ConsoleApp3;



public static class Program {

    /**
     * Získá generaci podle věku
     * <returns>Název generace</returns>
     * <param name="age">Věk</param>
     * <param name="birthYear">Rok narození, který metoda vrátí</param>
     */
    private static string? GetGeneration(in byte age, out ushort birthYear) {
        ushort year = (ushort)(DateTime.Now.Year - age);

        birthYear = year;
        return year switch {
            >= 1928 and <= 1945 => "Silent Generation",
            >= 1946 and <= 1964 => "Baby Boomer",
            >= 1965 and <= 1980 => "Generation X",
            >= 1981 and <= 1996 => "Millennials (Gen Y)",
            >= 1997 and <= 2012 => "Generation Z",
            >= 2013 => "Generation Alpha",
            _ => null
        };
    }



    /**
     * Main metoda
     */
    public static void Main() {
        bool running = true;

        while (running) {
            Jáchym.Klír();

            // zepta se na user input
            byte age = AnsiConsole.Prompt(new TextPrompt<byte>("Zadej věk: ")
                .Validate(input => {
                    if(input > 120) return ValidationResult.Error("[red]Neplatný věk! Zkus to znovu.[/]\n");

                    return ValidationResult.Success();
                })
                .ValidationErrorMessage("[red]Zadej tvůj věk jako kladné číslo.\n[/]")
            );


            // získá generaci a rok narození
            string generation = GetGeneration(age, out var birthYear) ?? "Neznámá generace";


            // vypise vysledek
            Jáchym.Klír();

            var panel = new Panel($"" +
                  $"[bold]Tvůj zadaný věk: [yellow1]{age}[/][/]\n" +
                  $"[bold]Tvůj rok narození: [yellow1]{birthYear}[/][/]\n" +
                  $"[bold]Tvoje generace je: [aqua]{generation}[/][/]"
            ) {
                Padding = new Padding(1,1,1,1),
                Header = new PanelHeader("[green]  Výsledek  [/]") { Justification = Justify.Center },
                BorderStyle = new Style(Color.Grey15)
            };

            AnsiConsole.Write(panel);
            AnsiConsole.Write(new FigletText("************"){Color = Color.Grey35});
            AnsiConsole.WriteLine();

            // zepta se na ukonecni nebo ne
            bool cont = AnsiConsole.Confirm("Chceš zadat další věk?", false);
            if(cont == false) running = false;
        }
    }
}

file static class Jáchym { public static void Klír() => Console.Clear(); }