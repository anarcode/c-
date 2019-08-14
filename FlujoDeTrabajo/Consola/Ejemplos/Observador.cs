namespace Consola.Ejemplos
{
    using System;
    using FlujoDeTrabajo.Nucelo;
    using FlujoDeTrabajo.Atributos;
    using FasesDeEjemplo.Fases;
    using FlujoDeTrabajo.Interfaces;
    using FasesDeEjemplo.Flujos;

    internal class Observador : ObservadorDeFlujo
    {
        FlujoDePrueba _flujo;

        public Observador(FlujoDePrueba flujo)
        {
            _flujo = flujo;
        }

        public override void AntesDeEjecutarFase(object instanciaDeFase, Type tipoDeFase, object[] parámetros)
        {
            base.AntesDeEjecutarFase(instanciaDeFase, tipoDeFase, parámetros);
        }

        // Estaría guay que le llegasen los DatosDeEjecución
        [ErrorAvisable]
        public Error<IEntidad> ErrorAvisable(Autorización fase, Error<IEntidad> error)
        {
            return new Error<IEntidad>(null, "Avisado queda", error);
        }
    }
}
