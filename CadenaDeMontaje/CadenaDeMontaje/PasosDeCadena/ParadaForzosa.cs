using CadenaDeMontaje.Interfaces;
using CadenaDeMontaje.Core;
using CadenaDeMontaje.Productos;

namespace CadenaDeMontaje.PasosDeCadena
{
    public class ParadaForzosa : PasoBaseMontajeDeCoche
    {
        public ParadaForzosa(IConfiguraciónDeCoche configuración, Coche coche) : base(configuración, coche)
        {
            EsFinal = true;
            Estado = "Cadena forzada a parar forzándose las forzadiones forzosamente";
        }

        public override IPasoDeCadena EjecutarPaso()
        {
            return this;
        }
    }
}