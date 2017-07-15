using CadenaDeMontaje.Interfaces;

namespace CadenaDeMontaje.PasosDeCadena
{
    public class FinalizarMontaje : IPasoDeCadena
    {
        public bool EjecuciónCorrecta
        {
            get { return true; }
        }

        public bool EsFinal
        {
            get { return true; }
        }

        public string Estado
        {
            get { return "Montaje finalizado correctamente"; }
        }

        public IPasoDeCadena EjecutarPaso()
        {
            return this;
        }
    }
}