using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.ComponentModel;

namespace scp_294.Classes
{
    public class SpecialEffects
    {
        [Description("Whether or not the player explodes after drinking.")]
        public bool PlayerExplode { get; set; } = false;

        [Description("Whether or not it will generate an explosion once you ask for the drink")]
        public bool ExplodeOnDispensing { get; set; } = false;

        [Description("Whether or not the player gains Ahp. Set this to 0 if no Ahp.")]
        public int AhpGain { get; set; } = 0;

        [Description("Whether or not the player gains/loses stamina. Value between -1 and 1. 0 for no change.")]
        public float StaminaChange { get; set; } = 0f;

        [Description("Whether or not tantrum is placed beneath the player.")]
        public bool PlaceTantrum { get; set; } = false;

        [Description("Whether or not the player receives HP.")]
        public int HealAmount { get; set; } = 0;

        [Description("How much damage the player will take on consuming the drink")]
        public int DamageAmount { get; set; } = 0;

        [Description("Whether or not the player receives passive regeneration.")]
        public Regeneration Regeneration { get; set; } = new();

        [Description("Whether or not the player gets teleported to pocket dimension.")]
        public bool TeleportToPocketDimension { get; set; } = false;

        public void ApplySpecialEffects(Player player)
        {
            if (PlayerExplode) { player.Explode(); return; }
            if (AhpGain > 0) player.AddAhp(AhpGain, 100, 0f);
            if (StaminaChange != 0) player.StaminaStat.ModifyAmount(StaminaChange);
            if (PlaceTantrum) player.PlaceTantrum();
            if (HealAmount > 0) player.Heal(HealAmount);
            if (DamageAmount > 0) player.Hurt(DamageAmount);
            if (Regeneration.Rate > 0) Regeneration.ApplyRegeneration(player.ReferenceHub);
            if (TeleportToPocketDimension) player.EnableEffect(EffectType.PocketCorroding);
        }
    }
}