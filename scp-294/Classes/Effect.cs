using Exiled.API.Enums;
using System.ComponentModel;

namespace scp_294.Classes
{
    public class Effect
    {
        [Description("Which effect is going to be applied.")]
        public EffectType Type { get; set; }

        [Description("For how long the effect will be applied")]
        public float Duration { get; set; }

        public Intensity Intensity { get; set; }

        [Description("The chance of this effect to be applied, in %")]
        public int Chance { get; set; }
    }
}
