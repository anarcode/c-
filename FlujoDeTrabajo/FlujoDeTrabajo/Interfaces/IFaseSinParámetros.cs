namespace FlujoDeTrabajo.Interfaces
{
    public interface IFaseSinParámetros<out T> : IFase where T : IEntidad
    {
        IResultado<T> Procesar();
    }
}
