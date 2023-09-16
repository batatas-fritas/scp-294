using Exiled.API.Enums;
using Exiled.API.Features;
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
        public bool PlayerTeleport { get; set; } = false;

        [Description("Whether or not the player can teleport out of the pocket dimension")]
        public bool CanPlayerEscapePocketDimension { get; set; } = false;

        [Description("Message that appears when player is prevented from leaving the pocket dimension")]
        public string MessagePreventingPocketTeleport { get; set; } = "";

        [Description("The zone to which the player will be teleported to. If this is anything but Unspecified it will teleport the player to a random room within that zone")]
        public ZoneType Zone { get; set; } = ZoneType.Unspecified;

        [Description("Ignored if zone is anything other than Unspecified. Room that the player will teleport too. Set this to Unknown along with Zone Unspecified to teleport to a random place across the entire facility")]
        public RoomType Room { get; set; } = RoomType.Unknown;

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

        public void TryTeleport(Player player)
        {
            if (!CanPlayerEscapePocketDimension && player.CurrentRoom.Type == RoomType.Pocket)
            {
                Log.Debug($"TryTeleport: {MessagePreventingPocketTeleport}");
                player.ShowHint(MessagePreventingPocketTeleport);
                return;
            }

            player.Teleport(GetTeleportLocation());
        }
    }
}
