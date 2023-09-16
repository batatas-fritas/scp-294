using InventorySystem.Items.Usables.Scp330;
using System.ComponentModel;

namespace scp_294.Classes
{
    public class Regeneration
    {
        [Description("Rate of the regeneration.")]
        public float Rate { get; set; } = 0f;

        [Description("Duration of the regeneration.")]
        public int Duration { get; set; } = 0;

        public void ApplyRegeneration(ReferenceHub playerRef) => Scp330Bag.AddSimpleRegeneration(playerRef, Rate, Duration);
    }
}
