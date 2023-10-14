using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MapEditorReborn.Events.EventArgs;
using scp_294.API.Features;
using scp_294.Events.EventArgs.Machines;

namespace scp_294.Events.Handlers.Internal
{
    internal class EventsHandler
    {
        /// <summary>
        /// Resets machine list when round ends.
        /// </summary>
        /// <param name="ev">The <see cref="RoundEndedEventArgs"/> instance.</param>
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Machine.StopMachines();
        }

        /// <summary>
        /// Initializes each <see cref="Machine"/> instance.
        /// </summary>
        /// <param name="ev">The <see cref="SchematicSpawnedEventArgs"> instance.</param>
        public void SchematicSpawned(SchematicSpawnedEventArgs ev)
        {
            if (ev == null) return;

            if (ev.Schematic.Name == Scp294.Instance.Config.SchematicName)
            {
                new Machine(ev.Schematic.CurrentRoom, ev.Schematic.Position);
            }
        }
    }
}
