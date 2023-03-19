using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal class Dungeon
    {
        private List<IRoomEvent> _RepeatingEvents { get; set; } = new List<IRoomEvent>();
        private List<IRoomEvent> _SingleEvents { get; set; } = new List<IRoomEvent>();
        private Game _Game = Program.MainGame;
        private Hero _Player { get; set; }
        private List<List<Room>> _Rooms = new List<List<Room>>();

        public Dungeon()
        {
            _Player = _Game.GetHero();
            string name = _Player.Name;

            //creates all the possible rooms

            //RepeatingEvents are room events that can be created many times
            _RepeatingEvents.Add(new CombatRoom($"{name} finds some monsters!"));
            _RepeatingEvents.Add(new CombatRoom($"{name} enters a fight!"));
            _RepeatingEvents.Add(new CombatRoom($"{name} gets ambushed!"));
            _RepeatingEvents.Add(new CombatRoom($"{name} gets attacked!"));
            _RepeatingEvents.Add(new EmptyRoom($"{name} enters an empty room"));

            //SingleEvents are special rooms that are only created once 
            _SingleEvents.Add(new Trap($"{name} enters a room with a spike trap", $"{name} dodged the spike trap with ease", $"{name} got impaled by a spike", 3));
            _SingleEvents.Add(new Trap($"{name} enters a room with a fire trap", $"{name} evaded the fire", $"{name} got some 2nd degree burns", 5));
            _SingleEvents.Add(new Trap($"{name} enters a room with a dart trap", $"{name} caught the dart mid-air", $"{name} got hit by the dart, which was probably not poisoned", 2, 2));
            _SingleEvents.Add(new Trap($"As {name} enters the next room they set off a boulder trap", $"{name} escaped the boulder", $"{name} got crushed", 15, 4));
            _SingleEvents.Add(new HealingRoom($"{name} enters a room with a majestic fountain. Feeling thirsty, {name} approaches the fountain and takes a few handfulls of water. As the water trickles down their throat a sensation of utter peace falls upon them", 0, true));
            _SingleEvents.Add(new HealingRoom($"{name} enters a room with a large statue of a majestic goddess. As they approach the statue a calm aura passes over their body", 5, false));
            _SingleEvents.Add(new TrickRoom($"{name} enters a room and finds an apple sitting on a table. Next to the apple is a note that reads \"Eat Me\".", new List<string> { "Follow the notes instructions and eat the apple", "Don't eat the apple, it's obviously a trap" }, $"Not only was the apple perfectly fine, it actually made {name} feel much healthier", $"{name} leaves the room without eating the apple", 0, 5));
            _SingleEvents.Add(new TrickRoom($"{name} enters a room and finds a vile containg an opaque orange liquid", new List<string> { "Drink it, whats the worst that could happen?", "Don't drink it, for all you know that liquid could kill you!" }, $"The liquid makes {name} feel sick and {name} throws up their lunch", $"{name} leaves the room without drinking the liquids", 5, 0));


            _Rooms = CreateLayout();
        }

        public List<List<Room>> CreateLayout()
        {
            //Layout of what the dungeon will look like
            return new List<List<Room>>
            {
                new List<Room> { CreateRoom("═══", false), CreateRoom("═╦═"),        CreateRoom("═══"), CreateRoom("═══"), CreateRoom("═╗ ") },
                new List<Room> { CreateRoom(),             CreateRoom(" ║ "),        CreateRoom(),      CreateRoom(),      CreateRoom(" ║ ") },
                new List<Room> { CreateRoom(" ══"),        CreateRoom("═╣ "),        CreateRoom(),      CreateRoom(),      CreateRoom()      },
                new List<Room> { CreateRoom(),             CreateRoom(" ╚═"),        CreateRoom("═╦═"), CreateRoom("══ "), CreateRoom()      },
                new List<Room> { CreateRoom(),             CreateRoom(),             CreateRoom(" ║ "), CreateRoom(),      CreateRoom()      },
                new List<Room> { CreateRoom(" ══"),        CreateRoom("═══"),        CreateRoom("═╩═"), CreateRoom("═══"), CreateRoom("══ ") }
            };
        }

        private Room CreateRoom()
        {
            //Creates a room with status of Empty
            return new Room();
        }

        private Room CreateRoom(string mapDisplay, bool special = true)
        {
            Room room = null;

            //special is used to make sure certain rooms dont have the chance to be a special room

            if(new Random().Next(0,4) == 0 && _SingleEvents.Count > 0 && special)
            {
                int num = new Random().Next(_SingleEvents.Count);
                room = new Room(_SingleEvents[num], mapDisplay);
                _SingleEvents.RemoveAt(num);
            }
            else
            {
                room = new Room(_RepeatingEvents[new Random().Next(_RepeatingEvents.Count)], mapDisplay);
            }

            return room;
        }

        public string DisplayDungeon()
        {
            //gets the character that will be displayed on the map
            char first = _Player.Name[0];
            string map = "Dungeon Map:\n";
            foreach (List<Room> row in _Rooms)
            {
                foreach(Room room in row)
                {
                    //If the room is actually a room with an event
                    if(room.Status != RoomStatus.Empty)
                    {
                        //checks if that is where the player currently is
                        if (_Player.CurrentRoom == room)
                        {
                            map += $" {first} ";
                        }
                        else
                        {
                            map += room.MapDisplay;
                        }
                    } else
                    {
                        map += "   ";
                    }
                }

                map += "\n";
            }

            return map;
        }

        //Returns the room based on coords
        public Room GetRoom(KeyValuePair<int, int> coords)
        {
            return _Rooms[coords.Key][coords.Value];
        }

        public List<string> GetPossibleDirections(KeyValuePair<int, int> coords)
        {
            List<string> options = new List<string>();

            //Checks all the directions you can go (right, left, up or down)

            if (coords.Key + 1 < _Rooms.Count)
            {
                if(_Rooms[coords.Key + 1][coords.Value].Status != RoomStatus.Empty)
                {
                    options.Add("Move Down");
                }
            } 
            
            if(coords.Key - 1 > -1)
            {
                if(_Rooms[coords.Key - 1][coords.Value].Status != RoomStatus.Empty)
                {
                    options.Add("Move Up");
                }
            }
            
            if(coords.Value + 1 < _Rooms[0].Count)
            {
                if (_Rooms[coords.Key][coords.Value + 1].Status != RoomStatus.Empty)
                {
                    options.Add("Move Right");
                }
            }

            if(coords.Value - 1 > -1)
            {
                if(_Rooms[coords.Key][coords.Value - 1].Status != RoomStatus.Empty)
                {
                    options.Add("Move Left");
                }
            }

            return options;
        }

        public void ClearedDungeon()
        {
            bool cleared = true;
            foreach (List<Room> row in _Rooms)
            {
                foreach (Room room in row)
                {
                    if(room.Status == RoomStatus.UnCleared)
                    {
                        cleared = false;
                    }
                }
            }

            if (cleared)
            {
                Clear();
                Print.CharacterByCharacter("YOU WIN", 100);

                Environment.Exit(0);
            }
        }
    }
}
