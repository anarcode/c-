using CadenaDeMontaje.Interfaces;
using CadenaDeMontaje.Core;
using CadenaDeMontaje.Productos;

namespace CadenaDeMontaje.PasosDeCadena
{
    public class ValidarMontaje : PasoBaseMontajeDeCoche
    {
        public ValidarMontaje(IConfiguraciónDeCoche configuración, Coche coche) : base(configuración, coche)
        {
            EsFinal = true;
        }

        public override IPasoDeCadena EjecutarPaso()
        {
            Estado = "Validador dice: ";
            EjecuciónCorrecta = true;

            if (!Coche.TieneChasis)
            {
                Estado += "El coche no tiene chasis. Mal vamos... ";
                EjecuciónCorrecta = false;
            }

            if (Configuración.NecesitaRuedas && !Coche.TieneRuedas)
            {
                Estado += "El coche no tiene ruedas y por lo visto hacen falta. ";
                EjecuciónCorrecta = false;
            }

            if (string.IsNullOrEmpty(Coche.Color))
            {
                Estado += "El coche viene sin pintar. ";
                EjecuciónCorrecta = false;
            }

            if (EjecuciónCorrecta)
            {
                Estado += "El coche está fetén.";
            }
            
            return this;
        }
    }
}