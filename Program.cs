using System;
public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        bool debug = false;
        if (args.Length == 1 && args[0].ToLower() == "debug")
        {
            debug = true;
        }
        MonoGameInterface interfaceGame = new MonoGameInterface(debug);
        interfaceGame.Run();
        interfaceGame.Dispose();
    }
}