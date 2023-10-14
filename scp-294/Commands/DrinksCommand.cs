using CommandSystem;
using Exiled.API.Features;
using scp_294.API.Features;
using scp_294.Items;
using System;
using System.Linq;

namespace scp_294.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class DrinksCommand : ICommand
    {
        /// <summary>
        /// Gets command name.
        /// </summary>
        public string Command => "customdrinks";

        /// <summary>
        /// Gets aliases.
        /// </summary>
        public string[] Aliases => new string[] {  };

        /// <summary>
        /// Gets command's description.
        /// </summary>
        public string Description => "Allows admins to spawn in drinks from scp294 and see registered drinks";

        /// <summary>
        /// Gets command's usage.
        /// </summary>
        private string Usage => "\ncustomdrinks:\ncustomdrinks give [id] [player_name/player_id/all] -> gives the indicated player the drink with the id provided. If player occulted, it will give it to the command sender.\ncustomdrinks list -> lists every registered drink.";

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="arguments">Players input.</param>
        /// <param name="sender">Sender of the command.</param>
        /// <param name="response">Response of the command.</param>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(arguments.Count == 0)
            {
                response = Usage;
                return true;
            }
            
            if(arguments.At(0) == "list")
            {
                response = "\n" + Machine.GetDrinksToString("\n");
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

                        if (!Machine.TryGetDrink(drink_id, out drink))
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
                        if (!Machine.TryGetDrink(drink_id, out drink))
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

                    if (!Machine.TryGetDrink(drink_id, out drink))
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


        /// <summary>
        /// Gets the player by Id.
        /// </summary>
        /// <param name="id">Player Id.</param>
        /// <returns>If found, returns the player else null.</returns>
        private Player GetPlayer(int id)
        {
            foreach(Player player in Player.List)
            {
                if(player.Id == id) return player;
            }
            return null;
        }

        /// <summary>
        /// Gets the player by <see cref="CommandSender"/> instance.
        /// </summary>
        /// <param name="sender">The <see cref="CommandSender"> instance.</param>
        /// <returns>If found, returns the player else null.</returns>
        private Player GetPlayer(CommandSender sender) 
        {
            foreach (Player player in Player.List)
            {
                if (player.Sender.SenderId == sender.SenderId) return player;
            }
            return null;
        }

        /// <summary>
        /// Gets the player by name.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <returns>If found, returns the player else null.</returns>
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
