namespace FlujoDeTrabajo.Nucelo
{
    using Interfaces;

    public class Error<T> : Resultado<T> where T : IEntidad
    {
        // IFase??

        public Error<T> ErrorInterno { get; private set; }

        public object[] ParámetrosDeEntrada { get; set; }

        public Error(T instancia, string mensaje, Error<T> errorInterno, object[] parámetrosDeEntrada) : base(false, instancia)
        {
            Mensaje = mensaje;
            ErrorInterno = errorInterno;
            ParámetrosDeEntrada = parámetrosDeEntrada;
        }

        public Error(T instancia, string mensaje, Error<T> errorInterno) : this(instancia, mensaje, errorInterno, null) { }

        public Error(T instancia, string mensaje) : this(instancia, mensaje, null) { }
    }
}
