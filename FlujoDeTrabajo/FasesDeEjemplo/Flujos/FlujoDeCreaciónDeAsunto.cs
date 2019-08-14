namespace FasesDeEjemplo.Flujos
{
    using System.Collections.Generic;
    using FlujoDeTrabajo.Interfaces;
    using FlujoDeTrabajo.Nucelo;
    using Fases;

    public sealed class FlujoDeCreaciónDeAsunto : Flujo
    {
        private readonly List<IFase> _fases;

        public override List<IFase> Fases => _fases;

        public FlujoDeCreaciónDeAsunto(string nombre) : base(nombre)
        {
            _fases = new List<IFase>
            {
                new CrearAsunto()
            };

            // Observadores.Add(new Observador(this));
        }
    }
}
