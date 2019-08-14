namespace FlujoDeTrabajo.Nucelo.ComandosDelGestorDeFlujo
{
    using Interfaces;
    using System.Collections.Generic;

    internal abstract class ComandoConPilaDeEjecución : ComandoBase
    {
        protected Stack<IFase> ColaDeEjecución { get; private set; }

        public ComandoConPilaDeEjecución(Flujo flujo, Stack<IFase> colaDeEjecución) : base(flujo)
        {
            ColaDeEjecución = colaDeEjecución;
        }
    }
}
