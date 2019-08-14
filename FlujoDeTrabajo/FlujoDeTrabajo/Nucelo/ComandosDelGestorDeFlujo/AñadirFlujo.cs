namespace FlujoDeTrabajo.Nucelo.ComandosDelGestorDeFlujo
{
    using Fases;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    internal class AñadirFlujo : ComandoConPilaDeEjecución
    {
        private readonly string _nombre;

        public AñadirFlujo(Flujo flujo, Stack<IFase> colaDeEjecución, string nombre) : base(flujo, colaDeEjecución)
        {
            _nombre = nombre;
        }

        public override void Ejecutar()
        {
            // Esto no sería mejor comprobarlo antes de insertar el comando??
            var flujos = Flujo.Flujos.Where(f => f.Nombre == _nombre).ToList();
            if (flujos.Count == 0)
            {
                // ERROR
                return;
            }

            Flujo.Configuración.EjecutorDeFlujo.EstablecerFlujo(flujos[0]);

            //REVERSE
            var nuevaLista = new List<IFase>(flujos[0].Fases);
            nuevaLista.Reverse();

            ColaDeEjecución.Push(new FinalizarSubflujo(flujos[0], Flujo));
            nuevaLista.ForEach(f => ColaDeEjecución.Push(f));
        }
    }
}
