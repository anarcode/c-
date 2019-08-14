namespace FlujoDeTrabajo.Interfaces
{
    public interface IConfiguraciónDeFlujo
    {
        IProcesadorDeParámetros ProcesadorDeParámetros { get; }

        IProcesadorDeCriticidad ProcesadorDeCriticidad { get; }

        IEjecutorDeFlujo EjecutorDeFlujo { get; }
    }
}
