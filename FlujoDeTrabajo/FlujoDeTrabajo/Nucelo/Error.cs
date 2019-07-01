namespace FlujoDeTrabajo.Nucelo
{
    using Interfaces;

    public class Error<T> : Resultado<T> where T : IEntidad
    {
        public Error(T instancia, string mensaje) : base(false, instancia)
        {
            Mensaje = mensaje;
        }
    }
}
