namespace FlujoDeTrabajo.Interfaces
{
    public interface IResultado<out T> where T : IEntidad
    {
        bool OperaciónCorrecta { get; }

        T Instancia();

        string Mensaje { get; }
    }
}
