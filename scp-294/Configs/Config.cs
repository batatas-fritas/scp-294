using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Loader;
using System.ComponentModel;
using System.IO;
using YamlDotNet.Serialization;

namespace scp_294.Configs
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("The path of the folder where the drinks will be stored.")]
        public string DrinksFolder { get; set; } = Path.Combine(Paths.Configs, "Drinks");

        [Description("The name of the file to load the drinks configs from")]
        public string DrinksFile { get; set; } = "global.yml";

        [Description("Whether or not random mode is enabled. Random Mode makes it so that every drink is random. This is false by default")]
        public bool RandomMode { get; set; } = false;

        [Description("Range to be able to use the machine")]
        public int Range { get; set; } = 2;

        [Description("Schematic Name. If you want to use a custom schematic, either change its name to scp294 or change this to the schematic's name")]
        public string SchematicName { get; set; } = "scp294";

        [Description("Message that appears once you approach Scp-294")]
        public string ApproachMessage { get; set; } = "You have approached SCP-294. Use .scp294 to get a drink";

        [Description("Message that appears once you approach Scp-294 with random mode enabled")]
        public string ApproachMessageRandomMode { get; set; } = "You have approached SCP-294. However the keyboard is broken, use '.scp294' to get a random drink";

        [Description("Message that appears after you get a drink")]
        public string EnjoyDrinkMessage { get; set; } = "<color=#00ff00>Enjoy your drink</color>";

        [Description("Message that appears if asked drink is not available")]
        public string OutOfRange { get; set; } = "<color=#ff0000>Out of range</color>";

        [Description("Message that appears if player is asked")]
        public string PlayerOutOfRange { get; set; } = "<color=#ff0000>you are not close enough to the machine</color>";

        [Description("Message that appears if player is not holding a coin")]
        public string PlayerNotHoldingCoin { get; set; } = "<color=#ff0000>you need to be holding coin</color>";

        [Description("Message that appears when an error occurs")]
        public string ErrorMessage { get; set; } = "<color=#ff0000>error occurred</color>";

        [Description("Message that appears if a player mistypes or uses the command incorrectly")]
        public string UsageMessage { get; set; } = "<color=#ff0000>Incorrect Usage. Try .scp294 [drink you want]. You can also use .scp294 list to print every drink currently available. If random mode is enabled you should type only '.scp294' to get your random drink</color>";

        [YamlIgnore]
        public DrinksConfig DrinksConfig { get; set; } = null!;

        public void LoadConfigs()
        {
            if (!Directory.Exists(DrinksFolder))
                Directory.CreateDirectory(DrinksFolder);

            string filePath = Path.Combine(DrinksFolder, DrinksFile);

            if (!File.Exists(filePath))
            {
                DrinksConfig = new DrinksConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(DrinksConfig));
            }
            else
            {
                DrinksConfig = Loader.Deserializer.Deserialize<DrinksConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(DrinksConfig));
            }
        }
    }
}
