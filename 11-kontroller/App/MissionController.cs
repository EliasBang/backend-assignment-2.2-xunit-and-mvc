using SpaceAcademy;

namespace SpaceAcademy.ConsoleApp;

public class MissionController
{
    private MissionBoard _board;
    private MissionView _view;

    public MissionController(MissionBoard board, MissionView view)
    {
        _board = board;
        _view = view;
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            _view.ShowMenu();
            string choice = _view.ReadInput("Valg: ");
            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    _view.ShowTasks(_board.GetAll());
                    break;
                case "3":
                    CompleteTask();
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    _view.ShowMessage("Ukjent menyvalg.");
                    break;
            }
        }
    }

    private void AddTask()
    {
        string title = _view.ReadInput("Tittel: ");
        if (string.IsNullOrWhiteSpace(title))
        {
            _view.ShowMessage("Tittel kan ikke være tom.");
            return;
        }
        var task = _board.Add(title);
        _view.ShowMessage($"Opprettet oppdrag {task.Id}.");
    }

    private void CompleteTask()
    {
        // TODO: Les ID, tolk tekst, kall modellen, vis resultat.
        _view.ShowMessage("Fullføring er ikke koblet til ennå.");
    }
}
