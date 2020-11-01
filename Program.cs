using Epsilon;
using System;
public static class Program
{
    [STAThread]
    public static void Main()
    {
        EpsilonGame game = new EpsilonGame();
        game.Initialize();
    }
}