using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using scp_294.Scp;
using System.ComponentModel;
using Player = Exiled.Events.Handlers.Player;

namespace scp_294.Items
{
    [CustomItem(ItemType.AntiSCP207)]
    public class Scp173Drink : CustomItem
    {
        public override uint Id { get; set; } = 72;
        public override string Name { get; set; } = "drink of scp173";
        [Description("Text that shows once you hold the drink")]
        public override string Description { get; set; } = "REEEEEEEEEEEEEEEEE.";
        public bool IsEnable { get; set; } = true;
        public override float Weight { get; set; } = 1f;
        public override ItemType Type { get; set; } = ItemType.AntiSCP207;
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1, // Irrelevant: determines the maximum of how many will spawn (they will not spawn in the map)
        };

        [Description("Intensity of movement speed")]
        public int MovementSpeedIntensity { get; set; } = 50;

        [Description("Duration of the movement speed")]
        public int MovementSpeedDuration { get; set; } = 30;

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
                ev.Player.ChangeEffectIntensity(EffectType.MovementBoost,(byte) MovementSpeedIntensity, MovementSpeedDuration);
            }
        }
    }
}