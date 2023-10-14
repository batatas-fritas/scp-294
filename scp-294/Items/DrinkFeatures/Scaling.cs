using Exiled.API.Features;
using System.ComponentModel;
using UnityEngine;

namespace scp_294.Items.DrinkFeatures
{
    public class Scaling
    {
        /// <summary>
        /// Gets or sets the amount the player will be scaled on x-axis.
        /// </summary>
        [Description("How much the player should be scaled on x-axis")]
        public double x { get; set; } = 1;

        /// <summary>
        /// Gets or sets the amount the player will be scaled on y-axis.
        /// </summary>
        [Description("How much the player should be scaled on y-axis")]
        public double y { get; set; } = 1;

        /// <summary>
        /// Gets or sets the amount the player will be scaled on z-axis.
        /// </summary>
        [Description("How much the player should be scaled on z-axis")]
        public double z { get; set; } = 1;

        /// <summary>
        /// Applies the scale to the player.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> instance.</param>
        public void ScalePlayer(Player player)
        {
            player.Scale = new Vector3(x, y, z);
        }


    }
}
