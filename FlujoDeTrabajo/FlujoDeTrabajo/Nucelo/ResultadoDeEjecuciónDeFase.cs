namespace FlujoDeTrabajo.Nucelo
{
    using Interfaces;
    using System;

    internal class ResultadoDeEjecuciónDeFase<T> where T : IEntidad
    {
        public IResultado<T> Resultado { get; private set; }

        public Type TipoDeFase { get; private set; }

        public ResultadoDeEjecuciónDeFase(IResultado<T> resultado, Type tipoDeFase)
        {
            Resultado = resultado;
            TipoDeFase = tipoDeFase;
        }
    }
}
