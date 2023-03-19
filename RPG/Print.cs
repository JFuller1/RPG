using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal class Print
    {
        public Print()
        {

        }

        public static void PrintWithDelay(string txt, int wait, int whitespace = 0, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black )
        {
            Write(txt, ForegroundColor = foregroundColor, BackgroundColor = backgroundColor);
            Thread.Sleep(wait);
            CreateWhiteSpace(whitespace);
        }

        public static void OneWordAtATime(string txt, int wait, int whitespace = 0, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            foreach (string word in txt.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Write($"{word} ", ForegroundColor = foregroundColor, BackgroundColor = backgroundColor);

                Thread.Sleep(wait);
            }
            CreateWhiteSpace(whitespace);
        }

        public static void CharacterByCharacter(string txt, int wait, int whitespace = 0, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {

            foreach (char c in txt)
            {
                Write(c.ToString(), ForegroundColor = foregroundColor, BackgroundColor = backgroundColor);
                Thread.Sleep(wait);
            }
            CreateWhiteSpace(whitespace);
        }

        public static void CreateWhiteSpace(int amt)
        {
            for (int i = 0; i < amt; i++)
            {
                WriteLine();
            }
        }
    }
}
