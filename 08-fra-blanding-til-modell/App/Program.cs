using SpaceAcademy;

Console.Write("Drivstoff (heltall 0–100): ");
int fuel = int.Parse(Console.ReadLine() ?? "0");
Console.Write("Sikkerhet godkjent? Skriv ja: ");
bool approved = Console.ReadLine() == "ja";
// TODO: Bruk LaunchPolicy i stedet for å gjenta regelen her.
bool canLaunch = fuel >= 50 && approved;
if (canLaunch)
{
    Console.WriteLine("Klar for oppskyting.");
}
else
{
    Console.WriteLine("Oppskyting avvist.");
}
