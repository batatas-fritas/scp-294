using System;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using InventorySystem.Items.Usables.Scp330;
using System.Collections.Generic;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Enums;
using Player = Exiled.Events.Handlers.Player;

namespace scp_294.Items
{
    [CustomItem(ItemType.AntiSCP207)]
    public class CandyJuice : CustomItem
    {
        public override uint Id { get; set; } = 100;
        public override string Name { get; set; } = "drink of candy";
        public override string Description { get; set; } = "The smell overpowers your senses. What does it taste like?";
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

        public int Times { get; set; } = 5;

        private void UsedItem(UsedItemEventArgs ev)
        {
            if (Check(ev.Item))
            {
                ev.Player.DisableEffect(EffectType.AntiScp207);

                List<ICandy> candies = new List<ICandy>()
                {
                    new CandyRainbow(),
                    new CandyYellow(),
                    new CandyPurple(),
                    new CandyRed(),
                    new CandyGreen(),
                    new CandyBlue(),
                    new CandyPink()
                };

                for (int i = 0, cont = 80; i < Times; i++, cont /= 2) 
                {
                    int idx = RollCandy(candies.Count);
                    ICandy candy = candies[idx];
                    Log.Debug($"Drink includes {candy.Kind} candy");
                    candy.ServerApplyEffects(ev.Player.ReferenceHub);
                    candies.RemoveAt(idx);
                    Log.Debug($"Current List size = {candies.Count}");
                    Log.Debug($"Chance of continuing = {cont}%");
                   
                    if (candy.Kind.Equals(CandyKindID.Pink) || !RollContinue(cont)) 
                    {
                        break;
                    }
                }
            }
        }

        public bool RollContinue(int chance) => new Random().Next(100) + 1 < chance;

        public int RollCandy(int poss) => new Random().Next(0, poss);
    }
}
