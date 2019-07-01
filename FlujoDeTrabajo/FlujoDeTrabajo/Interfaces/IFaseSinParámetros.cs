namespace FlujoDeTrabajo.Interfaces
{
    using Atributos;

    public interface IFaseSinParámetros<out T> : IFase where T : IEntidad
    {
        [MétodoDeFlujo]
        IResultado<T> Procesar();
    }
}
