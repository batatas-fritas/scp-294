using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using System;
using Server = Exiled.Events.Handlers.Server;
using Schematic = MapEditorReborn.Events.Handlers.Schematic;
using System.Collections.Generic;

namespace scp_294
{
    public class Plugin : Plugin<Config>
    {
        private EventHandler _handler;

        public override string Name => "scp-294";

        public override string Author => "batatas-fritas";

        public override Version Version => new Version(1, 1, 3);

        public override Version RequiredExiledVersion => new Version(8, 0, 0);

        public static Plugin Instance { get; private set; }

        public List<CustomItem> CustomItems { get; private set; } = new();

        private void RegisterEvents()
        {
            _handler = new EventHandler();
            Server.RoundEnded += _handler.OnRoundEnded;
            Schematic.SchematicSpawned += _handler.SchematicSpawned;
        }

        private void RegisterItems()
        {
            if (Config.ThickJuice.IsEnabled) CustomItems.Add(Config.ThickJuice);
            if (Config.TeleportationDrink.IsEnabled) CustomItems.Add(Config.TeleportationDrink);
            if (Config.ScpDrink.IsEnabled) CustomItems.Add(Config.ScpDrink);
            if (Config.Scp173Drink.IsEnabled) CustomItems.Add(Config.Scp173Drink);
            if (Config.Scp106Drink.IsEnabled) CustomItems.Add(Config.Scp106Drink);
            if (Config.CandyYellowJuice.IsEnabled) CustomItems.Add(Config.CandyYellowJuice);
            if (Config.CandyRedJuice.IsEnabled) CustomItems.Add(Config.CandyRedJuice);
            if (Config.CandyRainbowJuice.IsEnabled) CustomItems.Add(Config.CandyRainbowJuice);
            if (Config.CandyPurpleJuice.IsEnabled) CustomItems.Add(Config.CandyPurpleJuice);
            if (Config.CandyPinkJuice.IsEnabled) CustomItems.Add(Config.CandyPinkJuice);
            if (Config.CandyJuice.IsEnabled) CustomItems.Add(Config.CandyJuice);
            if (Config.CandyGreenJuice.IsEnabled) CustomItems.Add(Config.CandyGreenJuice);
            if (Config.CandyBlueJuice.IsEnabled) CustomItems.Add(Config.CandyBlueJuice);

            foreach (CustomItem item in CustomItems)
            {
                Log.Debug($"Registering {item.Name}");
                item.Register();
            }
        }

        private void DisableEvents()
        {
            Server.RoundEnded -= _handler.OnRoundEnded;
            Schematic.SchematicSpawned -= _handler.SchematicSpawned;
            _handler = null;
        }

        public override void OnEnabled()
        {
            Instance = this;
            RegisterEvents();
            RegisterItems();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null!;
            DisableEvents();
            CustomItem.UnregisterItems();
            base.OnDisabled();
        }
    }
}
