using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using scp_294.API.Features;

namespace scp_294.Events.EventArgs.Machines
{
    public class PlayerLeftEventArgs : IExiledEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerLeftEventArgs"/> class.
        /// </summary>
        /// <param name="player">Player that left.</param>
        /// <param name="machine">Machine that triggered the event.</param>
        public PlayerLeftEventArgs(Player player, Machine machine)
        {
            Player = player;
            Machine = machine;
        }

        /// <summary>
        /// Gets the machine the player left.
        /// </summary>
        public Machine Machine { get; }

        /// <summary>
        /// Gets the player that left.
        /// </summary>
        public Player Player { get; }
    }
}
