using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled;
using MEC;
using System.Linq.Expressions;

namespace Plugins
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Invisibility : ICommand
    {
        public string Command => "invisibility";
        public string[] Aliases => new string[] {"inv", "i", "268", "scp268", "invisible", "invcap"};
        public string Description => "SCP-268 - Invisibility cap.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            
            var player = Player.Get(sender);
            StatusEffectBase statusEffect = null;
            bool isInvisible = player.TryGetEffect(EffectType.Invisible, out statusEffect);

            if (isInvisible) player.EnableEffect(EffectType.Invisible);
            else player.DisableEffect(EffectType.Invisible);

            string msg = "";
            foreach (var str in arguments) msg += (str + " ");
            player.Broadcast(5, msg);
            player.SendConsoleMessage(msg, "red");
            response = msg;
            return true;
        }
    }
}
