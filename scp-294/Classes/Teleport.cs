using Exiled.API.Enums;
using Exiled.API.Features.Doors;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace scp_294.Classes
{
    public class Teleport
    {
        [Description("Whether or not the player is teleported")]
        public bool PlayerTeleport { get; set; }

        [Description("The zone to which the player will be teleported to. If this is anything but Unspecified it will teleport the player to a random room within that zone")]
        public ZoneType Zone { get; set; }

        [Description("Ignored if zone is anything other than Unspecified. Room that the player will teleport too. Set this to Unknown along with Zone Unspecified to teleport to a random place across the entire facility")]
        public RoomType Room { get; set; }

        public Vector3? GetTeleportLocation()
        {
            if(Zone == ZoneType.Unspecified && Room == RoomType.Unknown)
            {
                List<Door> doors = Door.List.Where(door => door.Rooms.Count > 1).ToList();
                Door door = doors[new System.Random().Next(doors.Count)];

                return door.Position + Vector3.up + door.Transform.forward;

            } else if(Zone == ZoneType.Unspecified)
            {
                List<Door> doors = Door.List.Where(door => door.Room.Type == Room).ToList();
                Door door = doors[new System.Random().Next(doors.Count)];

                return door.Position + Vector3.up + door.Transform.forward;

            } else if(Zone != ZoneType.Unspecified)
            {
                List<Door> doors = Door.List.Where(door => door.Zone == Zone).ToList();
                Door door = doors[new System.Random().Next(doors.Count)];

                return door.Position + Vector3.up + door.Transform.forward;
            }

            return null;
        }
    }
}
