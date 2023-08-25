using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using System;
using UnityEngine;

namespace Task2
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class RemoveNearest : ICommand
    {
        public string Command => "removenearest";

        public string[] Aliases => new string[] { "remnearest", "removenear", "remnear", };

        public string Description => "Удалить все предметы и трупы, находящиеся ближе указанного расстояния.\nИспользование: removenearest [расстояние]";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Команда принимает ровно один числовой аргумент.";
                return false;
            }
            else if (Single.TryParse(arguments.Array[1], out float distance) && distance > 0f)
            {
                var player = Player.Get(sender);
                int DeletedPickups = 0;
                int DeletedRagdolls = 0;
                foreach (var pickup in Pickup.List)
                {
                    bool IsInSameZone = pickup.Room.Zone == player.Zone;
                    if (IsInSameZone && Vector3.Distance(player.Position, pickup.Base.transform.position) <= distance) 
                    { 
                        pickup.Destroy();
                        ++DeletedPickups;
                    }
                }
                foreach (var ragdoll in Ragdoll.List)
                {
                    bool IsInSameZone = ragdoll.Zone == player.Zone;
                    if (IsInSameZone && Vector3.Distance(ragdoll.Position, player.Position) <= distance)
                    {
                        ragdoll.Destroy();
                        ++DeletedRagdolls;
                    }
                }
                response = $"Было удалено {DeletedPickups} предметов и {DeletedRagdolls} трупов на расстоянии {distance} от вас.";
                return true;
            }
            else
            {
                response = $"Аргумент [{arguments.Array[1]}] не является корректным значением расстояния.";
                return false;
            }
        }
    }
}
