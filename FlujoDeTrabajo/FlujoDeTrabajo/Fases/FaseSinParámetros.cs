namespace FlujoDeTrabajo.Fases
{
    using Atributos;
    using Interfaces;

    public abstract class FaseSinParámetros<T> : IFaseSinParámetros<T> where T : IEntidad
    {
        [MétodoDeFlujo]
        public abstract IResultado<T> Procesar();
    }
}
