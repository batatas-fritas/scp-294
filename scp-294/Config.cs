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

        public ThickJuice ThickJuice { get; set; } = new ThickJuice();
        public CandyJuice CandyJuice { get; set; } = new CandyJuice();
        public CandyRainbowJuice CandyRainbowJuice { get; set; } = new CandyRainbowJuice();
        public CandyYellowJuice CandyYellowJuice { get; set; } = new CandyYellowJuice();
        public CandyPurpleJuice CandyPurpleJuice { get; set; } = new CandyPurpleJuice();
        public CandyRedJuice CandyRedJuice { get; set; } = new CandyRedJuice();
        public CandyGreenJuice CandyGreenJuice { get; set; } = new CandyGreenJuice();
        public CandyBlueJuice CandyBlueJuice { get; set; } = new CandyBlueJuice();
        public CandyPinkJuice CandyPinkJuice { get; set; } = new CandyPinkJuice();
    }
}
