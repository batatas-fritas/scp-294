using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MapEditorReborn.Events.EventArgs;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace scp_294
{
    public class EventHandler
    {
        private static Config Config { get; set; }

        public EventHandler(Config config) { Config = config; }

        public static Room Scp294_room { get; set; }

        public static Vector3 Scp294_position { get; set; }

        private CoroutineHandle _handler;

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            // kill coroutine
            Log.Debug("killing coroutine");
            Timing.KillCoroutines(_handler); 
        }

        public void SchematicSpawned(SchematicSpawnedEventArgs ev)
        {
            if(ev == null) return;

            if(ev.Schematic.Name == "scp294") 
            {
                Scp294_room = ev.Schematic.CurrentRoom;
                Log.Debug(Scp294_room.name);
                Scp294_position = ev.Schematic.Position;
                Log.Debug($"{Scp294_position.x}, {Scp294_position.y}, {Scp294_position.z}");

                _handler = Timing.RunCoroutine(CheckPlayersAroundSCP());
            }
        }

        public static bool InRange(Vector3 position)
        {
            float res = Vector3.Distance(position, Scp294_position);
            Log.Debug(res);
            return res < Config.Range;
        }

        public IEnumerator<float> CheckPlayersAroundSCP()
        {
            while(true)
            {
                yield return Timing.WaitForSeconds(0.5f);

                foreach(Player player in Player.List)
                {
                    Log.Debug("Checking player");

                    if (player.IsDead || player.IsScp || player == null) continue;

                    if(InRange(player.Position) && player.CurrentRoom == Scp294_room)
                    {
                        player.ShowHint("You have approached SCP-294. Use .scp294 to get a drink");
                    }
                }
            }
        }
    }
}
