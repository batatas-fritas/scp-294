using Exiled.Events.EventArgs.Server;
using MapEditorReborn.Events.EventArgs;
using scp_294.Scp;

namespace scp_294
{
    public class EventHandler
    {
        private static Config Config { get; set; }

        public EventHandler(Config config) { Config = config; }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Scp294.End();
        }

        public void SchematicSpawned(SchematicSpawnedEventArgs ev)
        {
            if(ev == null) return;

            if(ev.Schematic.Name == "scp294") 
            {
                Scp294.Create(ev.Schematic.CurrentRoom, ev.Schematic.Position, Config.Range);
                Scp294.Start();
            }
        }
    }
}
