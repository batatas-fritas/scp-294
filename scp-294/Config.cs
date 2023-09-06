using Exiled.API.Interfaces;
using Exiled.CustomItems.API.Features;
using scp_294.Items;
using System.Collections.Generic;
using System.ComponentModel;

namespace scp_294
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;

        [Description("Range to be able to use the machine")]
        public int Range { get; set; } = 2;

        public ThickJuice ThickJuice { get; set; } = new();
        public CandyJuice CandyJuice { get; set; } = new();
        public CandyRainbowJuice CandyRainbowJuice { get; set; } = new();
        public CandyYellowJuice CandyYellowJuice { get; set; } = new();
        public CandyPurpleJuice CandyPurpleJuice { get; set; } = new();
        public CandyRedJuice CandyRedJuice { get; set; } = new();
        public CandyGreenJuice CandyGreenJuice { get; set; } = new();
        public CandyBlueJuice CandyBlueJuice { get; set; } = new();
        public CandyPinkJuice CandyPinkJuice { get; set; } = new();
        public TeleportationDrink TeleportationDrink { get; set; } = new();
        public ScpDrink ScpDrink { get; set; } = new();
        public Scp173Drink Scp173Drink { get;set; } = new();
    }
}
