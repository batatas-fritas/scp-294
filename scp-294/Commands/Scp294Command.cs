using CommandSystem;
using Exiled.API.Features;
using System;
using Exiled.API.Features.Items;
using scp_294.API.Features;
using scp_294.Items;
using scp_294.Events.EventArgs.Machines;
using scp_294.Events.Handlers;

namespace scp_294.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Scp294Command : ICommand
    {
        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string Command => "scp294";

        /// <summary>
        /// Gets the command aliases.
        /// </summary>
        public string[] Aliases => new string[] { "SCP294" };

        /// <summary>
        /// Gets the command description.
        /// </summary>
        public string Description => "Allows to order drinks from SCP-294";

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="arguments">Player input.</param>
        /// <param name="sender">Sender of the command.</param>
        /// <param name="response">Response of the command.</param>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = GetPlayer((CommandSender)sender);

            if (Machine.List.Count == 0 || player == null || player.IsDead || !player.IsHuman)
            {
                response = Scp294.Instance.Config.ErrorMessage;
                return true;
            }

            if (arguments.Count > 0 && arguments.At(0).ToLower() == "list")
            {
                if (arguments.Count == 1)
                {
                    response = "\n" + Machine.GetDrinksToString("\n");
                    return true;
                }
                else
                {
                    response = Scp294.Instance.Config.UsageMessage;
                    return true;
                }
            }

            if (!Machine.IsEligibleToGetDrink(player))
            {
                response = Scp294.Instance.Config.PlayerOutOfRange;
                return true;
            }

            if (player.CurrentItem == null || player.CurrentItem.Type != ItemType.Coin)
            {
                response = Scp294.Instance.Config.PlayerNotHoldingCoin;
                return true;
            }

            if (Scp294.Instance.Config.RandomMode)
            {
                if (arguments.Count > 0)
                {
                    response = Scp294.Instance.Config.UsageMessage;
                    return true;
                }

                Drink random_drink = Machine.GetRandomDrink();

                response = Scp294.Instance.Config.EnjoyDrinkMessage;
                RemoveCoinFromPlayer(player);
                random_drink.Give(player);
                
                DispensedDrinkEventArgs dispensedDrinkEventArgs = new DispensedDrinkEventArgs(player, random_drink);
                Log.Debug("Dispensed drink event about to be invoked...");
                Machines.OnMachineDispensedDrink(dispensedDrinkEventArgs);
                return true;
            }

            if (arguments.Count == 0)
            {
                response = Scp294.Instance.Config.UsageMessage;
                return true;
            }

            string drink_name = string.Join(" ", arguments);
            Log.Debug($"{player.Nickname} ordered a {drink_name}");
            Drink drink = Machine.GetDrink(drink_name);

            if (drink != null)
            {
                response = Scp294.Instance.Config.EnjoyDrinkMessage;
                RemoveCoinFromPlayer(player);
                drink.Give(player);

                DispensedDrinkEventArgs dispensedDrinkEventArgs = new DispensedDrinkEventArgs(player, drink);
                Log.Debug("Dispensed drink event about to be invoked...");
                Machines.OnMachineDispensedDrink(dispensedDrinkEventArgs);
            }
            else
            {
                response = Scp294.Instance.Config.OutOfRange;
            }

            return true;
        }

        /// <summary>
        /// Gets player by the <see cref="CommandSender"/> instance.
        /// </summary>
        /// <param name="sender">The <see cref="CommandSender"/> instance.</param>
        /// <returns>The player if found, null otherwise</returns>
        private Player GetPlayer(CommandSender sender)
        {
            foreach (Player player in Player.List)
            {
                if (player.Sender.SenderId == sender.SenderId) return player;
            }
            return null;
        }

        /// <summary>
        /// Removes a coin from the players inventory.
        /// </summary>
        /// <param name="player"></param>
        private void RemoveCoinFromPlayer(Player player)
        {
            foreach (Item item in player.Items)
            {
                if (item.Type == ItemType.Coin)
                {
                    player.RemoveItem(item);
                    return;
                }
            }
        }
    }
}