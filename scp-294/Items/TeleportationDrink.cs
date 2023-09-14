using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Player = Exiled.Events.Handlers.Player;
using System;
using System.ComponentModel;
using scp_294.Scp;
using Exiled.API.Features.Doors;
using System.Linq;
using System.Collections.Generic;
using Exiled.API.Enums;
using UnityEngine;

namespace scp_294.Items
{
    [CustomItem(ItemType.AntiSCP207)]
    public class TeleportationDrink : CustomItem
    {
        public override uint Id { get; set; } = 70;
        public override string Name { get; set; } = "drink of chorus fruit";
        [Description("Text that shows once you hold the drink")]
        public override string Description { get; set; } = "Ever played minecraft? Then you know what's about to go down.";
        public bool IsEnable { get; set; } = true;
        public override float Weight { get; set; } = 1f;
        public override ItemType Type { get; set; } = ItemType.AntiSCP207;
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1, // Irrelevant: determines the maximum of how many will spawn (they will not spawn in the map)
        };

        [Description("The zone to which the player will be teleported to. If this is anything but Unspecified it will teleport the player to a random room within that zone")]
        public ZoneType Zone { get; set; } = ZoneType.Unspecified;

        [Description("Ignored if zone is anything other than Unspecified. Room that the player will teleport too. Set this to Unknown along with Zone Unspecified to teleport to a random place across the entire facility")]
        public RoomType Room { get; set; } = RoomType.Unknown;

        protected override void SubscribeEvents()
        {
            Player.UsedItem += UsedItem;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.UsedItem -= UsedItem;
            base.UnsubscribeEvents();
        }

        private void UsedItem(UsedItemEventArgs ev)
        {
            if (Check(ev.Item))
            {
                Scp294.RemoveAntiScp207(ev.Player);
             
                ev.Player.Teleport(GetTeleportLocation());
            }
        }

        private Vector3? GetTeleportLocation()
        {
            if (Zone == ZoneType.Unspecified && Room == RoomType.Unknown)
            {
                List<Door> doors = Door.List.Where(door => door.Rooms.Count > 1).ToList();
                Door door = doors[new System.Random().Next(doors.Count)];

                return door.Position + Vector3.up + door.Transform.forward;

            }
            else if (Zone == ZoneType.Unspecified)
            {
                List<Door> doors = Door.List.Where(door => door.Room.Type == Room).ToList();
                Door door = doors[new System.Random().Next(doors.Count)];

                return door.Position + Vector3.up + door.Transform.forward;

            }
            else if (Zone != ZoneType.Unspecified)
            {
                List<Door> doors = Door.List.Where(door => door.Zone == Zone).ToList();
                Door door = doors[new System.Random().Next(doors.Count)];

                return door.Position + Vector3.up + door.Transform.forward;
            }

            return null;
        }
    }
}
