using Exiled.API.Features;
using MEC;
using Exiled.API.Features.Items;
using PlayerStatsSystem;
using PluginAPI.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Task2
{
    public class Plugin : Plugin<Config>
    {
        public static List<string> ReportsList = new List<string>();
        public static Dictionary<string, int> PlayerKills = new Dictionary<string, int>();
        public override string Name => "Тестовый мод";
        public override string Author => "Firegreat";

        public override void OnEnabled()
        {
            base.OnEnabled();
            Log.Info("-----Мод Task2 работает-----");
            Exiled.Events.Handlers.Player.Verified += Player_Verified;
            Exiled.Events.Handlers.Player.Dying += Player_Dying;
            Exiled.Events.Handlers.Player.TriggeringTesla += Player_TriggeringTesla;
            Exiled.Events.Handlers.Server.LocalReporting += Server_LocalReporting;
            Exiled.Events.Handlers.Map.ExplodingGrenade += Map_ExplodingGrenade;
            Exiled.Events.Handlers.Server.RoundEnded += Server_RoundEnded;
            Exiled.Events.Handlers.Server.RestartingRound += Server_RestartingRound;
        }
        private void Player_Verified(Exiled.Events.EventArgs.Player.VerifiedEventArgs ev)
        {
            if (!PlayerKills.ContainsKey(ev.Player.Nickname)) PlayerKills[ev.Player.Nickname] = 0;
        }

        private void Player_Dying(Exiled.Events.EventArgs.Player.DyingEventArgs ev)
        {
            if (ev.Attacker != Server.Host && ev.Attacker != ev.Player) PlayerKills[ev.Attacker.Nickname] += 1;
        }

        private void Player_TriggeringTesla(Exiled.Events.EventArgs.Player.TriggeringTeslaEventArgs ev)
        {
            ev.IsTriggerable = ev.Player.CurrentItem is null || !Config.NotTriggeringTesla.Contains(ev.Player.CurrentItem.Type);
        }

        private void Server_RoundEnded(Exiled.Events.EventArgs.Server.RoundEndedEventArgs ev)
        {
            int MaxKills = 0;
            string NickMaxKills = null;
            foreach (var nick in PlayerKills.Keys)
            {
                if (PlayerKills[nick] > MaxKills) 
                { 
                    MaxKills = PlayerKills[nick];
                    NickMaxKills = nick;
                }
            }
            Map.Broadcast(5, $"Больше всего убийств в раунде совершил игрок {NickMaxKills} ({MaxKills} убийств).");
        }

        private void Map_ExplodingGrenade(Exiled.Events.EventArgs.Map.ExplodingGrenadeEventArgs ev)
        {
            var lift = Lift.Get(ev.Projectile.Position);
            System.Random random = new System.Random();
            if (lift is null || random.NextDouble() < 0.5d) return;
            Timing.RunCoroutine(ActivateLiftWhenReady(lift));
        }

        private void Server_RestartingRound()
        {
            PlayerKills.Clear();
            ReportsList.Clear();
        }

        private void Server_LocalReporting(Exiled.Events.EventArgs.Player.LocalReportingEventArgs ev)
        {
            string msg = $"Игрок {ev.Player.Nickname} отправил репорт на игрока {ev.Target.Nickname}. Причина: {ev.Reason}.";
            ReportsList.Add(msg);
            Log.Info(msg);
            foreach (var player in Player.List)
            {
                if (player.RemoteAdminAccess) player.Broadcast(10, msg);
            }
        }

        private IEnumerator<float> ActivateLiftWhenReady(Lift lift)
        {
            while (lift.Status != Interactables.Interobjects.ElevatorChamber.ElevatorSequence.Ready)
            {
                yield return Timing.WaitForSeconds(.5f);
            }
            lift.TryStart(lift.CurrentLevel == 0 ? 1 : 0);
        }
    }
}
