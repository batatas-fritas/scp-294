using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Linq;

namespace scp_294.Items.DrinkFeatures
{
    public class AppearanceManager
    {
        /// <summary>
        /// Gets or sets whether or not the player should change appearance.
        /// </summary>
        [Description("Whether or not the player should change appearance")]
        public bool ChangePlayerAppearance { get; set; } = false;

        /// <summary>
        /// Gets or sets the list of possible roles.
        /// </summary>
        [Description("List of roles the player can turn to. As you can imagine scp-079 is not an option.")]
        public List<RoleTypeId> PossibleRoles { get; set; } = new();

        /// <summary>
        /// Gets or sets the duration of the appearance change.
        /// </summary>
        [Description("Amount of time the player's appearance will be changed")]
        public int Duration { get; set; } = 0;

        /// <summary>
        /// Gets or sets the disguise message.
        /// </summary>
        [Description("Hint displayed once the player changes appearance and counts the time left. Make sure to add '$new_role_name' and '$time_left', these will be replaced by the actual values")]
        public string DisguiseMessage { get; set; } = "";

        /// <summary>
        /// Gets or sets the message that appears once you're no longer in disguise.
        /// </summary>
        [Description("Hint displayed once you're no longer in disguise")]
        public string NoLongerInDisguise { get; set; } = "";

        /// <summary>
        /// Changes appearance of the player and then change it back.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> instance.</param>
        public void ChangeAppearance(Player player)
        {
            RoleTypeId DisguiseRole = PossibleRoles.Where(role => role != RoleTypeId.Scp079).ToList()[new Random().Next(PossibleRoles.Count)];
            Log.Debug($"Generated Disguise was: {DisguiseRole.GetFullName()}");
            player.ChangeAppearance(DisguiseRole);

            Timing.RunCoroutine(DurationTimer(Duration, player, DisguiseRole));
        }

        /// <summary>
        /// Coroutine used to change the player appearance.
        /// </summary>
        /// <param name="duration">The duration the transformation is going to last.</param>
        /// <param name="player">The <see cref="Player"/> instance.</param>
        /// <param name="new_role">The role the player is going transforming to.</param>
        /// <returns></returns>
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
