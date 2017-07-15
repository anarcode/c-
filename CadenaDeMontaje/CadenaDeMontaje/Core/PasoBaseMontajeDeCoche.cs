using CadenaDeMontaje.Interfaces;
using CadenaDeMontaje.Productos;

namespace CadenaDeMontaje.Core
{
    public abstract class PasoBaseMontajeDeCoche : IPasoDeCadena
    {
        protected IConfiguraciónDeCoche Configuración { get; private set; }

        protected Coche Coche { get; private set; }

        public bool EsFinal { get; protected set; }

        public string Estado { get; protected set; }

        public bool EjecuciónCorrecta { get; protected set; }

        public abstract IPasoDeCadena EjecutarPaso();

        protected PasoBaseMontajeDeCoche(IConfiguraciónDeCoche configruaciónDeCoche, Coche coche)
        {
            Configuración = configruaciónDeCoche;
            Coche = coche;
            EjecuciónCorrecta = false;
            EsFinal = false;
            Estado = "Sin incializar";
        }
    }
}