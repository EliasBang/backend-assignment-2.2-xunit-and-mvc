using SpaceAcademy;
using SpaceAcademy.ConsoleApp;

var board = new MissionBoard();
var view = new MissionView();
Console.WriteLine("Demo 1: tom tavle");
view.ShowTasks(board.GetAll());
board.Add("Sjekk antenne");
var rover = board.Add("Lad rover");
board.Complete(rover.Id);
Console.WriteLine("Demo 2: to oppdrag");
view.ShowTasks(board.GetAll());
Console.WriteLine("Demo 3: vis igjen");
view.ShowTasks(board.GetAll());
Console.WriteLine($"Antall fortsatt: {board.Count}; rover fullført: {rover.IsCompleted}");
