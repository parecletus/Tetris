namespace Helpers;


public class Helpers
{
    public static bool GameIsOn = false;

    public static void Gameover()
    {
        GameIsOn = !GameIsOn;
    }
}