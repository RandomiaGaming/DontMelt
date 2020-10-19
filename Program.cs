using System;
using System.Threading;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        MonoGameInterface interfaceGame = new MonoGameInterface();
        interfaceGame.Run();
        interfaceGame.Dispose();
    }
}