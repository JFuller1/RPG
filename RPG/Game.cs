using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal class Game
    {
        public string Name { get; private set; }

        private Hero _Hero;
        private MonsterDataBase _Monsters = new MonsterDataBase();
        private WeaponDataBase _Weapons = new WeaponDataBase();
        private ArmorDataBase _Armors = new ArmorDataBase();

        public Dungeon Dungeon;

        //used in the players menu to see if its the start of the game and decides what message to display  
        private bool _Start = true;


        public Game(string name)
        {
            Name = name;
        }

        //Prints title and then creates the hero
        public void StartGame()
        {
            Print.OneWordAtATime(Name, 750, 2);

            Print.CharacterByCharacter("Please Enter Your Character's Name:", 50, 2);

            CreateHero();

            PlayerMenu();
        }

        private bool ValidateString(string str)
        {
            if (str.Any(char.IsDigit))
            {
                Print.CharacterByCharacter("Name Contains Numbers, Please Resubmit", 50, 2);
                return false;
            }
            else if(str.Length == 0 || str == " ")
            {
                Print.CharacterByCharacter("Name Is Empty, Pleas Resubmit", 50, 2);
                return false;
            }

            return true;
        }

        private void CreateHero()
        {
            string playerName = null;

            do
            {
                Console.Write("> ");
                playerName = ReadLine();
            } while (!ValidateString(playerName));

            _Hero = new Hero(playerName);
            Dungeon = new Dungeon();
        }

        public void PlayerMenu()
        {
            //Checks if you cleared the dungeon
            Dungeon.ClearedDungeon();

            string actionOption = "";

            //First time you enter the consolegeon, every time afterwords you enter the next room
            if (_Start)
            {
                actionOption = "Enter The Consolegeon";
            } else
            {
                actionOption = "Enter The Next Room";
            }

            List<string> options = new List<string>() { actionOption, "Show your inventory", "Show your stats", "Exit Game (You Will Lose All Progress!)"};

            Menu menu = new Menu($"{Dungeon.DisplayDungeon()}\n\n\n Choose An Option:", options);

            int selectedOption = menu.DisplayMenu();

            switch (selectedOption)
            {
                case 1:
                    //Changes the menus display from enter the consolegeon to enter next room
                    if (_Start)
                    {
                        _Start = false;
                        _Hero.EnterRoom();
                    }
                    else
                    {
                        _Hero.MovePlayer();
                    }

                    break;

                case 2:
                    _Hero.ShowInventory(50);
                    break;

                case 3:
                     _Hero.ShowStats();
                     break;

                case 4:
                    Environment.Exit(0);
                    break;
            }
        }

        //Returns a random weapon from the list of basic weapons
        public Weapon GetBasicWeapon()
        {
            return _Weapons.BasicWeapons[new Random().Next(_Weapons.BasicWeapons.Count)];
        }

        public Armor GetBasicArmor()
        {
            return _Armors.BasicArmors[new Random().Next(_Armors.BasicArmors.Count)];
        }

        //returns the hero
        public Hero GetHero()
        {
            return _Hero;
        }
    }
}
