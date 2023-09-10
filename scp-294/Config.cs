using Exiled.API.Interfaces;
using Exiled.CustomItems.API.Features;
using scp_294.Items;
using System.Collections.Generic;
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

        [Description("Message that appears after you get a drink")]
        public string EnjoyDrinkMessage { get; set; } = "<color=#00ff00>Enjoy your drink</color>";

        [Description("Message that appears if asked drink is not available")]
        public string OutOfRange { get; set; } = "<color=#ff0000>Out of range</color>";

        [Description("Message that appears if player is asked ")]
        public string PlayerOutOfRange { get; set; } = "<color=#ff0000>you are not close enough to the machine</color>";

        [Description("Message that appears if player is not holding a coin")]
        public string PlayerNotHoldingCoin { get; set; } = "<color=#ff0000>you need to be holding coin</color>";

        [Description("Message that appears when an error occurs")]
        public string ErrorMessage { get; set; } = "<color=#ff0000>error occurred</color>";

        [Description("Message that appears if a player mistypes or uses the command incorrectly")]
        public string UsageMessage { get; set; } = "\n<color=#ff0000>Incorrect Usage. Try .scp294 [drink you want]</color>\n<color=#ff0000>You can also use .scp294 list to print every drink currently available</color>";

        public List<CustomItem> Drinks { get; set; } = new()
        {
            new ThickJuice(),
            new CandyJuice(),
            new CandyRainbowJuice(),
            new CandyYellowJuice(),
            new CandyPurpleJuice(),
            new CandyRedJuice(),
            new CandyGreenJuice(),
            new CandyBlueJuice(),
            new CandyPinkJuice(),
            new TeleportationDrink(),
            new ScpDrink(),
            new Scp173Drink(),
            new Scp106Drink(),
        };

        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
