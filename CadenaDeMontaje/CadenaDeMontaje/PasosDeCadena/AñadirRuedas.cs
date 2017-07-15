using CadenaDeMontaje.Interfaces;
using CadenaDeMontaje.Core;
using CadenaDeMontaje.Productos;

namespace CadenaDeMontaje.PasosDeCadena
{
    public class AñadirRuedas : PasoBaseMontajeDeCoche
    {
        int _númeroDeRuedas;

        public AñadirRuedas(IConfiguraciónDeCoche configuración, Coche coche) : base(configuración, coche)
        {
            EsFinal = false;
            EjecuciónCorrecta = true;
        }

        IPasoDeCadena FinalizarPaso()
        {
            Coche.TieneRuedas = true;
            Estado += string.Format(". El coche tiene {0} ruedas, listos para el Dakar...", _númeroDeRuedas);
            return new Pintar(Configuración, Coche);
        }

        public override IPasoDeCadena EjecutarPaso()
        {
            if(_númeroDeRuedas < Configuración.NúmeroDeRuedas)
            {
                _númeroDeRuedas++;
                Estado = "Añadida una rueda";

                if(_númeroDeRuedas == Configuración.NúmeroDeRuedas)
                {
                    return FinalizarPaso();
                }
                else
                {
                    return this;
                }
            }
            else if(_númeroDeRuedas == Configuración.NúmeroDeRuedas)
            {
                return FinalizarPaso();
            }
            else
            {
                EjecuciónCorrecta = false;
                Estado = "Se han añadido más ruedas de las configuradas.";
                return new ParadaForzosa(Configuración, Coche);
            }
        }
    }
}