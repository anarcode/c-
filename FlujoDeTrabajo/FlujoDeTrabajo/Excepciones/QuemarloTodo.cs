namespace FlujoDeTrabajo.Excepciones
{
    using Interfaces;
    using Nucelo;
    using System;

    // Si esta es internal evito que la capturen desde fuera
    // Tengo ExcepciónDeFlujo que es el que lanzo
    internal class QuemarloTodo : Exception
    {
        public Error<IEntidad> Error { get; private set; }

        public QuemarloTodo(Error<IEntidad> error, string mensaje, Exception excepción) : base(mensaje, excepción)
        {
            Error = error;
        }

        //public QuemarloTodo(Error<IEntidad> error) : this(error, string.Empty, null) { }
        public QuemarloTodo(Error<IEntidad> error, string mensaje) : this(error, mensaje, null) { }
    }
}
