using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using scp_294.Scp;
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

        public bool IsEnabled { get; set; } = true;
        public override float Weight { get; set; } = 1f;

        [Description("Text that appears once you're in disguise. If you change this, make sure to add '$new_role_name' and '$time_left', these will be replaced by the actual values")]
        public string CurrentDisguise { get; set; } = "You are disguised as $new_role_name. You have <color=#ff0000>$time_left</color> seconds left.";

        [Description("Text that appears once you're no longer in disguise")]
        public string NoLongerInDisguise { get; set; } = "You are no longer disguised";

        public override ItemType Type { get; set; } = ItemType.AntiSCP207;

        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1, // Irrelevant: determines the maximum of how many will spawn (they will not spawn in the map)
        };

        public int DisguiseDuration { get; set; } = 10;

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

                Timing.RunCoroutine(DurationTimer(DisguiseDuration, ev.Player, disguise));
            }
        }

        public IEnumerator<float> DurationTimer(int duration, Exiled.API.Features.Player player, RoleTypeId new_role)
        {
            int time_left = duration;
            while (true)
            {
                player.ShowHint(CurrentDisguise.Replace("$new_role_name", new_role.GetFullName()).Replace("$time_left", time_left.ToString()));
                yield return Timing.WaitForSeconds(1f);

                time_left -= 1;

                if(time_left == 0)
                {
                    player.ChangeAppearance(player.Role.Type);
                    player.ShowHint(NoLongerInDisguise);
                    yield break;
                }
              
            }
        }
    }
}