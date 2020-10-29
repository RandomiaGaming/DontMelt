using System;
public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        MonoGameInterface interfaceGame = new MonoGameInterface(args);
        interfaceGame.Run();
        interfaceGame.Dispose();
    }
}