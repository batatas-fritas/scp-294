using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace scp_294.Items.DrinkFeatures
{
    public class Teleport
    {
        /// <summary>
        /// Gets or sets whether or not the player should be teleported.
        /// </summary>
        [Description("Whether or not the player is teleported")]
        public bool PlayerTeleport { get; set; } = false;

        /// <summary>
        /// Gets or sets whether or not the player can teleport out of the pocket dimension.
        /// </summary>
        [Description("Whether or not the player can teleport out of the pocket dimension")]
        public bool CanPlayerEscapePocketDimension { get; set; } = false;

        /// <summary>
        /// Gets or sets the message that appears when a player is prevented from leaving the pocket dimension.
        /// </summary>
        [Description("Message that appears when player is prevented from leaving the pocket dimension")]
        public string MessagePreventingPocketTeleport { get; set; } = "A magical force prevents you from teleporting out of the pocket dimension";

        /// <summary>
        /// Gets or sets whether or not the player is able to teleport after the nuke has exploded.
        /// </summary>
        [Description("Whether or not the player is able to teleport after the nuke has exploded.")]
        public bool CanPlayerTeleportAfterNuke { get; set; } = false;

        /// <summary>
        /// Gets or sets the message that appears when a player is prevented from teleporting after the nuke has exploded.
        /// </summary>
        [Description("The message that appears when a player is prevented from teleporting after the nuke has exploded..")]
        public string MessagePreventingTeleportAfterNuke { get; set; } = "The nuke has exploded, you would end up locked up in the facility along with toxic gas.";

        /// <summary>
        /// Gets or sets the ZoneType the player will be teleported to.
        /// </summary>
        [Description("The zone to which the player will be teleported to. If this is anything but Unspecified it will teleport the player to a random room within that zone")]
        public ZoneType Zone { get; set; } = ZoneType.Unspecified;

        /// <summary>
        /// Gets or sets the RoomType the player will be teleported to.
        /// </summary>
        [Description("Ignored if zone is anything other than Unspecified. Room that the player will teleport too. Set this to Unknown along with Zone Unspecified to teleport to a random place across the entire facility")]
        public RoomType Room { get; set; } = RoomType.Unknown;

        /// <summary>
        /// Gets the teleport location.
        /// </summary>
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

        /// <summary>
        /// Tries to teleport the player.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> instance.</param>
        public void TryTeleport(Player player)
        {
            if (!CanPlayerEscapePocketDimension && player.CurrentRoom.Type == RoomType.Pocket)
            {
                Log.Debug($"TryTeleport: {MessagePreventingPocketTeleport}");
                player.ShowHint(MessagePreventingPocketTeleport);
                return;
            }

            if(!CanPlayerTeleportAfterNuke && Warhead.IsDetonated)
            {
                player.ShowHint(MessagePreventingTeleportAfterNuke);
                return;
            }

            player.Teleport(GetTeleportLocation());
        }
    }
}
