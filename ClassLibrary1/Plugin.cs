using Exiled.API.Features;
using Exiled.API.Enums;

namespace Plugins
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "Тестовый мод";
        public override string Author => "Firegreat";
        public override void OnEnabled()
        {
            base.OnEnabled();
            Log.Info("-----Мод работает-----");
            Exiled.Events.Handlers.Warhead.Detonated += Warhead_Detonated;
            Exiled.Events.Handlers.Player.Spawned += Player_Spawned;
        }
        private void Player_Spawned(Exiled.Events.EventArgs.Player.SpawnedEventArgs ev)
        {
            if (ev.Player.Role.Type == PlayerRoles.RoleTypeId.Scientist)
            {
                ev.Player.Health = 500f;
                ev.Player.AddItem(ItemType.SCP500);
                ev.Player.AddAmmo(AmmoType.Nato9, 100);
            }
        }
        private void Warhead_Detonated()
        {
            foreach (var player in Player.List) player.Kill(DamageType.Warhead);
        }
    }
}
