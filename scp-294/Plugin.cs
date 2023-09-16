using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using System;
using Server = Exiled.Events.Handlers.Server;
using Schematic = MapEditorReborn.Events.Handlers.Schematic;
using scp_294.Handlers;
using scp_294.Classes;
using Exiled.CustomItems;

namespace scp_294
{
    public class Plugin : Plugin<Config>
    {
        private EventsHandler _handler;

        public override string Name => "scp-294";

        public override string Author => "batatas-fritas";

        public override Version Version => new Version(1, 1, 3);

        public override Version RequiredExiledVersion => new Version(8, 0, 0);

        public static Plugin Instance { get; private set; }



        private void RegisterEvents()
        {
            _handler = new EventsHandler();
            Server.RoundEnded += _handler.OnRoundEnded;
            Schematic.SchematicSpawned += _handler.SchematicSpawned;
        }

        private void RegisterItems()
        {
            // custom register
        }

        private void DisableEvents()
        {
            Server.RoundEnded -= _handler.OnRoundEnded;
            Schematic.SchematicSpawned -= _handler.SchematicSpawned;
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
            DisableEvents();
            CustomItem.UnregisterItems();
            _handler = null!;
            Instance = null!;
            base.OnDisabled();
        }
    }
}
