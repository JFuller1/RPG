using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal interface IRoomEvent
    {
        string DisplayText { get; }

        void RoomFunction() { } 
    }

    internal class Trap : IRoomEvent
    {
        public string DisplayText { get; set; }
        private string _DodgeText { get; set; }
        private string _HitText { get; set; }

        private int _Dmg { get; set; }
        private int _Dodge { get; set; }

        private Hero _Hero = Program.MainGame.GetHero();

        public Trap(string displayText, string dodgeText, string hitText, int dmg, int dodge = 3)
        {
            DisplayText = displayText;
            _DodgeText = dodgeText;
            _HitText = hitText;
            _Dmg = dmg;
            _Dodge = dodge;
        }

        public void RoomFunction() 
        {
            Clear();

            Print.CharacterByCharacter(DisplayText, 50, 2);

            Print.CharacterByCharacter("...", 325, 2);

            if (new Random().Next(0,_Dodge) == 0)
            {
                //ignores defense
                _Hero.TakeDamage(_Dmg, true);
                Print.CharacterByCharacter(_HitText, 50, 2);
                Print.CharacterByCharacter($"{_Dmg} damage taken", 50, 2, ConsoleColor.Red);
                ResetColor();
            }
            else
            {
                Print.CharacterByCharacter(_DodgeText, 50, 2);
            }

            Thread.Sleep(1000);
        }
    }

    internal class CombatRoom : IRoomEvent
    {
        public string DisplayText { get; set; }

        private Hero _Hero = Program.MainGame.GetHero();
        private List<Monster> _CurrentFight = new List<Monster>();

        public CombatRoom(string displayText)
        {
            DisplayText = displayText;
        }

        public void RoomFunction()
        {
            Clear();

            Print.CharacterByCharacter(DisplayText, 50, 2);

            Monster monster = null;

            //Only can have one type of monster in find (ie cant have two goblins)

            if (_Hero.Coords.Key <= 1 && _Hero.Coords.Value <= 1)
            {
                MonsterDataBase _Monsters = new MonsterDataBase();

                //at most 3 easy monsters
                for (int i = 0; i <= new Random().Next(3); i++)
                {
                    monster = _Monsters.EasyMonsters[new Random().Next(_Monsters.EasyMonsters.Count)];
                    _CurrentFight.Add(monster);
                    _Monsters.EasyMonsters.Remove(monster);
                }
            } else if(_Hero.Coords.Key <= 3 && _Hero.Coords.Value <= 3)
            {
                // One medium monster and at most two easy monster
                bool medium = false;
                MonsterDataBase _Monsters = new MonsterDataBase();
                for (int i = 0; i <= new Random().Next(3); i++)
                {
                    if (!medium)
                    {
                        monster = _Monsters.MediumMonsters[new Random().Next(_Monsters.MediumMonsters.Count)];
                        _CurrentFight.Add(monster);
                        _Monsters.MediumMonsters.Remove(monster);
                        medium = true;
                    } else
                    {
                        monster = _Monsters.EasyMonsters[new Random().Next(_Monsters.EasyMonsters.Count)];
                        _CurrentFight.Add(monster);
                        _Monsters.EasyMonsters.Remove(monster);
                    }
                }
            } else
            {
                bool hard = false;
                MonsterDataBase _Monsters = new MonsterDataBase();
                for (int i = 0; i <= new Random().Next(3); i++)
                {
                    // One hard monster and at most two medium monsters
                    if (!hard)
                    {
                        monster = _Monsters.HardMonsters[new Random().Next(_Monsters.HardMonsters.Count)];
                        _CurrentFight.Add(monster);
                        _Monsters.HardMonsters.Remove(monster);
                        hard = true;
                    }
                    else
                    {
                        monster = _Monsters.MediumMonsters[new Random().Next(_Monsters.MediumMonsters.Count)];
                        _CurrentFight.Add(monster);
                        _Monsters.MediumMonsters.Remove(monster);
                    }
                }
            }

            if(_CurrentFight.Count > 1)
            {
                Print.CharacterByCharacter($"{_CurrentFight.Count} monsters approach {_Hero.Name}:", 50, 1);
            } else
            {
                Print.CharacterByCharacter($"{_CurrentFight.Count} monster approaches {_Hero.Name}:", 50, 1);
            }

            Print.CreateWhiteSpace(1);

            foreach(Monster enemy in _CurrentFight)
            {
                Print.CharacterByCharacter(enemy.Name, 50, 1, enemy.Colour);
            }

            Print.CreateWhiteSpace(1);

            ResetColor();
            Thread.Sleep(1000);

            Fight fight = new Fight(_CurrentFight, _Hero);
            fight.StartFight();
            _CurrentFight.Clear();
        }
    }

    internal class EmptyRoom : IRoomEvent
    {
        public string DisplayText { get; set; }

        public EmptyRoom(string displayText)
        {
            DisplayText = displayText;
        }

        public void RoomFunction()
        {
            Clear();

            Print.CharacterByCharacter(DisplayText, 50, 2);

            Thread.Sleep(1000);
        }
    }

    internal class HealingRoom : IRoomEvent
    {
        public string DisplayText { get; set; }
        private int _Healed { get; set; }
        private bool _ToMax { get; set; }

        private Hero _Hero = Program.MainGame.GetHero();

        public HealingRoom(string displayText, int healed, bool toMax = false)
        {
            DisplayText = displayText;
            _Healed = healed;
            _ToMax = toMax;
        }

        public void RoomFunction()
        {
            Clear();

            Print.CharacterByCharacter(DisplayText, 50, 2);

            if (_ToMax)
            {
                Print.CharacterByCharacter($"Healed to max health", 50, 2, ConsoleColor.Green);
                _Hero.HealToMax();
            } 
            else
            {
                Print.CharacterByCharacter($"Healed {_Healed} health", 50, 2, ConsoleColor.Green);
                _Hero.Heal(_Healed);
            }

            ResetColor();
            Thread.Sleep(1000);
        }
    }

    internal class TrickRoom : IRoomEvent
    {
        public string DisplayText { get; set; }
        public List<string> Options { get; set; }
        public string Aftermath1 { get; set; }
        public string Aftermath2 { get; set; }

        private int _Dmg { get; set; }
        private int _Heal { get; set; }

        private Hero _Hero = Program.MainGame.GetHero();


        public TrickRoom(string displayText, List<string> options, string aftermath1, string aftermath2, int dmg, int heal)
        {
            DisplayText = displayText;
            Options = options;
            Aftermath1 = aftermath1;
            Aftermath2 = aftermath2;
            _Dmg = dmg;
            _Heal = heal;
        }

        public void RoomFunction()
        {
            //Displays 2 options, one will interact with whatever the room is (and either get healed or hurt) and the other is just continue on
            Clear();

            Print.CharacterByCharacter(DisplayText, 50, 2);

            Menu menu = new Menu(DisplayText + "\n", Options);
            int selected = menu.DisplayMenu();
            Print.CreateWhiteSpace(1);

            switch (selected)
            {
                case 1:
                    Print.CharacterByCharacter(Aftermath1, 50, 2);

                    if(_Dmg != 0)
                    {
                        _Hero.TakeDamage(_Dmg, true);
                        Print.CharacterByCharacter($"{_Dmg} Damage taken", 50, 2, ConsoleColor.Red);
                        ResetColor();
                    }
                    else
                    {
                        _Hero.Heal(_Heal);
                        Print.CharacterByCharacter($"Healed {_Heal} health", 50, 2, ConsoleColor.Green);
                        ResetColor();
                    }

                    break;

                case 2:
                    Print.CharacterByCharacter(Aftermath2, 50, 2);
                    break;

                default:

                    break;
            }

            Thread.Sleep(1000);
        }
    }
}
