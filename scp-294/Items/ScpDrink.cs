using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Player = Exiled.Events.Handlers.Player;

namespace scp_294.Items
{
    [CustomItem(ItemType.AntiSCP207)]
    public class ScpDrink : CustomItem
    {
        public override uint Id { get; set; } = 71;
        public override string Name { get; set; } = "scp drink";
        [Description("Text that shows once you hold the drink")]
        public override string Description { get; set; } = "Disguise yourself as a random scp.";
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
                ev.Player.DisableEffect(EffectType.AntiScp207);
                List<RoleTypeId> roles = new()
                {
                    RoleTypeId.Scp173,
                    RoleTypeId.Scp049,
                    RoleTypeId.Scp096,
                    RoleTypeId.Scp0492,
                    RoleTypeId.Scp106,
                    RoleTypeId.Scp939,
                };

                RoleTypeId disguise = roles[new Random().Next(roles.Count)];

                ev.Player.ChangeAppearance(disguise);

                Log.Debug($"{ev.Player.Nickname} disguised himself as {disguise.GetFullName()}");

                Timing.RunCoroutine(DurationTimer(10, ev.Player, disguise));
            }
        }

        public IEnumerator<float> DurationTimer(int duration, Exiled.API.Features.Player player, RoleTypeId new_role)
        {
            int time_left = duration;
            while (true)
            {
                player.ShowHint($"You are disguised as {new_role.GetFullName()}. You have <color=#ff0000>{time_left}</color> seconds left.");
                yield return Timing.WaitForSeconds(1f);

                time_left -= 1;

                if(time_left == 0)
                {
                    player.ChangeAppearance(player.Role.Type);
                    player.ShowHint("You are no longer disguised");
                    yield break;
                }
              
            }
        }
    }
}