using System;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Player = Exiled.Events.Handlers.Player;

namespace scp_294.Classes
{
    public class Drink : CustomItem
    {
        public DrinkOptions Options { get; set; } = new();
        public override uint Id { get; set; } = 30;
        public override string Name { get => Options.Name; set => Name = value; }
        public override string Description { get => Options.Description; set => Description = value; }
        public override float Weight { get => Options.Weight; set => Weight = value; }
        public override SpawnProperties SpawnProperties { get; set; } = new();

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
            if (!Check(ev.Item)) return;

            if (Options.RemoveAntiColaEffect) RemoveAntiScp207(ev.Player);

            if (Options.SpawnScp173Tantrum) ev.Player.PlaceTantrum();

            if (Options.TeleportToPocketDimension) ev.Player.EnableEffect(EffectType.PocketCorroding);

            if (Options.TeleportOptions.PlayerTeleport) ev.Player.Teleport(Options.TeleportOptions.GetTeleportLocation());

            if (Options.AppearanceOptions.ChangePlayerAppearance) Options.AppearanceOptions.ChangeAppearance(ev.Player);

            if (Options.ShouldPlayerExplode)
            {
                ev.Player.Explode();
                return;
            }

            ApplyEffects(ev.Player);
        }

        private void ApplyEffects(Exiled.API.Features.Player player)
        {
            foreach (Effect effect in Options.Effects)
            {
                Log.Debug($"Trying to apply {effect.Type}. Chance: {effect.Chance}");
                if (Roll(effect.Chance))
                {
                    Log.Debug($"Applying effect to player. Duration: {effect.Duration}. Intensity fixed amount: {effect.Intensity.FixedAmount}. Intensity range: {effect.Intensity.LowestAmount} to {effect.Intensity.HighestAmount}");
                    player.ChangeEffectIntensity(effect.Type, (byte)effect.Intensity.GetIntensity(), effect.Duration);
                }
            }
        }

        private bool Roll(int chance) => new Random().Next(0, 101) < chance;

        private void RemoveAntiScp207(Exiled.API.Features.Player player)
        {
            int intensity = player.GetEffectIntensity<Scp207>();

            if (intensity > 0)
            {
                player.ChangeEffectIntensity<Scp207>(0);
            }

            player.ChangeEffectIntensity<AntiScp207>(0);
            player.ChangeEffectIntensity<Scp207>((byte)intensity);
        }
    }
}
