using Exiled.API.Features;
using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace scp_294.Scp
{
    public class Scp294
    {
        private static Scp294 _instance = null;

        public static Room Room { get; private set; }

        public static Vector3 Position { get; private set; }

        public static int Range { get; private set; }

        private static CoroutineHandle _handler { get; set; }

        private Scp294() { }

        private static void Update(Room room, Vector3 position, int range)
        {
            Room = room;
            Position = position;
            Range = range;
        }

        public static Scp294 Create(Room room, Vector3 position, int range)
        {
            if(_instance == null)
            {
                _instance = new Scp294();
            }
            Update(room, position, range);
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
                    Log.Debug("Checking player");

                    if (player.IsDead || player.IsScp || player == null) continue;

                    if (InRange(player.Position) && player.CurrentRoom == Room)
                    {
                        player.ShowHint("You have approached SCP-294. Use .scp294 to get a drink");
                    }
                }
            }
        }

        public static bool InRange(Vector3 position)
        {
            if (_instance == null) return false;
            float res = Vector3.Distance(position, Position);
            Log.Debug(res);
            return res < Range;
        }
    }
}
