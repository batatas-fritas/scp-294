using Exiled.API.Interfaces;
using Exiled.CustomItems.API.Features;
using scp_294.Items;
using System.Collections.Generic;

namespace scp_294
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;
        public ThickJuice ThickJuice { get; set; } = new ThickJuice();
    }
}
