namespace FlujoDeTrabajo.Nucelo
{
    using Interfaces;

    public class ConfiguraciónBase : IConfiguraciónDeFlujo
    {
        public IProcesadorDeAtributos ProcesadorDeAtributos { get; private set; }

        public ConfiguraciónBase()
        {
            ProcesadorDeAtributos = new ProcesadorDeAtributos();
        }
    }
}
