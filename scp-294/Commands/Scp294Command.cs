using CommandSystem;
using Exiled.API.Features;
using System;
using Exiled.CustomItems.API.Features;
using Exiled.API.Features.Items;
using System.Linq;
using scp_294.Scp;

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
                response = "<color=#ff0000>error occurred</color>";
                return false;
            }

            if(arguments.Count == 0) 
            {
                response = GetUsage();
                return false;
            }

            
            if (arguments.At(0).ToLower() == "list")
            {
                if(arguments.Count == 1)
                {
                    response = "\n" + GetAllDrinkNames();
                    return true;
                } else
                {
                    response = GetUsage();
                    return false;
                }
            }

            if (player.CurrentRoom.name != "EZ_PCs" || !Scp294.InRange(player.Position))
            {
                response = "<color=#ff0000>you are not close enough to the machine</color>";
                return false;
            }

            string drink_name = string.Join(" ", arguments);
            // Log.Debug($"{player.Nickname} ordered a {drink_name}");

            if(player.CurrentItem == null || player.CurrentItem.Type != ItemType.Coin)
            {
                response = "<color=#ff0000>you need to be holding coin</color>";
                return false;
            }

            CustomItem drink = CustomItem.Get(drink_name);

            if(drink != null) 
            {
                response = "<color=#00ff00>Enjoy your drink</color>";
                RemoveCoinFromPlayer(player);
                drink.Give(player);
                return true;
            } else
            {
                response = "<color=#00ff00>Enjoy your drink</color>";
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
                        response = "<color=#ff0000>Out of range</color>";
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

        private string GetUsage()
        {
            return "\n<color=#ff0000>Incorrect Usage. Try .scp294 [drink you want]</color>\n<color=#ff0000>You can also use .scp294 list to print every drink currently available</color>";
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
            return string.Join("\n", CustomItem.Registered.Select(item => "<color=#00ff00>" + item.Name + "</color>"));
        }
    }
}
