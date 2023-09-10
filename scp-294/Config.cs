using Exiled.API.Interfaces;
using scp_294.Items;
using System.ComponentModel;

namespace scp_294
{
    public class Config : IConfig
    {
        [Description("Range to be able to use the machine")]
        public int Range { get; set; } = 2;

        [Description("Schematic Name. If you want to use a custom schematic, either change its name to scp294 or change this to the schematic's name")]
        public string SchematicName { get; set; } = "scp294";
        [Description("Message that appears once you approach Scp-294")]
        public string ApproachMessage { get; set; } = "You have approached SCP-294. Use .scp294 to get a drink";
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
        public Scp106Drink Scp106Drink { get; set;} = new();

        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
