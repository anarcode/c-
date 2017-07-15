namespace CadenaDeMontaje.Core.Interfaces
{
    public interface IConjuntoDeComponentes
    {
        string Estado { get; }

        IConjuntoDeComponentes AgregarComponente(IComponente componente);
    }
}