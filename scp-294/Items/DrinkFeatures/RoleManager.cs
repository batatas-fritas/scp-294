using Exiled.API.Features;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;

namespace scp_294.Items.DrinkFeatures
{
    public class RoleManager
    {
        /// <summary>
        /// Gets or sets whether or not the player should change roles.
        /// </summary>
        [Description("Whether or not the player should change roles.")]
        public bool PlayerChangeRoles { get; set; } = false;

        /// <summary>
        /// Gets or sets the list of roles the player will be able to turn to.
        /// </summary>
        [Description("Roles the player will be able to turn to. It will choose one randomly. If there is only one it will choose that one.")]
        public List<RoleTypeId> Roles { get; set; } = new();

        /// <summary>
        /// Sets a random role in Roles to the player.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> instance.</param>
        public void ChangeRole(Player player)
        {
            player.Role.Set(Roles.RandomItem(), RoleSpawnFlags.None);
        }
    }
}
