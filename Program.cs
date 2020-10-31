using System;
public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        bool debugMode = false;
        if (args != null && args.Length == 1 && args[0] == "-Debug")
        {
            debugMode = true;
        }
        MonoGameInterface interfaceGame = new MonoGameInterface(debugMode);
        interfaceGame.Run();
        interfaceGame.Dispose();
    }
}