using Exiled.Events.EventArgs.Interfaces;
using Exiled.API.Features;
using scp_294.API.Features;

namespace scp_294.Events.EventArgs.Machines
{
    public class PlayerEnteredRangeEventArgs : IExiledEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerEnteredRangeEventArgs"/> class.
        /// </summary>
        /// <param name="player">Player that entered in range.</param>
        /// <param name="machine">Machine that triggered the event.</param>
        public PlayerEnteredRangeEventArgs(Player player, Machine machine)
        {
            Player = player;
            Machine = machine;
        }

        /// <summary>
        /// Gets the machine the player is now in range of.
        /// </summary>
        public Machine Machine { get; }

        /// <summary>
        /// Gets the player that entered in range of the machine.
        /// </summary>
        public Player Player { get; }
    }
}
