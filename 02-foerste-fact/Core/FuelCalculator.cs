namespace SpaceAcademy;

public static class FuelCalculator
{
    public static int Calculate(int crewCount, int days)
    {
        return crewCount * days * 2;
    }
}
