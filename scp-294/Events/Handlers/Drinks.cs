using Exiled.Events.Features;
using scp_294.Events.EventArgs.Drinks;

namespace scp_294.Events.Handlers
{
    public class Drinks
    {
        /// <summary>
        /// Invoked after player has consumed a drink.
        /// </summary>
        public static Event<ConsumedDrinkEventArgs> ConsumedDrink { get; set; } = new();

        /// <summary>
        /// Called after player has consumed a drink.
        /// </summary>
        /// <param name="ev">The <see cref="ConsumedDrinkEventArgs"/> instance.</param>
        public static void OnConsumedDrink(ConsumedDrinkEventArgs ev) => ConsumedDrink.InvokeSafely(ev);
    }
}
