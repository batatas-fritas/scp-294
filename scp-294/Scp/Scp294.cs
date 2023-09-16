using CustomPlayerEffects;
using Exiled.API.Features;
using MEC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace scp_294.Scp
{
    public class Scp294
    {
        private static Scp294 _instance = null;

        public static Room Room { get; private set; }

        public static Vector3 Position { get; private set; }

        private static CoroutineHandle _handler { get; set; }

        private Scp294() { }

        private static List<Player> PlayersInRange { get; set; } = new();

        private static void Update(Room room, Vector3 position)
        {
            Room = room;
            Position = position;
        }

        public static Scp294 Create(Room room, Vector3 position)
        {
            if(_instance == null)
            {
                _instance = new Scp294();
            }
            Update(room, position);
            return _instance;
        }

        public static Scp294 Get()
        {
            return _instance;
        }

        public static void Start()
        {
            if (_instance == null) return;
            _handler = Timing.RunCoroutine(CheckPlayersAroundSCP());
        }

        public static void End()
        {
            if (_instance == null) return;
            Timing.KillCoroutines(_handler);
        }

        private static IEnumerator<float> CheckPlayersAroundSCP()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(0.5f);

                foreach (Player player in Player.List)
                {
                    if (player.IsDead || player.IsScp || player == null) continue;

                    if (InRange(player.Position) && player.CurrentRoom == Room && !PlayersInRange.Contains(player))
                    {
                        if(Plugin.Instance.Config.RandomMode)
                        {
                            player.ShowHint(Plugin.Instance.Config.ApproachMessageRandomMode);
                        } else
                        {
                            player.ShowHint(Plugin.Instance.Config.ApproachMessage);
                        }
                        
                        PlayersInRange.Add(player);
                    }
                }
                PlayersInRange = PlayersInRange.Where(player => InRange(player.Position)).ToList();
            }
        }

        public static bool InRange(Vector3 position)
        {
            if (_instance == null) return false;
            float res = Vector3.Distance(position, Position);
            return res < Plugin.Instance.Config.Range;
        }
    }
}
