using System;
public static class Program
{
    [STAThread]
    static void Main()
    {
        var game = new InterfaceGame();
        game.Run();
        game.Dispose();
    }
}