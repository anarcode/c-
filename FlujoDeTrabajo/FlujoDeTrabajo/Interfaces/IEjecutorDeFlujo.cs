namespace FlujoDeTrabajo.Interfaces
{
    using Nucelo;

    public interface IEjecutorDeFlujo : IAsociableAFlujo
    {
        DatosDeEjecución Ejecutar();
    }
}
