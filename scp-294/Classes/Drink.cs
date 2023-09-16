using System.Collections.Generic;
using System.ComponentModel;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Player = Exiled.Events.Handlers.Player;

namespace scp_294.Classes
{
    public class Drink : CustomItem
    {
        public override uint Id { get; set; } = 0;
        public override SpawnProperties SpawnProperties { get; set; } = new();
        public override string Name { get; set; } = "";

        public List<string> Aliases { get; set; } = new();

        [Description("Description of the drink, this is what appears when holding the drink")]
        public override string Description { get; set; } = "";

        [Description("Whether or not the drink is enabled on your server. If this is set to false, drinks won't even register so you won't be able to have it through RA")]
        public bool IsEnabled { get; set; } = true;

        public override ItemType Type { get; set; } = ItemType.AntiSCP207;

        public override float Weight { get; set; } = 1f;

        public bool RemoveAntiColaEffect { get; set; } = true;

        [Description("List of effects that will be applied to the player")]
        public List<Effect> Effects { get; set; } = new();

        public Teleport TeleportManager { get; set; } = new();

        public AppearanceManager AppearanceOptions { get; set; } = new();

        public SpecialEffects ExtraEffects { get; set; } = new();

        public override void Init()
        {
            SubscribeEvents();
        }

        public override void Destroy()
        {
            UnsubscribeEvents();
        }

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

            if (RemoveAntiColaEffect) RemoveAntiScp207(ev.Player);

            if (TeleportManager.PlayerTeleport) TeleportManager.TryTeleport(ev.Player);

            if (AppearanceOptions.ChangePlayerAppearance) AppearanceOptions.ChangeAppearance(ev.Player);

            ApplyEffects(ev.Player);
        }

        private void ApplyEffects(Exiled.API.Features.Player player)
        {
            ExtraEffects.ApplySpecialEffects(player);
            foreach (Effect effect in Effects)
            {
                effect.ApplyEffect(player, effect.Type == EffectType.MovementBoost || effect.Type == EffectType.BodyshotReduction);
            }
        }

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
