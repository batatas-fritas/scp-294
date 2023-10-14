using Exiled.API.Enums;
using Exiled.API.Features;
using System.ComponentModel;
using UnityEngine;

namespace scp_294.Items.DrinkFeatures
{
    public class Effect
    {
        /// <summary>
        /// Gets or sets the effect type.
        /// </summary>
        public EffectType Type { get; set; }

        /// <summary>
        /// Gets or sets the duration of the effect.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// Gets or sets the intensity of the effect.
        /// </summary>
        public Intensity Intensity { get; set; }

        /// <summary>
        /// Gets or sets the change of this effect to be applied.
        /// </summary>
        [Description("The chance of this effect to be applied, in %")]
        public int Chance { get; set; }

        /// <summary>
        /// Applies this effect to the player.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> instance.</param>
        /// <param name="addToIntensity">Whether or not the intensity should be added to the existing one.</param>
        public void ApplyEffect(Player player, bool addToIntensity = false)
        {
            if (Roll(Chance))
            {
                Log.Debug($"Applying effect to player. Duration: {Duration}. Intensity fixed amount: {Intensity.FixedAmount}. Intensity range: {Intensity.LowestAmount} to {Intensity.HighestAmount}");

                byte currInt = (byte)(addToIntensity ? player.GetEffect(EffectType.MovementBoost).Intensity : 0);

                player.ChangeEffectIntensity(Type, (byte)Mathf.Clamp(currInt + Intensity.GetIntensity(), 0, 255), Duration);
            }
        }

        /// <summary>
        /// Rolls chance.
        /// </summary>
        /// <param name="chance">Chance being rolled.</param>
        private bool Roll(int chance) => new System.Random().Next(0, 100) < chance;
    }
}
