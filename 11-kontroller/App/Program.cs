using SpaceAcademy;
using SpaceAcademy.ConsoleApp;

var board = new MissionBoard();
var view = new MissionView();
var controller = new MissionController(board, view);
controller.Run();
