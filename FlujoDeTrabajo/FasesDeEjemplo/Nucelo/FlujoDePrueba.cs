namespace FasesDeEjemplo.Nucelo
{
    using System.Collections.Generic;
    using FlujoDeTrabajo.Atributos;
    using FlujoDeTrabajo.Interfaces;
    using FlujoDeTrabajo.Nucelo;
    using Fases;

    public sealed class FlujoDePrueba : Flujo
    {
        private readonly List<IFase> _fases;

        [DatoDeFlujo("nombre")]
        public string NombreDeUsuario { get; set; }

        public override List<IFase> Fases => _fases;

        public FlujoDePrueba() : base()
        {
            _fases = new List<IFase>
            {
                new Autenticación(),
                new Autorización(),
                new CrearAsunto()
            };
        }
    }
}
