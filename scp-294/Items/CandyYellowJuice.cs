using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Enums;
using Player = Exiled.Events.Handlers.Player;
using CustomPlayerEffects;
using UnityEngine;

namespace scp_294.Items
{
    [CustomItem(ItemType.AntiSCP207)]
    public class CandyYellowJuice : CustomItem
    {
        public override uint Id { get; set; } = 102;
        public override string Name { get; set; } = "drink of yellow candy";
        public override string Description { get; set; } = "The overwhelming smell of lemon makes you cringe a little.";
        public override float Weight { get; set; } = 1f;
        public override ItemType Type { get; set; } = ItemType.AntiSCP207;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
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

        public float Times { get; set; } = 2.5f;

        private void UsedItem(UsedItemEventArgs ev)
        {
            if (Check(ev.Item))
            {
                ev.Player.DisableEffect(EffectType.AntiScp207);

                ev.Player.StaminaStat.ModifyAmount(0.25f * Times);
                ev.Player.EnableEffect(EffectType.Invigorated, 8f * Times);
                StatusEffectBase mb = ev.Player.GetEffect(EffectType.MovementBoost);
                mb.Intensity = (byte)Mathf.Clamp(mb.Intensity + 10, 0, 255);
                mb.ServerChangeDuration(8f * Times, addDuration: true);
            }
        }
    }
}

