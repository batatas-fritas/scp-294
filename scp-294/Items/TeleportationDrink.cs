﻿using Exiled.API.Features.Attributes;
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

namespace scp_294.Items
{
    [CustomItem(ItemType.AntiSCP207)]
    public class TeleportationDrink : CustomItem
    {
        public override uint Id { get; set; } = 70;
        public override string Name { get; set; } = "drink of chorus fruit";
        [Description("Text that shows once you hold the drink")]
        public override string Description { get; set; } = "Ever played minecraft? Then you know what's about to go down.";
        [Description("Weight of the drink. Higher weights -> move slower")]
        public override float Weight { get; set; } = 1f;
        public override ItemType Type { get; set; } = ItemType.AntiSCP207;
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1, // Irrelevant: determines the maximum of how many will spawn (they will not spawn in the map)
        };

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

                List<Door> doors = Door.List.Where(door => door.Rooms.Count > 1).ToList();
                Door door = doors[new Random().Next(doors.Count)];
                
                ev.Player.Teleport(door, door.Transform.forward);
            }
        }

        //testing shit purpose

    }
}
