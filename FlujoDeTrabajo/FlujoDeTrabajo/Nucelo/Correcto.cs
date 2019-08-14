namespace FlujoDeTrabajo.Nucelo
{
    using Interfaces;

    public class Correcto<T> : Resultado<T> where T : IEntidad
    {
        // IFase??

        public Error<T> ErrorInterno { get; private set; }

        public object[] ParámetrosDeEntrada { get; set; }

        public Correcto(T instancia, string mensaje) : base(true, instancia)
        {
            Mensaje = mensaje;
        }

        public Correcto(T instancia) : this(instancia, string.Empty) { }
    }
}
