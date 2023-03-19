using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal class Weapon
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Strength { get; private set; }

        public ConsoleColor Foreground { get; }

        public Weapon(string name, string description, int strength)
        {
            Name = name;
            Description = description;
            Strength = strength;

            Foreground = ConsoleColor.Red;
        }
    }
}
