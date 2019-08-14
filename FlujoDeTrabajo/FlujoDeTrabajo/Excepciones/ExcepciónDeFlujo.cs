namespace FlujoDeTrabajo.Excepciones
{
    using Interfaces;
    using Nucelo;
    using System;

    // Si esta es internal evito que la capturen desde fuera
    // Debería crear un ExcepciónDeFlujo que sea la que lance
    internal class ExcepciónDeFlujo : Exception
    {
        public Error<IEntidad> Error { get; private set; }

        public ExcepciónDeFlujo(Error<IEntidad> error, string mensaje, Exception excepción) : base(mensaje, excepción)
        {
            Error = error;
        }
    }
}
