using Exiled.Events.Features;
using scp_294.Events.EventArgs.Machines;

namespace scp_294.Events.Handlers
{
    public class Machines
    {
        /// <summary>
        /// Invoked after player has entered the range of a machine.
        /// </summary>
        public static Event<PlayerEnteredRangeEventArgs> PlayerEnteredRange { get; set; } = new();

        /// <summary>
        /// Invoked after player has left the machine.
        /// </summary>
        public static Event<PlayerLeftEventArgs> PlayerLeft { get; set; } = new();

        /// <summary>
        /// Invoked after machine has dispensed a drink to a player.
        /// </summary>
        public static Event<DispensedDrinkEventArgs> DispensedDrink { get; set; } = new();

        /// <summary>
        /// Called after player has entered the range of a machine.
        /// </summary>
        /// <param name="ev">The <see cref="PlayerEnteredRangeEventArgs"/> instance.</param>
        public static void OnPlayerEnteredRange(PlayerEnteredRangeEventArgs ev) => PlayerEnteredRange.InvokeSafely(ev);

        /// <summary>
        /// Called after player has left the machine.
        /// </summary>
        /// <param name="ev">The <see cref="PlayerLeftEventArgs"/> instance.</param>
        public static void OnPlayerLeft(PlayerLeftEventArgs ev) => PlayerLeft.InvokeSafely(ev);

        /// <summary>
        /// Called after machine has dispensed a drink to a player.
        /// </summary>
        /// <param name="ev">The <see cref="DispensedDrinkEventArgs"/> instance.</param>
        public static void OnMachineDispensedDrink(DispensedDrinkEventArgs ev) => DispensedDrink.InvokeSafely(ev);

    }
}
