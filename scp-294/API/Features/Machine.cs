using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using scp_294.Classes;
using scp_294.Events.EventArgs.Machines;
using scp_294.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace scp_294.API.Features
{
    public class Machine
    {
        /// <summary>
        /// List of every <see cref="Machine"/> spawned.
        /// </summary>
        public static List<Machine> List = new();

        /// <summary>
        /// Table that keeps every <see cref="Drink"/> registered by Id.
        /// </summary>
        private static Dictionary<uint, Drink> LookupIdTable = new();

        /// <summary>
        /// Table that keeps every <see cref="Drink"/> registered by Name.
        /// </summary>
        private static Dictionary<string, Drink> LookupStringTable = new();

        /// <summary>
        /// List of every <see cref="Drink"/> registered.
        /// </summary>
        private static List<Drink> Drinks = new();

        /// <summary>
        /// List of every <see cref="Player"/> in range of the machine.
        /// </summary>
        private List<Player> PlayersInRange = new();


        /// <summary>
        /// Initializes a new instance of the <see cref="Machine"/> class and adds it to the list of machines.
        /// </summary>
        /// <param name="room">Room the machine has spawned in.</param>
        /// <param name="position">Position of the machine relative to the world.</param>
        public Machine(Room room, Vector3 position)
        {
            Room = room;
            Position = position;
            PlayersInRangeCoroutine = Timing.RunCoroutine(CheckPlayersAroundMachine());
            List.Add(this);
        }

        /// <summary>
        /// Gets or sets the <see cref="Room"/> the machine is in.
        /// </summary>
        private Room Room { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Position"/> the machine is in.
        /// </summary>
        private Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the PlayersInRangeCoroutine.
        /// </summary>
        private CoroutineHandle PlayersInRangeCoroutine { get; set; }

        /// <summary>
        /// Registers a new drink into the lookup tables
        /// </summary>
        /// <param name="drink">Drink you want to register.</param>
        public static void RegisterDrink(Drink drink)
        {
            if (LookupIdTable.ContainsKey(drink.Id) || LookupStringTable.ContainsKey(drink.Name))
            {
                Debug.Log($"Tried registering item that already exists: {drink.Name} {drink.Id}");
                return;
            }

            LookupIdTable.Add(drink.Id, drink);
            LookupStringTable.Add(drink.Name, drink);
            drink.Init();
        }

        /// <summary>
        /// Registers a enumerable of drinks into the lookup tables.
        /// </summary>
        /// <param name="drinks">Enumerable of drinks</param>
        public static void RegisterDrinks(IEnumerable<Drink> drinks)
        {
            foreach (Drink drink in drinks) RegisterDrink(drink);
        }

        /// <summary>
        /// Unregisters every drink registered. Resets LookupIdTable, LookupStringTable, Drinks.
        /// </summary>
        public static void UnregisterDrinks()
        {
            foreach (Drink drink in Drinks) drink.Destroy();

            LookupIdTable = new();
            LookupStringTable = new();
            Drinks = new();
        }

        /// <summary>
        /// Gets a drink by name or alias.
        /// </summary>
        /// <param name="name">Name or alias of the drink.</param>
        /// <returns>Returns a <see cref="Drink" /> if it is registered, else null.</returns>
        public static Drink GetDrink(string name)
        {
            if (LookupStringTable.ContainsKey(name)) return LookupStringTable[name];
            foreach(Drink drink in Drinks)
            {
                if(drink.Aliases.Contains(name)) return drink;
            }
            return null;
        }

        /// <summary>
        /// Gets a drink by id.
        /// </summary>
        /// <param name="id">Id of the drink.</param>
        /// <returns>Returns a <see cref="Drink"/> if it is registered, else null.</returns>
        public static Drink GetDrink(uint id) => LookupIdTable.ContainsKey(id) ? LookupIdTable[id] : null;

        /// <summary>
        /// Gets a random drink.
        /// </summary>
        /// <returns>Returns a random <see cref="Drink"/>.</returns>
        public static Drink GetRandomDrink() => Drinks.GetRandomValue();

        /// <summary>
        /// Returns whether or not a player is eligible to get a drink.
        /// </summary>
        /// <param name="player"></param>
        public static bool IsEligibleToGetDrink(Player player)
        {
            return List.Any(machine => machine.GetPlayersInRange().Contains(player));
        }

        /// <summary>
        /// Gets every player that is in range of the machine.
        /// </summary>
        public List<Player> GetPlayersInRange()
        {
            return PlayersInRange;
        }

        /// <summary>
        /// Gets every player that is in range of the machine that matches given predicate.
        /// </summary>
        public List<Player> GetPlayersInRange(Func<Player, bool> predicate)
        {
            return PlayersInRange.Where(predicate).ToList();
        }

        /// <summary>
        /// Checks if given player is in range of this machine
        /// </summary>
        /// <returns></returns>
        private bool InRange(Player player)
        {
            return Vector3.Distance(player.Position, Position) < Scp294.Instance.Config.Range;
        }

        /// <summary>
        /// Coroutine that runs internally to check if a player has entered the range of the machine.
        /// </summary>
        private IEnumerator<float> CheckPlayersAroundMachine()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(0.1f);

                foreach (Player player in Player.List)
                {
                    if (InRange(player) && player.CurrentRoom == Room && !PlayersInRange.Contains(player))
                    {
                        PlayerEnteredRangeEventArgs playerEnteredRangeEventArgs = new PlayerEnteredRangeEventArgs(player, this);
                        Machines.OnPlayerEnteredRange(playerEnteredRangeEventArgs);
                        PlayersInRange.Add(player);
                    }

                    if(!InRange(player) && PlayersInRange.Contains(player))
                    {
                        PlayerLeftEventArgs playerLeftEventArgs = new PlayerLeftEventArgs(player, this);
                        Machines.OnPlayerLeft(playerLeftEventArgs);
                        PlayersInRange.Remove(player);
                    }
                }
            }
        }
    }
}
