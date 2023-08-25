using CommandSystem;
using Exiled.API.Features;
using System;
using System.Linq;

namespace Plugins
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ChangeNickname : ICommand
    {
        public string Command => "changenick";
        public string[] Aliases => new string[] {"nick"};
        public string Description => "Изменить ник на сервере.\nИспользование: .nick [новый_никнейм].";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Некорректное использование.";
                return false;
            }
            var player = Player.Get(sender);
            var nick = String.Join(" ", arguments);
            player.CustomName = nick;
            response = $"Successfully changed nickname to {nick}.";
            return true;
        }
    }
}
