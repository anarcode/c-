namespace FasesDeEjemplo.Fases
{
    using Entidades;
    using FlujoDeTrabajo.Atributos;
    using FlujoDeTrabajo.Interfaces;
    using FlujoDeTrabajo.Nucelo;

    public class Autorización : IFase
    {
        [MétodoDeFlujo]
        public IResultado<Usuario> Procesar(Usuario usuario)
        {
            var resultadoDeOperación = usuario.Id == 1;
            var instancia = resultadoDeOperación ? new Usuario { Id = usuario.Id, Nombre = usuario.Nombre } : null;
            if(instancia != null)
            {
                instancia.Roles.AddRange(new string[] { "ABC", "EFG" });
            }

            var resultado = new Resultado<Usuario>(resultadoDeOperación, instancia);
            return resultado;
        }
    }
}
