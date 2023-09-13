using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Linq;

namespace scp_294.Classes
{
    public class AppearanceManager
    {
        [Description("Whether or not the player should change appearance")]
        public bool ChangePlayerAppearance {  get; set; }

        [Description("List of roles the player can turn to. As you can imagine scp-079 is not an option.")]
        public List<RoleTypeId> PossibleRoles { get; set; }

        [Description("Amount of time the player's appearance will be changed")]
        public int Duration { get; set; }

        [Description("Hint displayed once the player changes appearance and counts the time left. Make sure to add '$new_role_name' and '$time_left', these will be replaced by the actual values")]
        public string DisguiseMessage { get; set; }

        [Description("Hint displayed once you're no longer in disguise")]
        public string NoLongerInDisguise { get; set; }

        public void ChangeAppearance(Player player)
        {
            RoleTypeId DisguiseRole = PossibleRoles.Where(role => role != RoleTypeId.Scp079).ToList()[new Random().Next(PossibleRoles.Count)];
            Log.Debug($"Generated Disguise was: {DisguiseRole.GetFullName()}");
            player.ChangeAppearance(DisguiseRole);

            Timing.RunCoroutine(DurationTimer(Duration, player, DisguiseRole));
        }

        private IEnumerator<float> DurationTimer(int duration, Player player, RoleTypeId new_role)
        {
            int time_left = duration;
            while (true)
            {
                player.ShowHint(DisguiseMessage.Replace("$new_role_name", new_role.GetFullName()).Replace("$time_left", time_left.ToString()));
                yield return Timing.WaitForSeconds(1f);

                time_left -= 1;

                if (time_left == 0)
                {
                    player.ChangeAppearance(player.Role.Type);
                    player.ShowHint(NoLongerInDisguise);
                    yield break;
                }

            }
        }

    }
}
