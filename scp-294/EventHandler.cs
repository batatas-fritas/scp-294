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

        // private CoroutineHandle _handler;

        private List<Player> PlayersInRange = new List<Player>();

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            // kill coroutine
            // Log.Debug("killing coroutine");
            // Timing.KillCoroutines(_handler); not working rn
        }

        public void SchematicSpawned(SchematicSpawnedEventArgs ev)
        {
            if (ev == null) return;

            if(ev.Schematic.Name == "scp294") 
            {
                Scp294_room = ev.Schematic.CurrentRoom;
                Log.Debug(Scp294_room.name);
                Scp294_position = ev.Schematic.Position;
                Log.Debug($"{Scp294_position.x}, {Scp294_position.y}, {Scp294_position.z}");

                // start 294 coroutine (not working rn)
                // _handler = Timing.RunCoroutine(CheckPlayersAroundSCP());
            }
        }

        public static bool InRange(Vector3 position)
        {
            double res = Math.Sqrt(Math.Pow(position.x - Scp294_position.x, 2) + Math.Pow(position.y - Scp294_position.y, 2) + Math.Pow(position.z - Scp294_position.z, 2));
            Log.Debug(res);
            return res < Config.Range;
        }

        public IEnumerator<float> CheckPlayersAroundSCP()
        {
            while(true)
            {
                yield return Timing.WaitForSeconds(0.1f);

                foreach(Player player in Player.List)
                {

                    if (player.IsDead || player.IsScp || player == null || player.CurrentRoom.name != "EZ_PCs") continue;

                    Log.Debug($"Player encontrado nome: {player.Nickname}. Posicao: {player.Position.x},{player.Position.y},{player.Position.z}");

                    if(InRange(player.Position) && !PlayersInRange.Contains(player))
                    {
                        player.ShowHint("You have approached SCP-294. Use .scp294 to get a drink");
                        Log.Debug($"Added player {player.Nickname} to InRangeList");
                        PlayersInRange.Add(player);
                    }
                }

                foreach(Player player in PlayersInRange)
                {
                    if (!InRange(player.Position))
                    {
                        Log.Debug($"Removed player {player.Nickname} from InRangeList");
                        PlayersInRange.Remove(player);
                    }
                }
            }
        }
    }
}
