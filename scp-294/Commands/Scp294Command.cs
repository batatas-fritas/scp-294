using CommandSystem;
using Exiled.API.Features;
using System;
using Exiled.CustomItems.API.Features;
using Exiled.API.Features.Items;
using System.Linq;
using scp_294.Scp;
using System.Collections.Generic;

namespace scp_294.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Scp294Command : ICommand
    {
        public string Command => "scp294";

        public string[] Aliases => new string[] { "scp294", "SCP294" };

        public string Description => "Allows to order drinks from SCP-294";

        private List<string> Drinks
        {
            get
            {
                if(Scp294.Get() == null) return null;

                return new()
                {
                    Scp294.Config.CandyBlueJuice.Name,
                    Scp294.Config.CandyGreenJuice.Name,
                    Scp294.Config.CandyJuice.Name,
                    Scp294.Config.CandyPinkJuice.Name,
                    Scp294.Config.CandyPurpleJuice.Name,
                    Scp294.Config.CandyRainbowJuice.Name,
                    Scp294.Config.CandyRedJuice.Name,
                    Scp294.Config.CandyYellowJuice.Name,
                    Scp294.Config.Scp106Drink.Name,
                    Scp294.Config.Scp173Drink.Name,
                    Scp294.Config.ScpDrink.Name,
                    Scp294.Config.TeleportationDrink.Name,
                    Scp294.Config.ThickJuice.Name,
                };
            }
        }

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = GetPlayer((CommandSender)sender);

            if (Scp294.Get() == null || player == null || player.IsDead || !player.IsHuman)
            {
                response = Scp294.Config.ErrorMessage;
                return false;
            }

            if(arguments.Count == 0) 
            {
                response = Scp294.Config.UsageMessage;
                return false;
            }
           
            if (arguments.At(0).ToLower() == "list")
            {
                if(arguments.Count == 1)
                {
                    response = GetAllDrinkNames();
                    return true;
                } else
                {
                    response = Scp294.Config.UsageMessage;
                    return false;
                }
            }

            if (player.CurrentRoom != Scp294.Room || !Scp294.InRange(player.Position))
            {
                response = Scp294.Config.PlayerOutOfRange;
                return false;
            }

            if(player.CurrentItem == null || player.CurrentItem.Type != ItemType.Coin)
            {
                response = Scp294.Config.PlayerNotHoldingCoin;
                return false;
            }

            string drink_name = string.Join(" ", arguments);
            // Log.Debug($"{player.Nickname} ordered a {drink_name}");
            CustomItem drink = GetDrink(drink_name);

            if(drink != null) 
            {
                response = Scp294.Config.EnjoyDrinkMessage;
                RemoveCoinFromPlayer(player);
                drink.Give(player);
                return true;
            } else
            {
                response = Scp294.Config.EnjoyDrinkMessage;
                switch (drink_name)
                {
                    case "cola":
                    case "scp207":
                        RemoveCoinFromPlayer(player);
                        player.AddItem(ItemType.SCP207);
                        break;
                    case "anticola":
                    case "scp207?":
                    case "antiscp207":
                        RemoveCoinFromPlayer(player);
                        player.AddItem(ItemType.AntiSCP207);
                        break;
                    default:
                        response = Scp294.Config.OutOfRange;
                        return true;
                }

                return true;
            }
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
            string drinks = "\n" + string.Join("\n", Drinks.Select(item => "<color=#00ff00>" + item + "</color>"));
            drinks += "\n" + "<color=#00ff00>scp207</color>";
            drinks += "\n" + "<color=#00ff00>scp207?</color>";
            return drinks;
        }

        private CustomItem GetDrink(string name)
        {
            if(Drinks.Contains(name))
            {
                return CustomItem.Get(name);
            }
            return null;
        }
    }
}
