using Exiled.API.Features;
using System;
using Schematic = MapEditorReborn.Events.Handlers.Schematic;
using Server = Exiled.Events.Handlers.Server;
using scp_294.Configs;
using scp_294.Events.Handlers;
using scp_294.API.Features;
using scp_294.Events.Handlers.Internal;

namespace scp_294
{
    public class Scp294 : Plugin<Config>
    {
        /// <summary>
        /// Gets the name of the Plugin.
        /// </summary>
        public override string Name => "scp-294";

        /// <summary>
        /// Gets the name of the Author of the Plugin.
        /// </summary>
        public override string Author => "batatas-fritas";

        /// <summary>
        /// Gets the version of the Plugin.
        /// </summary>
        public override Version Version => new Version(2, 0, 1);

        /// <summary>
        /// Gets the required Exiled Version of the Plugin.
        /// </summary>
        public override Version RequiredExiledVersion => new Version(8, 0, 0);

        /// <summary>
        /// Gets or sets the instance of the Plugin.
        /// </summary>
        public static Scp294 Instance { get; private set; }

        /// <summary>
        /// Gets or sets the event handler of the plugin.
        /// </summary>
        private EventsHandler Handler { get; set; }

        /// <summary>
        /// Subscribes to all events necessary.
        /// </summary>
        private void SubscribeEvents()
        {
            Handler = new EventsHandler();
            Machines.PlayerEnteredRange += Handler.PlayerEnteredRange;
            Machines.DispensedDrink += Handler.OnMachineDispensedDrink;
            Schematic.SchematicSpawned += Handler.SchematicSpawned;
            Server.RoundEnded += Handler.OnRoundEnded;
            
        }

        /// <summary>
        /// Unsubscribes all subscribed events.
        /// </summary>
        private void UnsubscribeEvents()
        {
            Schematic.SchematicSpawned -= Handler.SchematicSpawned;
            Machines.PlayerEnteredRange -= Handler.PlayerEnteredRange;
            Machines.DispensedDrink -= Handler.OnMachineDispensedDrink;
            Server.RoundEnded -= Handler.OnRoundEnded;
            Handler = null;
        }

        /// <summary>
        /// Entry point of the Plugin.
        /// </summary>
        public override void OnEnabled()
        {
            Instance = this;
            Config.LoadConfigs();
            Machine.RegisterDrinks(Config.DrinksConfig.Drinks);
            Machine.SyncSchematicsWithMachines();
            SubscribeEvents();
            base.OnEnabled();
        }

        /// <summary>
        /// Exit point of the Plugin
        /// </summary>
        public override void OnDisabled()
        {
            UnsubscribeEvents();
            Machine.UnregisterDrinks();
            Machine.DestroyMachines();
            Instance = null!;
            base.OnDisabled();
        }
    }
}
