using Exiled.API.Features;
namespace Plugins
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "Тестовый мод";
        public override string Author => "FiregreaT";
        public override void OnEnabled()
        {
            
            base.OnEnabled();
            Log.Info("Мод работает");
            Exiled.Events.Handlers.Player.Verified += Player_Verified;
            Exiled.Events.Handlers.Player.InteractingDoor += Player_InteractingDoor;
            Exiled.Events.Handlers.Server.RespawningTeam += Server_RespawningTeam;
            Exiled.Events.Handlers.Scp079.LockingDown += Scp079_LockingDown;
            Exiled.Events.Handlers.Scp079.TriggeringDoor += Scp079_TriggeringDoor;
        }

        private void Scp079_TriggeringDoor(Exiled.Events.EventArgs.Scp079.TriggeringDoorEventArgs ev)
        {
            
        }

        private void Scp079_LockingDown(Exiled.Events.EventArgs.Scp079.LockingDownEventArgs ev)
        {
            
        }

        private void Server_RespawningTeam(Exiled.Events.EventArgs.Server.RespawningTeamEventArgs ev)
        {
            
        }
        private void Player_InteractingDoor(Exiled.Events.EventArgs.Player.InteractingDoorEventArgs ev)
        {
            var msg = ev.IsAllowed ? $"Вы {(ev.Door.IsOpen ? "закрыли" : "открыли")} дверь {ev.Door.Name} (id: {ev.Door.InstanceId})" : "Вы не можете открыть эту дверь.";
            ev.Player.Broadcast(1, msg);
        }

        private void Player_Verified(Exiled.Events.EventArgs.Player.VerifiedEventArgs ev)
        {
            ev.Player.Broadcast(5, "Добро пожаловать! (мод)");
        }
    }
}
