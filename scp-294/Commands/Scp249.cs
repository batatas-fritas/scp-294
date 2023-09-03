using CommandSystem;
using Exiled.API.Features;
using System;

namespace scp_294.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Scp249 : ICommand
    {
        public string Command => "scp249";

        public string[] Aliases => new string[] { "scp249" };

        public string Description => "Allows to order drinks from SCP-249";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            for (int i = 0; i < arguments.Count; i++)
            {
                Log.Debug(arguments.At(i));
            }

            Log.Debug($"The player that sent the command is: {GetPlayer((CommandSender) sender).Nickname}");

            response = "command received, information sent to server console";
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
    }
}
