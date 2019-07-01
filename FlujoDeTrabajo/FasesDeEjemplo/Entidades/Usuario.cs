namespace FasesDeEjemplo.Entidades
{
    using FlujoDeTrabajo.Interfaces;
    using System.Collections.Generic;

    public class Usuario : IEntidad
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<string> Roles { get; set; }

        public Usuario()
        {
            Roles = new List<string>();
        }
    }
}
