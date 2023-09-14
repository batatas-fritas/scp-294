using Exiled.Events.EventArgs.Server;
using MapEditorReborn.Events.EventArgs;
using scp_294.Scp;

namespace scp_294
{
    public class EventHandler
    {
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Scp294.End();
        }

        public void SchematicSpawned(SchematicSpawnedEventArgs ev)
        {
            if(ev == null) return;

            if(ev.Schematic.Name == Plugin.Instance.Config.SchematicName) 
            {
                Scp294.Create(ev.Schematic.CurrentRoom, ev.Schematic.Position);
                Scp294.Start();
            }
        }
    }
}
