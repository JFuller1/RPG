using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal enum RoomStatus
    {
        //Used to check if you have already been in the room (Cleared) or if the room doesnt have any event (Empty) or if you have yet to interact with it (Uncleared)
        UnCleared,
        Cleared,
        Empty
    }

    internal class Room
    {
        public IRoomEvent Event { get; set; }
        public RoomStatus Status { get; set; }
        private Hero _Player = Program.MainGame.GetHero();
        // chooses which character will be displayed on the map
        public string MapDisplay = "";

        public Room(IRoomEvent @event, string mapDisplay)
        {
            Event = @event;
            Status = RoomStatus.UnCleared;
            MapDisplay = mapDisplay;
        }

        public Room()
        {
            Status = RoomStatus.Empty;
        }

        public void RoomFunction()
        {
            if(Status == RoomStatus.UnCleared)
            {
                Status = RoomStatus.Cleared;
                Event.RoomFunction();
            }
        }
    }
}
