namespace FasesDeEjemplo.Ejemplos
{
    using Fases;
    using FlujoDeTrabajo.Atributos;
    using FlujoDeTrabajo.Interfaces;
    using FlujoDeTrabajo.Nucelo;
    using Flujos;

    internal class Observador : ObservadorDeFlujo
    {
        FlujoDePrueba _flujo;

        public Observador(FlujoDePrueba flujo)
        {
            _flujo = flujo;
        }

        [ErrorAvisable]
        public Error<IEntidad> ErrorAvisable(Autorización fase, Error<IEntidad> error)
        {
            return new Error<IEntidad>(null, "Avisado queda", error);
        }
    }
}
