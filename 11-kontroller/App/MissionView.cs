using SpaceAcademy;

namespace SpaceAcademy.ConsoleApp;

public class MissionView
{
    public void ShowMenu()
    {
        Console.WriteLine("1. Legg til | 2. Vis alle | 3. Fullfør | 0. Avslutt");
    }

    public string ReadInput(string prompt)
    {
        Console.Write(prompt);
        // EOF betyr avslutt. ?? velger høyresiden hvis ReadLine returnerer null.
        return Console.ReadLine() ?? "0";
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowTasks(List<MissionTask> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("Ingen oppdrag.");
            return;
        }
        foreach (var task in tasks)
        {
            string status = "Venter";
            if (task.IsCompleted)
            {
                status = "Fullført";
            }
            Console.WriteLine($"{task.Id}: {task.Title} [{status}]");
        }
    }
}
