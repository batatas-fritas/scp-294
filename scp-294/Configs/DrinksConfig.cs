using Exiled.API.Enums;
using PlayerRoles;
using scp_294.Classes;
using System.Collections.Generic;

namespace scp_294.Configs
{
    public class DrinksConfig
    {
        public List<Drink> Drinks { get; set; } = new()
        {
            new Drink()
            {
                Id = 1,
                Name = "drink of air",
                Aliases = { "nothing", "drink of cup", "drink of emptiness", "drink of vacuum", "HL3", "Half Life 3" },
                Description = "There is nothing to drink in the bottle."
            },
            new Drink()
            {
                Id = 2,
                Name = "scp drink",
                Description = "Disguise yourself as a random scp.",
                AppearanceOptions =
                {
                    ChangePlayerAppearance = true,
                    PossibleRoles = new()
                    {
                        RoleTypeId.Scp173,
                        RoleTypeId.Scp049,
                        RoleTypeId.Scp096,
                        RoleTypeId.Scp0492,
                        RoleTypeId.Scp106,
                        RoleTypeId.Scp939,
                    },
                    Duration = 10,
                    DisguiseMessage = "You are disguised as $new_role_name. You have <color=#ff0000>$time_left</color> seconds left.",
                    NoLongerInDisguise = "You are no longer disguised"
                }
            },
            new Drink()
            {
                Id = 3,
                Name = "drink of scp173",
                Aliases = { "drink of 173" },
                Description = "REEEEEEEEEEEEEEEEE.",
                Effects =
                {
                    new Effect()
                    {
                        Type = EffectType.MovementBoost,
                        Duration = 30f,
                        Intensity = new() { FixedAmount = 50 },
                        Chance = 100
                    }
                }
            },
            new Drink()
            {
                Id = 4,
                Name = "drink of scp106",
                Aliases = { "drink of 106" },
                Description = "Old man sludge. Yum!",
                ExtraEffects = { TeleportToPocketDimension = true }
            },
            new Drink()
            {
                Id = 5,
                Name = "drink of cum",
                Aliases = { "cum" },
                Description = "It is cum. Drink up buddy.",
                ExtraEffects = { PlaceTantrum = true }
            },
            new Drink()
            {
                Id = 6,
                Name = "drink of chorus fruit",
                Aliases = { "teleport drink" },
                Description = "Ever played minecraft? Then you know what's about to go down.",
                TeleportManager = 
                {
                    PlayerTeleport = true,
                    CanPlayerEscapePocketDimension = false,
                    MessagePreventingPocketTeleport = "You are prevented by a magical force from being teleported out of the pocket dimension",
                    Zone = ZoneType.Unspecified,
                    Room = RoomType.Unknown
                }
            },
            new Drink()
            {
                Id = 7,
                Name = "drink of candy",
                Aliases = { "candy" },
                Description = "The smell overpowers your senses. What does it taste like?",
                Effects =
                {
                    new Effect()
                    {
                        Type = EffectType.Vitality,
                        Intensity = new() { FixedAmount = 3 },
                        Duration = 40f,
                        Chance = 100
                    },
                    new Effect()
                    {
                        Type = EffectType.DamageReduction,
                        Intensity = new() { FixedAmount = 40 },
                        Duration = 30f,
                        Chance = 50
                    },
                    new Effect()
                    {
                        Type = EffectType.RainbowTaste,
                        Intensity = new() { FixedAmount = 1 },
                        Duration = 20f,
                        Chance = 50
                    },
                    new Effect()
                    {
                        Type = EffectType.Invigorated,
                        Intensity = new() { FixedAmount = 1 },
                        Duration = 10f,
                        Chance = 50
                    },
                    new Effect()
                    {
                        Type = EffectType.BodyshotReduction,
                        Intensity = new() { FixedAmount = 1 },
                        Duration = 0,
                        Chance = 50
                    },
                    new Effect()
                    {
                        Type = EffectType.MovementBoost,
                        Intensity = new() { FixedAmount = 10 },
                        Duration = 20f,
                        Chance = 50
                    }
                },
                ExtraEffects = { AhpGain = 20, HealAmount = 90 }
            },
            new Drink()
            {
                Id = 8,
                Name = "drink of rainbow candy",
                Description = "It is packed with all sorts of flavours.",
                Effects =
                {
                    new Effect()
                    {
                        Type = EffectType.Invigorated,
                        Intensity = new() { FixedAmount = 1 },
                        Duration = 10f,
                        Chance = 100
                    },
                    new Effect()
                    {
                        Type = EffectType.RainbowTaste,
                        Intensity = new() { FixedAmount = 1 },
                        Duration = 20f,
                        Chance = 100
                    },
                    new Effect()
                    {
                        Type = EffectType.BodyshotReduction,
                        Intensity = new() { FixedAmount = 1 },
                        Duration = 0,
                        Chance = 100
                    }
                },
                ExtraEffects = { AhpGain = 40, HealAmount = 30 }
            },
            new Drink()
            {
                Id = 9,
                Name = "drink of yellow candy",
                Description = "The overwhelming smell of lemon makes you cringe a little.",
                Effects =
                {
                    new Effect()
                    {
                        Type = EffectType.Invigorated,
                        Intensity = new() { FixedAmount = 1 },
                        Duration = 20f,
                        Chance = 100
                    },
                    new Effect()
                    {
                        Type = EffectType.MovementBoost,
                        Intensity = new() { FixedAmount = 10 },
                        Duration = 20f,
                        Chance = 100
                    }
                }
            },
            new Drink()
            {
                Id = 10,
                Name = "drink of purple candy",
                Description = "It has a juicy grape smell. Your mouth begins to water.",
                Effects =
                {
                    new Effect()
                    {
                        Type = EffectType.DamageReduction,
                        Intensity = new() { FixedAmount = 40 },
                        Duration = 30f,
                        Chance = 100
                    }
                },
                ExtraEffects = { Regeneration = { Rate = 1.5f, Duration = 20 } }
            },
            new Drink()
            {
                Id = 11,
                Name = "drink of red candy",
                Description = "A strong scent of cherry fills the room. It’s a bit... too strong.",
                ExtraEffects = { Regeneration = { Rate = 9f, Duration = 10 } }
            },
            new Drink()
            {
                Id = 12,
                Name = "drink of green candy",
                Description = "A nasty herbal smell emits from the juice.",
                Effects =
                {
                    new Effect()
                    {
                        Type = EffectType.Vitality,
                        Intensity = new() { FixedAmount = 1 },
                        Duration = 45f,
                        Chance = 100
                    }
                },
                ExtraEffects = { Regeneration = { Rate = 2.25f, Duration = 80 } }
            },
            new Drink()
            {
                Id = 13,
                Name = "drink of blue candy",
                Description = "It smells soft and sweet, something akin to a marshmallow.",
                ExtraEffects = { AhpGain = 60 }
            },
            new Drink()
            {
                Id = 14,
                Name = "drink of pink candy",
                Description = "The strawberry scent is as gentle as it looks.",
                ExtraEffects = { PlayerExplode = true }
            },
            new Drink()
            {
                Id = 15,
                Name = "drink of blood",
                Description = "You feel tired as if you've lost some of your blood.",
                ExtraEffects = { DamageAmount = 30 }
            },
            new Drink()
            {
                Id = 16,
                Name = "drink of life",
                Description = "You feel refreshed.",
                ExtraEffects = { HealAmount = 100 }              
            },
            new Drink()
            {
                Id = 17,
                Name = "antimatter",
                Aliases = { "anti-matter", "void" },
                Description = "You feel as if you are being sucked into the drink",
                ExtraEffects = { ExplodeOnDispensing = true }
            }
        };
    }
}
