namespace FlujoDeTrabajo.Nucelo
{
    using System;
    using Interfaces;

    public class Resultado<T> : IResultado<T> where T : IEntidad
    {
        private T _instancia;

        public bool OperaciónCorrecta { get; private set; }

        public string Mensaje { get; protected set; }

        public Resultado(bool resultado, T instancia)
        {
            OperaciónCorrecta = resultado;
            _instancia = instancia;
        }

        public T Instancia() => _instancia;
    }
}
