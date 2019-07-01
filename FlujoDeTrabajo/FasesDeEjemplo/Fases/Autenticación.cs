namespace FasesDeEjemplo.Fases
{
    using Entidades;
    using FlujoDeTrabajo.Fases;
    using FlujoDeTrabajo.Interfaces;
    using FlujoDeTrabajo.Nucelo;

    public class Autenticación : FaseSinParámetros<Usuario>
    {
        public override IResultado<Usuario> Procesar()
        {
            return new Resultado<Usuario>(true, new Usuario { Id = 1, Nombre = "Pamplinas" });
        }
    }
}
