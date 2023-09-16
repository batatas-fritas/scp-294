using Exiled.API.Enums;
using Exiled.API.Features;
using System.ComponentModel;
using UnityEngine;

namespace scp_294.Classes
{
    public class Effect
    {
        public EffectType Type { get; set; }

        public float Duration { get; set; }

        public Intensity Intensity { get; set; }

        [Description("The chance of this effect to be applied, in %")]
        public int Chance { get; set; }

        public void ApplyEffect(Player player, bool addToIntensity = false)
        {
            if (Roll(Chance))
            {
                Log.Debug($"Applying effect to player. Duration: {Duration}. Intensity fixed amount: {Intensity.FixedAmount}. Intensity range: {Intensity.LowestAmount} to {Intensity.HighestAmount}");

                byte currInt = (byte)(addToIntensity ? player.GetEffect(EffectType.MovementBoost).Intensity : 0);

                player.ChangeEffectIntensity(Type, (byte)Mathf.Clamp(currInt + Intensity.GetIntensity(), 0, 255), Duration);
            }
        }

        private bool Roll(int chance) => new System.Random().Next(0, 100) < chance;
    }
}
