using CommandSystem;
using Exiled.API.Features;
using System;
using Exiled.API.Features.Items;
using System.Linq;
using scp_294.Scp;
using scp_294.Classes;
using MapEditorReborn.Commands.UtilityCommands;
using System.Collections.Generic;

namespace scp_294.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Scp294Command : ICommand
    {
        public string Command => "scp294";

        public string[] Aliases => new string[] { "scp294", "SCP294" };

        public string Description => "Allows to order drinks from SCP-294";
       

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = GetPlayer((CommandSender)sender);

            if (Scp294.Get() == null || player == null || player.IsDead || !player.IsHuman)
            {
                response = Plugin.Instance.Config.ErrorMessage;
                return false;
            }

            if (player.CurrentRoom != Scp294.Room || !Scp294.InRange(player.Position))
            {
                response = Plugin.Instance.Config.PlayerOutOfRange;
                return false;
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
                    return false;
                }
            }         

            if(player.CurrentItem == null || player.CurrentItem.Type != ItemType.Coin)
            {
                response = Plugin.Instance.Config.PlayerNotHoldingCoin;
                return false;
            }

            if(Plugin.Instance.Config.RandomMode)
            {
                if (arguments.Count > 0)
                {
                    response = Plugin.Instance.Config.UsageMessage;
                    return false;
                }

                DrinkOptions random_drink = GetRandomDrink();

                response = Plugin.Instance.Config.EnjoyDrinkMessage;
                RemoveCoinFromPlayer(player);
                Plugin.Instance.Drink.Options = random_drink;
                Plugin.Instance.Drink.Give(player);

                return true;              
            }

            if (arguments.Count == 0)
            {
                response = Plugin.Instance.Config.UsageMessage;
                return false;
            }

            string drink_name = string.Join(" ", arguments);
            Log.Debug($"{player.Nickname} ordered a {drink_name}");
            DrinkOptions drink = GetDrink(drink_name);

            if (drink != null)
            {
                response = Plugin.Instance.Config.EnjoyDrinkMessage;
                RemoveCoinFromPlayer(player);
                Plugin.Instance.Drink.Options = drink;
                Plugin.Instance.Drink.Give(player);
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
            string drinks = "\n" + string.Join("\n", Plugin.Instance.Config.Drinks.Where(drink => drink.IsEnabled).Select(drink => drink.Name));
            return drinks;
        }

        private DrinkOptions GetDrink(string name)
        {
            foreach(DrinkOptions drink in Plugin.Instance.Config.Drinks)
            {
                if((drink.Name == name || drink.Aliases.Contains(name)) && drink.IsEnabled) return drink;
            }
            return null;
        }

        private DrinkOptions GetRandomDrink()
        {
            List<DrinkOptions> available_drinks = Plugin.Instance.Config.Drinks.Where(drink => drink.IsEnabled).ToList();
            return available_drinks[new Random().Next(0, available_drinks.Count())];
        }
    }
}
