using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MapEditorReborn.Events.EventArgs;
using scp_294.API.Features;
using scp_294.Events.EventArgs.Machines;

namespace scp_294.Events.Handlers.Internal
{
    internal class EventsHandler
    {
        /// <summary>
        /// Resets machine list when round ends.
        /// </summary>
        /// <param name="ev">The <see cref="RoundEndedEventArgs"/> instance.</param>
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Machine.StopMachines();
        }

        /// <summary>
        /// Initializes each <see cref="Machine"/> instance.
        /// </summary>
        /// <param name="ev">The <see cref="SchematicSpawnedEventArgs"> instance.</param>
        public void SchematicSpawned(SchematicSpawnedEventArgs ev)
        {
            if (ev == null) return;

            if (ev.Schematic.Name == Scp294.Instance.Config.SchematicName)
            {
                new Machine(ev.Schematic.CurrentRoom, ev.Schematic.Position);
            }
        }

        /// <summary>
        /// Shows hint to the player that entered in range of the <see cref="Machine"/>.
        /// </summary>
        /// <param name="ev">The <see cref="PlayerEnteredRangeEventArgs"/> instance.</param>
        public void PlayerEnteredRange(PlayerEnteredRangeEventArgs ev)
        {
            Log.Debug("Player entered range event invoked successfully");
            if (Scp294.Instance.Config.RandomMode)
            {
                ev.Player.ShowHint(Scp294.Instance.Config.ApproachMessageRandomMode);
            } else
            {
                ev.Player.ShowHint(Scp294.Instance.Config.ApproachMessage);
            }     
        }

        /// <summary>
        /// Verifies if explode on dispensing property is true and explodes.
        /// </summary>
        /// <param name="ev">The <see cref="DispensedDrinkEventArgs"/> instance.</param>
        public void OnMachineDispensedDrink(DispensedDrinkEventArgs ev)
        {
            Log.Debug("On machine dispensing drink event invoked successfully");
            if (ev.Drink.ExtraEffects.ExplodeOnDispensing) Map.Explode(ev.Player.Position, ProjectileType.FragGrenade);
        }
    }
}
