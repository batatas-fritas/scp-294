using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;

namespace scp_294.Classes
{
    public class DrinkOptions
    {
        public string Name { get; set; } = "drink of scp173";

        public string[] Aliases { get; set; } = { "drink of 173" };

        [Description("Description of the drink, this is what appears when holding the drink")]
        public string Description { get; set; } = "REEEEEEEEEE";

        [Description("Whether or not the drink is enabled on your server. If this is set to false, drinks won't even register so you won't be able to have it through RA")]
        public bool IsEnabled { get; set; } = true;

        public uint Id { get; set; } = 69;

        public ItemType Type { get; set; } = ItemType.AntiSCP207;

        public float Weight { get; set; } = 1f;

        public bool RemoveAntiColaEffect { get; set; } = true;

        public bool ShouldPlayerExplode { get; set; } = false;

        public bool SpawnScp173Tantrum { get; set; } = false;

        [Description("List of effects that will be applied to the player")]
        public List<Effect> Effects { get; set; } = new()
        {
            new Effect()
            {
                Type = EffectType.MovementBoost,
                Duration = 30,
                Intensity = new Intensity()
                {
                    FixedAmount = 20
                },
                Chance = 100,
            }
        };

        public bool TeleportToPocketDimension { get; set; } = false;

        public Teleport TeleportOptions { get; set; } = new()
        {
            PlayerTeleport = false,
            Zone = ZoneType.Unspecified,
            Room = RoomType.Unknown
        };

        public AppearanceManager AppearanceOptions { get; set; } = new()
        {
            ChangePlayerAppearance = false,
        };
    }
}
