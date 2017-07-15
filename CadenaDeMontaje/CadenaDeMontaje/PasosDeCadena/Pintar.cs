using CadenaDeMontaje.Interfaces;
using CadenaDeMontaje.Productos;
using CadenaDeMontaje.Core;

namespace CadenaDeMontaje.PasosDeCadena
{
    public class Pintar : PasoBaseMontajeDeCoche
    {
        public Pintar(IConfiguraciónDeCoche configuración, Coche coche) : base(configuración, coche)
        {
        }

        public override IPasoDeCadena EjecutarPaso()
        {
            Coche.Color = Configuración.Color;
            Estado = string.Format("Coche tuneado de color {0}", Coche.Color);
            EjecuciónCorrecta = true;
            return new ValidarMontaje(Configuración, Coche);
        }
    }
}