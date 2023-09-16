using CommandSystem;
using Exiled.API.Features;
using scp_294.Classes;
using System;
using System.Linq;

namespace scp_294.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class DrinksCommand : ICommand
    {
        public string Command => "customdrinks";

        public string[] Aliases => new string[] {  };

        public string Description => "Allows admins to spawn in drinks from scp294 and see registered drinks";

        private string Usage => "\ncustomdrinks:\ncustomdrinks give [id] [player_name/player_id/all] -> gives the indicated player the drink with the id provided. If player occulted, it will give it to the command sender.\ncustomdrinks list -> lists every registered drink.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(arguments.Count == 0)
            {
                response = Usage;
                return true;
            }
            
            if(arguments.At(0) == "list")
            {
                response = GetDrinkList();
                return true;
            }

            uint drink_id = Convert.ToUInt32(arguments.At(1));

            if (arguments.At(0) == "give")
            {
                if(arguments.Count == 3)
                {
                    int player_id;
                    Drink drink;

                    if(int.TryParse(arguments.At(2), out player_id))
                    {
                        Player player = GetPlayer(player_id);

                        if (!Plugin.Instance.LookupIdTable.TryGetValue(drink_id, out drink))
                        {
                            response = $"Drink with id: {drink_id} not registered";
                            return true;
                        }
                      
                        if (player != null)
                        {
                            drink.Give(player);
                            response = $"Gave {drink.Name} to player {player.Nickname}";
                            return true;
                        }
                    } else
                    {
                        if (!Plugin.Instance.LookupIdTable.TryGetValue(drink_id, out drink))
                        {
                            response = $"Drink with id: {drink_id} not registered";
                            return true;
                        }

                        if (arguments.At(2) == "all")
                        {
                            foreach (Player p in Player.List.Where(player => player.IsAlive)) drink.Give(p);
                            response = $"Gave {drink.Name} to every player alive";
                            return true;
                        }

                        Player player = GetPlayer(arguments.At(2));
                        if(player != null)
                        {
                            drink.Give(player);
                            response = $"Gave {drink.Name} to player {player.Nickname}";
                            return true;
                        }

                    }
                }

                if(arguments.Count == 2) {
                    Drink drink;

                    if (!Plugin.Instance.LookupIdTable.TryGetValue(drink_id, out drink))
                    {
                        response = $"Drink with id: {drink_id} not registered";
                        return true;
                    }

                    Player player = GetPlayer((CommandSender) sender);
                    if(player != null)
                    {
                        drink.Give(player);
                        response = $"Gave {drink.Name} to player {player.Nickname}";
                        return true;
                    }
                }
            }

            response = Usage;
            return true;
        }

        private string GetDrinkList()
        {
            return "\n" + string.Join("\n", Plugin.Instance.Drinks.Select(drink => $"[{drink.Id}] Name: {drink.Name} | Aliases: {string.Join(" | ", drink.Aliases)}"));
        }

        private Player GetPlayer(int id)
        {
            foreach(Player player in Player.List)
            {
                if(player.Id == id) return player;
            }
            return null;
        }

        private Player GetPlayer(CommandSender sender) 
        {
            foreach (Player player in Player.List)
            {
                if (player.Sender.SenderId == sender.SenderId) return player;
            }
            return null;
        }

        private Player GetPlayer(string name)
        {
            foreach (Player player in Player.List)
            {
                if (player.Nickname.StartsWith(name) || player.Nickname == name) return player;
            }
            return null;
        }
    }
}
