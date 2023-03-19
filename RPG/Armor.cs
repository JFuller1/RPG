using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal class Armor
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Defense { get; private set; }

        public ConsoleColor Foreground { get; }

        public Armor(string name, string description, int defense)
        {
            Name = name;
            Description = description;
            Defense = defense;

            Foreground = ConsoleColor.Blue;
        }
    }
}
