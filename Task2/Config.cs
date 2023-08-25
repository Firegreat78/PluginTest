using Exiled.API.Features.Items;
using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        public ItemType[] NotTriggeringTesla =
        {
            ItemType.KeycardO5,
            ItemType.KeycardNTFCommander,
            ItemType.None,
        };
    }
}
