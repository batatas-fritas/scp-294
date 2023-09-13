using Exiled.API.Enums;
using System.ComponentModel;

namespace scp_294.Classes
{
    public class Effect
    {
        public EffectType Type { get; set; }

        public float Duration { get; set; }

        public Intensity Intensity { get; set; }

        [Description("The chance of this effect to be applied, in %")]
        public int Chance { get; set; }
    }
}
