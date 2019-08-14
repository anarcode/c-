namespace FlujoDeTrabajo.Interfaces
{
    using Nucelo;
    using System;
    using System.Reflection;

    public interface IProcesadorDeCriticidad : IAsociableAFlujo
    {
        Error<IEntidad> Procesar(IFase fase, MethodInfo método, Error<IEntidad> error, Exception excepción);
    }
}
