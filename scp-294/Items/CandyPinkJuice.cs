﻿using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Enums;
using Player = Exiled.Events.Handlers.Player;
using System.ComponentModel;
using scp_294.Scp;

namespace scp_294.Items
{
    [CustomItem(ItemType.AntiSCP207)]
    public class CandyPinkJuice : CustomItem
    {
        public override uint Id { get; set; } = 107;
        public override string Name { get; set; } = "drink of pink candy";
        [Description("Text that shows once you hold the drink")]
        public override string Description { get; set; } = "The strawberry scent is as gentle as it looks.";
        public bool IsEnabled { get; set; } = true;
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

                ev.Player.Explode();
            }
        }
    }
}
