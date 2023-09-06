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

        public override Version Version => new Version(1, 0, 0);

        public override Version RequiredExiledVersion => new Version(8, 0, 0); // 7.2 may work

        private void RegisterEvents()
        {
            _handler = new EventHandler(Config);
            Server.RoundEnded += _handler.OnRoundEnded;
            Schematic.SchematicSpawned += _handler.SchematicSpawned;
            // register events
        }

        private void RegisterItems()
        {
            Config.ThickJuice.Register();
            Config.CandyJuice.Register();
            Config.CandyRainbowJuice.Register();
            Config.CandyYellowJuice.Register();
            Config.CandyPurpleJuice.Register();
            Config.CandyRedJuice.Register();
            Config.CandyGreenJuice.Register();
            Config.CandyBlueJuice.Register();
            Config.CandyPinkJuice.Register();
            Config.TeleportationDrink.Register();
            Config.ScpDrink.Register();
            Config.Scp173Drink.Register();
        }

        private void DisableEvents()
        {
            _handler = null;
            Server.RoundEnded -= _handler.OnRoundEnded;
            Schematic.SchematicSpawned -= _handler.SchematicSpawned;
            // unregister events
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
