using System;
public static class Program
{
    [STAThread]
    static void Main()
    {
        MonoGameInterface interfaceGame = new MonoGameInterface();
        interfaceGame.Run();
        interfaceGame.Dispose();
    }
}