namespace FasesDeEjemplo.Entidades
{
    using FlujoDeTrabajo.Interfaces;

    public class Asunto : IEntidad
    {
        public int IdUsuario { get; private set; }

        public Asunto(int idUsuario)
        {
            IdUsuario = idUsuario;
        }
    }
}
