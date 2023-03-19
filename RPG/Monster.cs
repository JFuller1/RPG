using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPG
{
    internal class Monster
    {
        public string Name { get; private set; }
        public ConsoleColor Colour { get; set; }
        public int Strength { get; private set; }
        public int Defense { get; private set; }
        private int _MaxHealth { get; set; }
        private int _CurrentHealth;

        public int Hit { get; set; }

        public Monster(string name, int strength, int defense, int maxHealth, ConsoleColor colour, int hit = 2)
        {
            Name = name;
            Strength = strength;
            Defense = defense;
            _MaxHealth = maxHealth;
            _CurrentHealth = maxHealth;
            Hit = hit;
            Colour = colour;
        }

        public void Attack()
        {
            Hero _Hero = Program.MainGame.GetHero();
            int dmgMinusDefense = _Hero.TakeDamage(Strength);

            //Checks if with after calculating the hero's defense if any damage will be done
            if(dmgMinusDefense > 0)
            {
                Print.CharacterByCharacter("Hit!", 50, 2);
                Print.CharacterByCharacter($"{_Hero.Name} takes ", 50, 0);
                Print.CharacterByCharacter($"{dmgMinusDefense} damage", 50, 2, ConsoleColor.Red);
            }
            else
            {
                Print.CharacterByCharacter($"{_Hero.Name}'s armor completely negated the damage!", 50, 2);
            }

            ResetColor();
            Thread.Sleep(500);
        }

        public void TakeDamage(int dmg)
        {
            int totalDamage = dmg - Defense;

            if (totalDamage <= 0)
            {
                Print.CharacterByCharacter(Name, 50, 0, Colour);
                Print.CharacterByCharacter($"'s armor completely negated the damage!", 50, 2);
            } else
            {
                _CurrentHealth -= totalDamage;
                Print.CharacterByCharacter(Name, 50, 0, Colour);
                Print.CharacterByCharacter($" takes ", 50);
                Print.CharacterByCharacter($"{totalDamage} damage", 50, 2, ConsoleColor.Red);
            }
        }

        public bool IsDead()
        {
            if(_CurrentHealth <= 0)
            {
                Print.CharacterByCharacter(Name, 50, 0, Colour);
                Print.CharacterByCharacter($" has died", 50, 2);
                return true;
            } 
            else
            {
                return false;
            }
        }
    }

    internal class BigMonster : Monster
    {
        private int Timer = 0;
        private int MaxTime { get; set; }

        public BigMonster(string name, int strength, int defense, int maxHealth, ConsoleColor colour, int maxTime, int hit = 2) : base(name, strength, defense, maxHealth, colour, hit)
        {
            MaxTime = maxTime;
        }

        //Special feature of the big monster is that it has a timer for attack, bigger attack but attack takes longer
        public new void Attack()
        {
            Timer++;

            if(Timer == MaxTime)
            {
                Print.CharacterByCharacter(Name, 50, 0, Colour);
                Print.CharacterByCharacter($" attacks {Program.MainGame.GetHero().Name}", 50, 2);
                Print.CharacterByCharacter("...", 325, 2);

                Hero _Hero = Program.MainGame.GetHero();
                int dmgMinusDefense = _Hero.TakeDamage(Strength);

                if (dmgMinusDefense > 0)
                {
                    Print.CharacterByCharacter("Hit!", 50, 2);
                    Print.CharacterByCharacter($"{_Hero.Name} takes ", 50, 0);
                    Print.CharacterByCharacter($"{dmgMinusDefense} damage", 50, 2, ConsoleColor.Red);
                }
                else
                {
                    Print.CharacterByCharacter($"{_Hero.Name}'s armor completely negated the damage!", 50, 2);
                }

                Timer = 0;
                ResetColor();
                Thread.Sleep(500);
            }
            else
            {
                Print.CharacterByCharacter(Name, 50, 0, Colour);
                ResetColor();
                Print.CharacterByCharacter(" is charging up a big attack", 50, 2);

                int timeUntilAttack = MaxTime - Timer;

                if(timeUntilAttack > 1)
                {
                    Print.CharacterByCharacter($"{timeUntilAttack} turns until ", 50);
                    Print.CharacterByCharacter(Name, 50, 0, ForegroundColor);
                    Print.CharacterByCharacter(" attacks", 50, 2);
                }
                else
                {
                    Print.CharacterByCharacter($"{timeUntilAttack} turn until ", 50);
                    Print.CharacterByCharacter(Name, 50, 0, ForegroundColor);
                    Print.CharacterByCharacter(" attacks", 50, 2);
                }
            }
        }
    }
}
