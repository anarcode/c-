namespace FlujoDeTrabajo.Entidades
{
    using Interfaces;
    using Nucelo;

    public class EjecuciónDeFlujo : IEntidad
    {
        public DatosDeEjecución DatosDeEjecución { get; private set; }

        public EjecuciónDeFlujo()
        {
            DatosDeEjecución = new DatosDeEjecución();
        }

        public EjecuciónDeFlujo(DatosDeEjecución datosDeEjecución)
        {
            DatosDeEjecución = datosDeEjecución;
        }
    }
}
