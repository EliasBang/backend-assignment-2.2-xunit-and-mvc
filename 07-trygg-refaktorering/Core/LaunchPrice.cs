namespace SpaceAcademy;

public static class LaunchPrice
{
    public static int Calculate(int n, bool p)
    {
        int x = 0;
        if (p)
        {
            x = n * 100;
            x = x + n * 20;
        }
        else
        {
            x = n * 100;
        }
        return x;
    }
}
