namespace FlujoDeTrabajo.Nucelo
{
    using Entidades;

    internal class ResultadoDeEjecuciónDeFlujo : Resultado<EjecuciónDeFlujo>
    {
        public ResultadoDeEjecuciónDeFlujo(bool resultado, EjecuciónDeFlujo instancia) : base(resultado, instancia)
        {
        }
    }
}
