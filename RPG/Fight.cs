using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal class Fight
    {
        private List<Monster> _Monsters { get; set; } = new List<Monster>();
        private Hero _Hero { get; set; }
        private bool _StopCombat = false;
        private WeaponDataBase _Weapons = new WeaponDataBase();
        private ArmorDataBase _Armors = new ArmorDataBase();

        public Fight(List<Monster> monsters, Hero hero)
        {
            _Monsters = monsters;
            _Hero = hero;
        }

        public void StartFight()
        {
            while(!_StopCombat)
            {
                if (_Hero.GetHealth() > 0)
                {
                    HeroTurn();
                }
                else
                {
                    _StopCombat = true;
                }

                //If hero died no need to call the enemies turn
                if (!_StopCombat)
                {
                    // Checks if hero won on their turn
                    if (_Monsters.Count > 0)
                    {
                        EnemyTurn();
                    }
                    else
                    {
                        _StopCombat = true;
                    }
                }
            }

            if(_Monsters.Count == 0) 
            {
                Print.CharacterByCharacter($"{_Hero.Name} won the fight!", 50, 2);

                //Get monsters drops

                _Hero.WinFight();
                Thread.Sleep(500);
            }

            if(_Hero.GetHealth() <= 0)
            {
                Print.CharacterByCharacter($"{_Hero.Name} lost the fight...", 50, 2);

                Environment.Exit(0);
            }
        }

        public void HeroTurn()
        {
            //All enemies are added to this list
            List<string> attackOptions = new List<string>();

            foreach(Monster monster in _Monsters)
            {
                attackOptions.Add($"Attack {monster.Name}");
            }

            Menu attackMenu = new Menu($"Current Health: {_Hero.GetHealth()}\n", attackOptions);

            int selected = attackMenu.DisplayMenu();
            Monster hitMonster = _Monsters[selected - 1];

            Print.CreateWhiteSpace(1);

            Print.CharacterByCharacter($"Attacking ", 50);
            Print.CharacterByCharacter(hitMonster.Name, 50, 2, hitMonster.Colour);
            ResetColor();

            Print.CharacterByCharacter("...", 325, 2);

            //4 in 5 chance to hit
            if(new Random().Next(5) > 0)
            {
                Print.CharacterByCharacter("Hit!", 50, 2);

                hitMonster.TakeDamage(_Hero.TotalDamage());

                if (hitMonster.IsDead())
                {
                    _Monsters.Remove(hitMonster);
                    // See's what the enemy will drop upon death
                    DropItem(hitMonster);
                }
            } else
            {
                Print.CharacterByCharacter("Miss!", 50, 2);
            }

        }

        public void DropItem(Monster monster)
        {
            Random rndm = new Random();
            if(rndm.Next(2) == 0)
            {
                int dropNum = rndm.Next(0, 20);
                Weapon weapon = null;

                if(dropNum <= 13)
                {
                    weapon = _Weapons.CommonDrops[rndm.Next(_Weapons.CommonDrops.Count)];   
                } 
                else if(dropNum <= 18)
                {
                    weapon = _Weapons.UncommonDrops[rndm.Next(_Weapons.UncommonDrops.Count)];
                } 
                else
                {
                    weapon = _Weapons.RareDrops[rndm.Next(_Weapons.RareDrops.Count)];
                }

                Print.CharacterByCharacter(monster.Name, 50, 0, monster.Colour);
                Print.CharacterByCharacter($" dropped ", 50);
                Print.CharacterByCharacter(weapon.Name, 50, 2, weapon.Foreground);
                _Hero.AddToInventory(weapon);
            }
            else
            {
                int dropNum = rndm.Next(0, 20);
                Armor armor = null;

                if (dropNum <= 13)
                {
                    armor = _Armors.CommonDrops[rndm.Next(_Armors.CommonDrops.Count)];
                }
                else if (dropNum <= 18)
                {
                    armor = _Armors.UncommonDrops[rndm.Next(_Armors.UncommonDrops.Count)];
                }
                else
                {
                    armor = _Armors.RareDrops[rndm.Next(_Armors.RareDrops.Count)];
                }

                Print.CharacterByCharacter(monster.Name, 50, 0, monster.Colour);
                Print.CharacterByCharacter($" dropped ", 50);
                Print.CharacterByCharacter(armor.Name, 50, 2, armor.Foreground);
                _Hero.AddToInventory(armor);
            }
        }

        public void EnemyTurn()
        {
            foreach(Monster monster in _Monsters)
            {
                //Checks if the monster is a special type and calls its specific attack
                if (monster is BigMonster)
                {
                    BigMonster bigMonster = (BigMonster)monster;

                    bigMonster.Attack();
                }
                else
                {
                    Print.CharacterByCharacter(monster.Name, 50, 0, monster.Colour);
                    Print.CharacterByCharacter($" attacks {_Hero.Name}", 50, 2);
                    Print.CharacterByCharacter("...", 325, 2);

                    if (new Random().Next(monster.Hit) == 0)
                    {
                        monster.Attack();
                    }
                    else
                    {
                        Print.CharacterByCharacter("Miss!", 50, 2);
                    }
                }
            }
        }
    }
}
