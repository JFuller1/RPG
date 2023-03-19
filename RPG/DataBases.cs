using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{

    // File that contains classes that contain every creted Monster, Weapon, Armor
    internal class MonsterDataBase
    {
        public List<Monster> EasyMonsters { get; private set; }
        public List<Monster> MediumMonsters { get; private set; }
        public List<Monster> HardMonsters { get; private set; }

        public MonsterDataBase()
        {
            EasyMonsters = new List<Monster>()
            {
                new Monster("Goblin", 5, 0, 7, ConsoleColor.Green),
                new Monster("Skeleton", 7, 0, 2, ConsoleColor.White),
                new Monster("Vampire Bat", 5, 0, 4, ConsoleColor.Gray, 1),
                new BigMonster("Small Giant", 10, 1, 12, ConsoleColor.Yellow, 4, 1)
            };

            MediumMonsters = new List<Monster>()
            {
                new Monster("Giant Rat", 8, 2, 10, ConsoleColor.Gray),
                new Monster("Slime", 9, 0, 8, ConsoleColor.Green),
                new Monster("Dungeon Troll", 10, 0, 10, ConsoleColor.DarkGreen, 3)
            };

            HardMonsters = new List<Monster>()
            {
                new Monster("Skeleton Warrior", 12, 4, 12, ConsoleColor.White, 3),
                new Monster("Beholder", 10, 4, 15, ConsoleColor.Red, 3),
                new Monster("Giant Spider", 11, 0, 20, ConsoleColor.Magenta, 4),
                new BigMonster("Giant", 20, 3, 20, ConsoleColor.Yellow, 4, 1)
            };
        }
    }

    internal class WeaponDataBase
    {
        public List<Weapon> BasicWeapons { get; private set; }
        public List<Weapon> CommonDrops { get; private set; }
        public List<Weapon> UncommonDrops { get; private set; }
        public List<Weapon> RareDrops { get; private set; }

        public WeaponDataBase()
        {
            BasicWeapons = new List<Weapon>()
            {
                new Weapon("Stick", "Literally just a large stick", 2),
                new Weapon("Wooden Sword", "Looks like it will give you and your enemies splinters", 3),
                new Weapon("Slingshot", "Comes with an infinite supply of rocks", 2)
            };

            CommonDrops = new List<Weapon>()
            {
                new Weapon("Club", "Like the \"Stick\" but larger and with much less splinters", 3),
                new Weapon("Iron Sword", "So original", 4),
                new Weapon("Bow", "Comes with an infinite supply of arrows", 3)
            };

            UncommonDrops = new List<Weapon>()
            {
                new Weapon("Snake Sword", "Dont worry, the snake is friendly", 5),
                new Weapon("Grass Blade", "Suprisingly sharp", 5),
                new Weapon("Magical Yo-Yo", "Your enemies will be quivering in fear when they see you with this", 6)
            };

            RareDrops = new List<Weapon>()
            {
                new Weapon("Demon Blood Sword", "The demon the blood belonged too is totally not looking for this sword", 8),
                new Weapon("Glass Sword", "No refunds if it breaks", 9),
                new Weapon("Crossbow", "Comes with an infinite supply of arrows", 7)
            };
        }
    }

    internal class ArmorDataBase
    {
        public List<Armor> BasicArmors { get; private set; }
        public List<Armor> CommonDrops { get; private set; }
        public List<Armor> UncommonDrops { get; private set; }
        public List<Armor> RareDrops { get; private set; }

        public ArmorDataBase()
        {
            BasicArmors = new List<Armor>()
            {
                new Armor("Leather Armor", "Cheap and uncomfortable", 2),
                new Armor("Work Clothes", "Covered in dirt and barely able to protect you", 1),
                new Armor("Plastic Bag", "Bad at defense and for the earth", 1)
            };

            CommonDrops = new List<Armor>()
            {
                new Armor("Paper Bag", "A direct upgrade from the \"Plastic Bag\"", 3),
                new Armor("Studded Leather Armor", "Stronger than regualr leather, but not by much", 3),
                new Armor("Bear Hide", "Very warm but it works", 3)
            };

            UncommonDrops = new List<Armor>()
            {
                new Armor("Fabric Bag", "Even better than the flammable \"Paper Bag\"", 4),
                new Armor("Chainmail Armor", "What can I say, its a classic", 5),
                new Armor("Lizard Scale Armor", "Bonus points for looking cool", 4)
            };

            RareDrops = new List<Armor>()
            {
                new Armor("Magical Armor", "Probably has some interesting properties but I'll never program them", 6),
                new Armor("Chainmail Bag", "The strongest bag yet", 7),
                new Armor("(Nearly) Indestructible Armor", "Aside from nuclear warfare this armor should hold up pretty well against any foe", 8)
            };
        }
    }
}
