using CommandSystem;
using Exiled.API.Features;
using System;
using Exiled.API.Features.Items;
using System.Linq;
using scp_294.Scp;
using scp_294.Classes;

namespace scp_294.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Scp294Command : ICommand
    {
        public string Command => "scp294";

        public string[] Aliases => new string[] { "SCP294" };

        public string Description => "Allows to order drinks from SCP-294";
       

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = GetPlayer((CommandSender)sender);

            if (Scp294.Get() == null || player == null || player.IsDead || !player.IsHuman)
            {
                response = Plugin.Instance.Config.ErrorMessage;
                return true;
            }

            if (player.CurrentRoom != Scp294.Room || !Scp294.InRange(player.Position))
            {
                response = Plugin.Instance.Config.PlayerOutOfRange;
                return true;
            }

                     
            if (arguments.Count > 0 && arguments.At(0).ToLower() == "list")
            {
                if(arguments.Count == 1)
                {
                    response = GetAllDrinkNames();
                    return true;
                } else
                {
                    response = Plugin.Instance.Config.UsageMessage;
                    return true;
                }
            }         

            if(player.CurrentItem == null || player.CurrentItem.Type != ItemType.Coin)
            {
                response = Plugin.Instance.Config.PlayerNotHoldingCoin;
                return true;
            }

            if(Plugin.Instance.Config.RandomMode)
            {
                if (arguments.Count > 0)
                {
                    response = Plugin.Instance.Config.UsageMessage;
                    return true;
                }

                Drink random_drink = GetRandomDrink();

                response = Plugin.Instance.Config.EnjoyDrinkMessage;
                RemoveCoinFromPlayer(player);
                random_drink.Give(player);

                return true;              
            }

            if (arguments.Count == 0)
            {
                response = Plugin.Instance.Config.UsageMessage;
                return true;
            }

            string drink_name = string.Join(" ", arguments);
            Log.Debug($"{player.Nickname} ordered a {drink_name}");
            Drink drink = GetDrink(drink_name);

            if (drink != null)
            {
                response = Plugin.Instance.Config.EnjoyDrinkMessage;
                RemoveCoinFromPlayer(player);
                drink.Give(player);
            }
            else
            {
                response = Plugin.Instance.Config.OutOfRange;
            }

            return true;
        }

        private Player GetPlayer(CommandSender sender)
        {
            foreach (Player player in Player.List)
            {
                if (player.Sender.SenderId == sender.SenderId) return player;
            }
            return null;
        }

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

        private string GetAllDrinkNames()
        {
            string drinks = "\n" + string.Join("\n", Plugin.Instance.Drinks.Select(drink => drink.Name));
            return drinks;
        }

        private Drink GetDrink(string name)
        {
            foreach(Drink drink in Plugin.Instance.Drinks)
            {
                if(drink.Name == name || drink.Aliases.Contains(name)) return drink;
            }
            return null;
        }

        private Drink GetRandomDrink()
        {
            return Plugin.Instance.Drinks.RandomItem();
        }
    }
}
