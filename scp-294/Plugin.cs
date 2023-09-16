using Exiled.API.Features;
using System;
using Server = Exiled.Events.Handlers.Server;
using Schematic = MapEditorReborn.Events.Handlers.Schematic;
using scp_294.Handlers;
using scp_294.Configs;
using System.Collections.Generic;
using scp_294.Classes;

namespace scp_294
{
    public class Plugin : Plugin<Config>
    {
        private EventsHandler _handler;

        public override string Name => "scp-294";

        public override string Author => "batatas-fritas";

        public override Version Version => new Version(2, 0, 0);

        public override Version RequiredExiledVersion => new Version(8, 0, 0);

        public static Plugin Instance { get; private set; }

        public Dictionary<uint, Drink> LookupIdTable = new();

        public List<Drink> Drinks = new();

        private void RegisterEvents()
        {
            _handler = new EventsHandler();
            Server.RoundEnded += _handler.OnRoundEnded;
            Schematic.SchematicSpawned += _handler.SchematicSpawned;
        }

        private void RegisterItems()
        {
            foreach(Drink drink in Config.DrinksConfig.Drinks)
            {
                if (!drink.IsEnabled) continue;

                Log.Debug($"{drink.Name} enabled");

                LookupIdTable.Add(drink.Id, drink);
                Drinks.Add(drink);
                drink.Init();
            }
        }

        private void UnregisterItems()
        {
            foreach (Drink drink in Drinks) 
            {
                if (drink.IsEnabled) drink.Destroy(); 
            }
            LookupIdTable = null;
            Drinks = null;
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
            Config.LoadConfigs();

            RegisterEvents();
            RegisterItems();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null!;
            DisableEvents();
            UnregisterItems();

            _handler = null!;
            Instance = null!;

            base.OnDisabled();
        }
    }
}
