namespace FlujoDeTrabajo.Fases
{
    using Interfaces;

    public abstract class FaseSinParámetros<T> : IFaseSinParámetros<T> where T : IEntidad
    {
        public abstract IResultado<T> Procesar();
    }
}
