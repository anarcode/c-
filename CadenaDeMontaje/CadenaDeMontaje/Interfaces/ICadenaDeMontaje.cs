namespace CadenaDeMontaje.Interfaces
{
    public interface ICadenaDeMontaje<T>
    {
        bool Completada { get; }

        bool ProductoListo { get; }

        string Estado { get; }

        T ProductoMontado { get; }

        void SiguientePaso();
    }
}