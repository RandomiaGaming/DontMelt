using System;
namespace DontMelt
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            MonoGameInterface game = new MonoGameInterface();
            game.Run();
            game.Dispose();
        }
    }
}
