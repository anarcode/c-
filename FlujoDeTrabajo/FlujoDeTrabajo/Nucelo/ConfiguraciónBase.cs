namespace FlujoDeTrabajo.Nucelo
{
    using Interfaces;

    public class ConfiguraciónBase : IConfiguraciónDeFlujo
    {
        public IProcesadorDeParámetros ProcesadorDeParámetros { get; private set; }

        public IProcesadorDeCriticidad ProcesadorDeCriticidad { get; private set; }

        public IEjecutorDeFlujo EjecutorDeFlujo { get; private set; }

        public ConfiguraciónBase()
        {
            ProcesadorDeParámetros = new ProcesadorDeParámetros();
            ProcesadorDeCriticidad = new ProcesadorDeCriticidad();
            EjecutorDeFlujo = new EjecutorDeFlujo();
        }
    }
}
