using InventorySystem.Items.Usables.Scp330;
using System.ComponentModel;

namespace scp_294.Items.DrinkFeatures
{
    public class Regeneration
    {
        /// <summary>
        /// Gets or sets the rate of regeneration.
        /// </summary>
        [Description("Rate of the regeneration.")]
        public float Rate { get; set; } = 0f;

        /// <summary>
        /// Gets or sets the duration of the regeneration.
        /// </summary>
        [Description("Duration of the regeneration.")]
        public int Duration { get; set; } = 0;

        /// <summary>
        /// Applies the regeneration.
        /// </summary>
        /// <param name="playerRef">The player's <see cref="ReferenceHub"/> instance.</param>
        public void ApplyRegeneration(ReferenceHub playerRef) => Scp330Bag.AddSimpleRegeneration(playerRef, Rate, Duration);
    }
}
