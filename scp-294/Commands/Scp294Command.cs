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
       
            {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = GetPlayer((CommandSender)sender);

            if (Scp294.Get() == null || player == null || player.IsDead || !player.IsHuman)
            {
                response = Plugin.Instance.Config.ErrorMessage;
                return true;
            }
        }

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
                response = Plugin.Instance.Config.PlayerOutOfRange;
                return true;


                     
            if (arguments.Count > 0 && arguments.At(0).ToLower() == "list")
                response = Scp294.Config.ErrorMessage;
                return false;
            }

            if(arguments.Count == 0) 
            {
                response = Scp294.Config.UsageMessage;
                return false;
            }
                    response = Plugin.Instance.Config.UsageMessage;
                    return true;
            {
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
                } else
                {
                    response = Scp294.Config.UsageMessage;
                    return false;
                response = Plugin.Instance.Config.UsageMessage;
                return true;

            if (player.CurrentRoom != Scp294.Room || !Scp294.InRange(player.Position))
            {
            Log.Debug($"{player.Nickname} ordered a {drink_name}");
            Drink drink = GetDrink(drink_name);
            }

            if (arguments.Count == 0)
            {
                response = Scp294.Config.PlayerNotHoldingCoin;
                return false;
            }
            else
            {
                response = Plugin.Instance.Config.OutOfRange;
            }

            return true;
        }
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
        private string GetAllDrinkNames()
        {
            string drinks = "\n" + string.Join("\n", Plugin.Instance.Drinks.Select(drink => drink.Name));
            return drinks;
        }

        private Player GetPlayer(CommandSender sender)
        {
            foreach(Drink drink in Plugin.Instance.Drinks)
            {
                if (player.Sender.SenderId == sender.SenderId) return player;
            }
            return null;
        }

        private Drink GetRandomDrink()
        {
            return Plugin.Instance.Drinks.RandomItem();
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

        private Drink GetDrink(string name)
        {
            if(Drinks.Contains(name))
            {
                if(drink.Name == name || drink.Aliases.Contains(name)) return drink;
            }
            return null;
        }
    }
}
