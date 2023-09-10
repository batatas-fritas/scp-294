using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using System;
using Server = Exiled.Events.Handlers.Server;
using Schematic = MapEditorReborn.Events.Handlers.Schematic;

namespace scp_294
{
    public class Plugin : Plugin<Config>
    {
        private EventHandler _handler;

        public override string Name => "scp-294";

        public override string Author => "batatas-fritas";

        public override Version Version => new Version(1, 0, 1);

        public override Version RequiredExiledVersion => new Version(8, 0, 0);

        private void RegisterEvents()
        {
            _handler = new EventHandler(Config);
            Server.RoundEnded += _handler.OnRoundEnded;
            Schematic.SchematicSpawned += _handler.SchematicSpawned;
        }

        private void RegisterItems()
        {
            foreach(CustomItem drink in Config.Drinks)
            {
                drink.Register();
            }
        }

        private void DisableEvents()
        {
            _handler = null;
            Server.RoundEnded -= _handler.OnRoundEnded;
            Schematic.SchematicSpawned -= _handler.SchematicSpawned;
        }

        public override void OnEnabled()
        {
            RegisterEvents();
            RegisterItems();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            DisableEvents();
            CustomItem.UnregisterItems();
            base.OnDisabled();
        }
    }
}
