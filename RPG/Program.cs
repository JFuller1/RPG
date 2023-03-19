global using static System.Console;
using System;
using RPG;

namespace RPG
{
    internal class Program
    {
        public static Game MainGame { get; set; }

        static void Main(string[] args)
        {
            Game game = new Game("Enter The Consolegeon");
            MainGame = game;

            game.StartGame();
        }
    }
}

