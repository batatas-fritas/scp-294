using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using scp_294.Classes;

namespace scp_294.Events.EventArgs.Drinks
{
    public class ConsumedDrinkEventArgs : IExiledEvent
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumedDrinkEventArgs"/> class.
        /// </summary>
        /// <param name="player">Player that consumed the drink.</param>
        /// <param name="drink">Drink that has been consumed.</param>
        public ConsumedDrinkEventArgs(Player player, Drink drink)
        {
            Player = player;
            Drink = drink;
        }

        /// <summary>
        /// Gets the drink that has been consumed.
        /// </summary>
        public Drink Drink { get; }

        /// <summary>
        /// Gets the player that consumed the drink.
        /// </summary>
        public Player Player { get; }

    }
}
