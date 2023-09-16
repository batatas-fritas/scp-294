using scp_294.Classes;
using System.Collections.Generic;

namespace scp_294.Configs
{
    public class DrinksConfig
    {
        public List<Drink> Drinks { get; set; } = new()
        {
            new Drink(),
        };
    }
}
