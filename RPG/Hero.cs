using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal class Hero
    {
        public string Name { get; private set; }
        private int _BaseStrength;
        private int _BaseDefense;
        private int _MaxHealth;
        private int _CurrentHealth;
        public Weapon EquippedWeapon { get; private set; }
        public Armor EquippedArmor { get; private set; }
        private List<Armor> _ArmorsBag = new List<Armor>();
        private List<Weapon> _WeaponsBag = new List<Weapon>();
        private Game _Game = Program.MainGame;
        private int _FightsWon;

        public Room CurrentRoom = null;
        private int _X = 0;
        private int _Y = 0;
        public KeyValuePair<int, int> Coords { get; private set; }


        public Hero(string name)
        {
            Random rnd = new Random();
            Name = name;

            //Base strength is slightly random per new character, ranging from  1 - 4
            _BaseStrength = rnd.Next(1, 5);

            //Base defense on generation ranges from 1-2
            _BaseDefense = rnd.Next(1, 3);

            //Max health ranges from 15-20 hit points
            _MaxHealth = rnd.Next(25, 31);
            _CurrentHealth = _MaxHealth;

            EquippedArmor = _Game.GetBasicArmor();
            EquippedWeapon = _Game.GetBasicWeapon();

            AddToInventory(_Game.GetBasicArmor());

            AddToInventory(_Game.GetBasicWeapon());

            AddToInventory(_Game.GetBasicArmor());

            AddToInventory(_Game.GetBasicWeapon());

            AddToInventory(_Game.GetBasicArmor());

            AddToInventory(_Game.GetBasicWeapon());

            AddToInventory(_Game.GetBasicArmor());

            AddToInventory(_Game.GetBasicWeapon());

            AddToInventory(_Game.GetBasicArmor());

            AddToInventory(_Game.GetBasicWeapon());

            AddToInventory(_Game.GetBasicArmor());

            AddToInventory(_Game.GetBasicWeapon());

        }

        public void ShowStats()
        {
            Clear();

            Print.CharacterByCharacter($"Showing {Name}'s stats", 100, 1);
            Print.PrintWithDelay($"Base Strength: {_BaseStrength}", 300, 1);
            Print.PrintWithDelay($"Base Defense: {_BaseDefense}", 300, 1);
            Print.PrintWithDelay($"Max Health: {_MaxHealth}", 300, 1);
            Print.PrintWithDelay($"Current Health: {_CurrentHealth}", 300, 1);
            Print.PrintWithDelay($"Fights Won: {_FightsWon}", 300, 1);

            ExitMenu();
        }

        public void ShowInventory(int wait)
        {
            Clear();

            DisplayInventory(wait);

            //If inventory is empty
            if (_ArmorsBag.Count == 0 && _WeaponsBag.Count == 0)
            {
                Print.CharacterByCharacter("You have nothing in your inventory", wait, 2);
                ExitMenu();
            } else
            {
                //Prints out inventory in a menu
                Menu menu = new Menu("Inventory: \n(Press Escape Key To Exit)\n\n");

                int selected = menu.DisplayMenu(_WeaponsBag, _ArmorsBag, this, 0);

                Clear();

                if (selected <= _WeaponsBag.Count)
                {
                    Weapon wep = _WeaponsBag[selected - 1];
                    ItemMenu(wep, PrintInfo(wep, 50));
                }
                else if (selected <= _WeaponsBag.Count + _ArmorsBag.Count)
                {
                    Armor arm = _ArmorsBag[selected - _WeaponsBag.Count - 1];
                    ItemMenu(arm, PrintInfo(arm, 50));
                }
            }
        }

        public void DisplayInventory(int wait)
        {
            Print.CharacterByCharacter("Current Weapon: ", wait, 0);
            Print.CharacterByCharacter(EquippedWeapon.Name, wait, 1, EquippedWeapon.Foreground);
            Print.CharacterByCharacter("Current Armor: ", wait, 0);
            Print.CharacterByCharacter(EquippedArmor.Name, wait, 1, EquippedArmor.Foreground);
        }

        public void ItemMenu(Weapon weapon, string info)
        {
            Menu equipMenu = new Menu(info, new List<string>() { "Equip Weapon", "Return To Inventory", "Exit" });
            int selectedEquip = equipMenu.DisplayMenu();

            switch (selectedEquip)
            {
                //Equips 
                case 1:
                    EquipWeapon(weapon);
                    ShowInventory(0);
                    break;

                //Return to inventory menu
                case 2:
                    ShowInventory(0);
                    break;
                
                //Return to main game
                case 3:
                    Program.MainGame.PlayerMenu();
                    break;
            }
        }

        public void ItemMenu(Armor armor, string info)
        {
            Menu equipMenu = new Menu(info, new List<string>() { "Equip Armor", "Return To Inventory", "Exit" });
            int selectedEquip = equipMenu.DisplayMenu();

            switch (selectedEquip)
            {
                case 1:
                    EquipArmor(armor);
                    ShowInventory(0);
                    break;

                case 2:
                    ShowInventory(0);
                    break;

                case 3:
                    Program.MainGame.PlayerMenu();
                    break;
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            //Adds current weapon to your inventory, equips the weapon you want and then removes it from your inventory
            _WeaponsBag.Add(EquippedWeapon);
            EquippedWeapon = weapon;
            _WeaponsBag.Remove(weapon);
        }

        public void EquipArmor(Armor armor)
        {
            _ArmorsBag.Add(EquippedArmor);
            EquippedArmor = armor;
            _ArmorsBag.Remove(armor);
        }

        public string PrintInfo(Weapon weapon, int wait)
        {
            string info = "";
            string name = weapon.Name;
            string description = weapon.Description;
            string comparision = $"Strength: {weapon.Strength} (your current weapon has {EquippedWeapon.Strength} strength)";

            Print.CharacterByCharacter(name, wait, 1);
            info += name + "\n";
            Print.CharacterByCharacter(description, wait, 1);
            info += description + "\n";
            Print.CharacterByCharacter(comparision, wait, 2);
            info += comparision + "\n";

            return info;

        }

        public string PrintInfo(Armor armor, int wait)
        {
            string info = "";
            string name = armor.Name;
            string description = armor.Description;
            string comparision = $"Defense: {armor.Defense} (your current armor has {EquippedArmor.Defense} defense)";

            Print.CharacterByCharacter(name, wait, 1);
            info += name + "\n";
            Print.CharacterByCharacter(description, wait, 1);
            info += description + "\n";
            Print.CharacterByCharacter(comparision, wait, 2);
            info += comparision + "\n";

            return info;
        }

        public void EnterRoom()
        {
            //sets current room according to coords
            Coords = new KeyValuePair<int, int>(_Y, _X);
            CurrentRoom = Program.MainGame.Dungeon.GetRoom(Coords);
            
            //calls rooms function
            CurrentRoom.RoomFunction();

            Program.MainGame.PlayerMenu();
        }

        public void MovePlayer()
        {
            Dungeon dungeon = Program.MainGame.Dungeon;
            List<string> options = dungeon.GetPossibleDirections(Coords);

            //creates menu with all possible directions
            Menu movementMenu = new Menu($"{dungeon.DisplayDungeon()}\n\n\nPick a direction", options);

            // - 1 for proper index
            int selected = movementMenu.DisplayMenu() - 1;

            if (options[selected].Contains("Right"))
            {
                _X++;
            } 
            else if (options[selected].Contains("Left"))
            {
                _X--;
            } 
            else if (options[selected].Contains("Up"))
            {
                _Y--;
            }
            else
            {
                _Y++;
            }

            EnterRoom();
        }

        public static void ExitMenu()
        {
            Print.CharacterByCharacter("Enter any character to continue", 50, 1);
            ReadKey();

            Program.MainGame.PlayerMenu();
        }

        public int TakeDamage(int dmg, bool ignoreDefense = false)
        {
            if (!ignoreDefense)
            {
                int totalDamage = dmg - (_BaseDefense + EquippedArmor.Defense);

                // damge minus defense is smaller than 0 set it to 0
                if (totalDamage < 0)
                {
                    totalDamage = 0;
                }

                _CurrentHealth -= totalDamage;

                return totalDamage;
            }
            else
            {
                _CurrentHealth -= dmg;
                return dmg;
            }
        }

        public void HealToMax()
        {
            _CurrentHealth = _MaxHealth;
        }

        public void Heal(int amt)
        {
            if(_CurrentHealth + amt >= _MaxHealth)
            {
                _CurrentHealth = _MaxHealth;
            }
            else
            {
                _CurrentHealth += amt;
            }
        }

        public int GetHealth()
        {
            return _CurrentHealth;
        }

        public void WinFight()
        {
            _FightsWon++;
        }

        public int TotalDamage()
        {
            return _BaseStrength + EquippedWeapon.Strength;
        }

        public void AddToInventory(Armor armor)
        {
            //My previous method of avoiding doubles didnt work and this does even tho its a bit of spaghetti
            HashSet<Armor> armors = _ArmorsBag.ToHashSet();
            if(EquippedArmor != armor)
            {
                armors.Add(armor);
            }
            _ArmorsBag = armors.ToList();
        }

        public void AddToInventory(Weapon weapon)
        {
            HashSet<Weapon> weapons = _WeaponsBag.ToHashSet();
            if(EquippedWeapon != weapon)
            {
                weapons.Add(weapon);
            }
            _WeaponsBag = weapons.ToList();
        }
    }
}
