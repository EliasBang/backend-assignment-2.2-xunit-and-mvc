namespace SpaceAcademy;

public static class SeatPolicy
{
    public static bool CanReserve(int requested, int available)
    {
        // TODO: Finn grensefeilen ved hjelp av testene.
        return requested > 0 && requested < available;
    }
}
