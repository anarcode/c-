namespace FlujoDeTrabajo.Interfaces
{
    using Nucelo;
    using System;

    public interface IObservadorDeFlujo
    {
        void AntesDeEjecutarFase(object instanciaDeFase, Type tipoDeFase, object[] parámetros);

        void DespuésDeEjecutarFase(object instanciaDeFase, Type tipoDeFase, object[] parámetros, object resultado);

        void ErrorCrítico();

        void ErrorAvisable(Error<IEntidad> error);
    }
}
