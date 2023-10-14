using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using scp_294.Items;

namespace scp_294.Events.EventArgs.Machines
{
    public class DispensedDrinkEventArgs : IExiledEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DispensedDrinkEventArgs"/> class.
        /// </summary>
        /// <param name="player">The player that received a drink.</param>
        /// <param name="drink">The drink given to the player.</param>
        public DispensedDrinkEventArgs(Player player, Drink drink)
        {
            Player = player;
            Drink = drink; 
        }

        /// <summary>
        /// Gets the <see cref="Player"/> that received the drink.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Gets the <see cref="Drink"> that was given to the player.
        /// </summary>
        public Drink Drink { get; set;}
    }
}
