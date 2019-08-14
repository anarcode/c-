namespace FlujoDeTrabajo.Fases
{
    using Atributos;
    using Entidades;
    using Interfaces;
    using Nucelo;

    internal class FinalizarSubflujo : IFase
    {
        private readonly Flujo _flujo;
        private readonly Flujo _flujoAnterior;

        public FinalizarSubflujo(Flujo flujo, Flujo flujoAnterior)
        {
            _flujo = flujo;
            _flujoAnterior = flujoAnterior;
        }

        [MétodoDeFlujo]
        public IResultado<EjecuciónDeFlujo> Procesar() // Tendré que tener en cuenta el flujo actual
        {
            // Aqui tengo que volcar datos de un flujo a otro y esas cosas
            _flujo.DatosDeEjecución.VolcarDatos(_flujoAnterior.DatosDeEjecución);
            _flujoAnterior.Configuración.EjecutorDeFlujo.EstablecerFlujo(_flujoAnterior);

            DatosDeEjecución datos = new DatosDeEjecución();
            _flujo.DatosDeEjecución.VolcarDatos(datos);
            return new ResultadoDeEjecuciónDeFlujo(true, new EjecuciónDeFlujo(datos));
        }
    }
}
