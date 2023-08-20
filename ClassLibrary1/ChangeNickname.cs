using CommandSystem;
using Exiled.API.Features;
using System;

namespace Plugins
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ChangeNickname : ICommand
    {
        public string Command => "changenick";
        public string[] Aliases => new string[] {"nick"};
        public string Description => "Изменить ник на сервере. Синтаксис: .nick [новый_никнейм]. Пример: \".nick Мой_новый_никнейм\" -> Новый ник после выполнения команды: \"Мой новый никнейм\"";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Некорректное использование.";
                return false;
            }
            var player = Player.Get(sender);
            var nick = arguments.Array[1].Replace('_', ' ');
            player.CustomName = nick;
            response = $"Successfully changed nickname to {nick}.";
            return true;
        }
    }
}
