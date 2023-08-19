using Exiled.API.Interfaces;

namespace Plugins
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public uint Duration { get; set; } = 5;
    }
}
