using CadenaDeMontaje.Interfaces;
using CadenaDeMontaje.Productos;
using CadenaDeMontaje.Core;

namespace CadenaDeMontaje.PasosDeCadena
{
    public class AñadirChasis : PasoBaseMontajeDeCoche
    {
        public AñadirChasis(IConfiguraciónDeCoche configuración, Coche coche) : base(configuración, coche)
        {
        }

        public override IPasoDeCadena EjecutarPaso()
        {
            Coche.TieneChasis = true;
            Estado = "Añadido chasis";
            EjecuciónCorrecta = true;
            if (Configuración.NecesitaRuedas)
            {
                return new AñadirRuedas(Configuración, Coche);
            }
            else
            {
                return new Pintar(Configuración, Coche);
            }
        }
    }
}