using System;
public static class Program
{
    [STAThread]
    static void Main()
    {
        var game = new MonoGameInterface();
        game.Run();
        game.Dispose();
    }
}