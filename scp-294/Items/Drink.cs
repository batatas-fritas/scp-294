using System.Collections.Generic;
using System.ComponentModel;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Player = Exiled.Events.Handlers.Player;
using scp_294.Items.DrinkFeatures;
using scp_294.Events.Handlers;
using scp_294.Events.EventArgs.Drinks;

namespace scp_294.Items
{
    public class Drink : CustomItem
    {

        /// <inheritdoc/>
        public override string Name { get; set; } = "";

        /// <summary>
        /// Gets or sets the drink aliases.
        /// </summary>
        public List<string> Aliases { get; set; } = new();

        /// <inheritdoc/>
        public override uint Id { get; set; } = 0;

        /// <inheritdoc/>
        [Description("Description of the drink, this is what appears when holding the drink")]
        public override string Description { get; set; } = "";

        /// <summary>
        /// Gets or sets whether or not the drink is enabled.
        /// </summary>
        [Description("Whether or not the drink is enabled on your server. If this is set to false, drinks won't even register so you won't be able to have it through RA")]
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc/>
        public override ItemType Type { get; set; } = ItemType.AntiSCP207;

        /// <inheritdoc/>
        public override float Weight { get; set; } = 1f;

        /// <summary>
        /// Gets or sets whether or not the anti cola effect should be removed.
        /// </summary>
        public bool RemoveAntiColaEffect { get; set; } = true;

        /// <summary>
        /// Gets or sets the list of <see cref="Effect"/>.
        /// </summary>
        [Description("List of effects that will be applied to the player")]
        public List<Effect> Effects { get; set; } = new();

        /// <summary>
        /// Gets or sets the <see cref="Teleport"/> instance.
        /// </summary>
        public Teleport TeleportManager { get; set; } = new();

        /// <summary>
        /// Gets or sets the <see cref="AppearanceManager"/> instance.
        /// </summary>
        public AppearanceManager AppearanceOptions { get; set; } = new();

        /// <summary>
        /// Gets or sets the <see cref="SpecialEffects"/> instance.
        /// </summary>
        public SpecialEffects ExtraEffects { get; set; } = new();

        /// <summary>
        /// Gets or sets the <see cref="Scaling"/> instance.
        /// </summary>
        public Scaling ScalingOptions { get; set; } = new();

        /// <summary>
        /// Gets or sets the <see cref="RoleManager"/> instance.
        /// </summary>
        public RoleManager RoleManagerOptions { get; set; } = new();

        /// <inheritdoc/>
        public override SpawnProperties SpawnProperties { get; set; } = new();

        /// <summary>
        /// Initializes the item by subscribe to all events needed.
        /// </summary>
        public override void Init()
        {
            SubscribeEvents();
        }

        /// <summary>
        /// Destroys the item by unsubscribing to all events subscribed.
        /// </summary>
        public override void Destroy()
        {
            UnsubscribeEvents();
        }

        /// <summary>
        /// Subscribe to all special events.
        /// </summary>
        protected override void SubscribeEvents()
        {
            Player.UsedItem += UsedItem;
            base.SubscribeEvents();
        }

        /// <summary>
        /// Unsubscribe to all special events.
        /// </summary>
        protected override void UnsubscribeEvents()
        {
            Player.UsedItem -= UsedItem;
            base.UnsubscribeEvents();
        }

        /// <summary>
        /// Applies every effect/teleport/appearance specified by the drink properties.
        /// </summary>
        /// <param name="ev">The <see cref="UsedItemEventArgs"/> instance.</param>
        private void UsedItem(UsedItemEventArgs ev)
        {
            if (!Check(ev.Item)) return;

            if (RemoveAntiColaEffect) RemoveAntiScp207(ev.Player);

            if (TeleportManager.PlayerTeleport) TeleportManager.TryTeleport(ev.Player);

            if (AppearanceOptions.ChangePlayerAppearance) AppearanceOptions.ChangeAppearance(ev.Player);

            if (RoleManagerOptions.PlayerChangeRoles) RoleManagerOptions.ChangeRole(ev.Player);

            ScalingOptions.ScalePlayer(ev.Player);

            ApplyEffects(ev.Player);

            ConsumedDrinkEventArgs consumedDrinkEventArgs = new ConsumedDrinkEventArgs(ev.Player, this);
            Drinks.OnConsumedDrink(consumedDrinkEventArgs);
        }

        /// <summary>
        /// Applies every effect in the effect list.
        /// </summary>
        /// <param name="player">The <see cref="Exiled.API.Features.Player"/> instance.</param>
        private void ApplyEffects(Exiled.API.Features.Player player)
        {
            ExtraEffects.ApplySpecialEffects(player);
            foreach (Effect effect in Effects)
            {
                effect.ApplyEffect(player, effect.Type == EffectType.MovementBoost || effect.Type == EffectType.BodyshotReduction);
            }
        }

        /// <summary>
        /// Removes anti cola effect from the player.
        /// </summary>
        /// <param name="player">The <see cref="Exiled.API.Features.Player"/> instance.</param>
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
