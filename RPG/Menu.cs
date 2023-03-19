using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal class Menu
    {
        private List<string> _Options { get; set; } = new List<string>();
        //Take in a list of weapons and armor iterates based on length of all lists
        public List<Weapon> Weapons { get; set; } = new List<Weapon>();
        public List<Armor> Armors { get; set; } = new List<Armor>();

        private int _Selected = 1;
        private string _Prompt { get; set; }


        public Menu(string prompt, List<string> options)
        {
            _Prompt = prompt;
            _Options = options;
        }

        public Menu(string prompt)
        {
            _Prompt = prompt;
        }

        public Menu(List<string> options)
        {
            _Options = options;
        }

        public void DisplayOptions()
        {
            if(_Prompt != null)
            {
                WriteLine(_Prompt);
            }

            for (int i = 0; i < _Options.Count; i++)
            {
                string currentOption = _Options[i];
                int currentIndex = i + 1;

                if (_Selected == currentIndex)
                {
                    BackgroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Black;
                }

                WriteLine($"{currentIndex}. {currentOption}", ForegroundColor, BackgroundColor);
                ResetColor();
            }
        }

        public void DisplayInventory()
        {
            Print.CreateWhiteSpace(1);
            ResetColor();

            Print.CharacterByCharacter(_Prompt, 0);

            for(int i = 0; i < Weapons.Count + Armors.Count; i++)
            {

                int armorSelect = Weapons.Count;
                if (_Selected == i + 1)
                {
                    BackgroundColor = ConsoleColor.White;
                }

                //Checks if its smaller than the index of the first piece of armor in the inventory
                if (i < armorSelect)
                {
                    Print.CharacterByCharacter($"{Weapons[i].Name}", 0, 1, Weapons[i].Foreground, BackgroundColor);
                } //else it is armor that is being selected 
                else
                {
                    Print.CharacterByCharacter($"{Armors[i - armorSelect].Name}", 0, 1, Armors[i - armorSelect].Foreground, BackgroundColor);
                }

                ResetColor();
            }

            Print.CreateWhiteSpace(2);
        }

        public int DisplayMenu()
        {
            ConsoleKey keyPressed;
            int count = _Options.Count;

            do
            {
                Clear();
                DisplayOptions();
                keyPressed = ReadKey().Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SwitchOption(true, count);
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SwitchOption(false, count);
                }

            } while (keyPressed != ConsoleKey.Enter);

            return _Selected;
        }
        
        //Display menu used for displaying the inventory
        public int DisplayMenu(List<Weapon> weapons, List<Armor> armors, Hero hero, int wait)
        {
            ConsoleKey keyPressed;

            if(weapons != null)
            {
                Weapons = weapons;
            }

            if (armors != null)
            {
                Armors = armors;
            }

            //used for looping through the menu
            int count = Weapons.Count + Armors.Count;

            do
            {
                Clear();
                //Prints current weapons
                Program.MainGame.GetHero().DisplayInventory(wait);
                DisplayInventory();
                keyPressed = ReadKey().Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SwitchOption(true, count);
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SwitchOption(false, count);
                }

                if(keyPressed == ConsoleKey.Escape)
                {
                    Program.MainGame.PlayerMenu();
                }

            } while (keyPressed != ConsoleKey.Enter);

            return _Selected;
        }

        private void SwitchOption(bool plus, int max)
        {
            if (plus)
            {
                if (_Selected == 1)
                {
                    _Selected = max;
                }
                else
                {
                    _Selected--;
                }
            }
            else
            {
                if (_Selected == max)
                {
                    _Selected = 1;
                }
                else
                {
                    _Selected++;
                }
            }
        }
    }
}
