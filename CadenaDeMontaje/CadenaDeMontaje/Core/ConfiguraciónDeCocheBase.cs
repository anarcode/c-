using CadenaDeMontaje.Interfaces;

namespace CadenaDeMontaje.Core
{
    public class ConfiguraciónDeCocheBase : IConfiguraciónDeCoche
    {
        public virtual bool NecesitaRuedas { get { return true; } }

        public virtual int NúmeroDeRuedas { get { return 4; } }

        public virtual string Color { get; set; }
    }
}