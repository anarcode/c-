namespace FasesDeEjemplo.Fases
{
    using Entidades;
    using FlujoDeTrabajo.Atributos;
    using FlujoDeTrabajo.Interfaces;
    using FlujoDeTrabajo.Nucelo;

    // Sería interesante que cada fase pudiera tener un [GestorDeError] al que le invoque si falla
    // y que decida él la lógica
    public class Autenticación : IFase
    {
        [MétodoDeFlujo(Criticidad.QueArdaTodoElFuegoPurifica)]
        public IResultado<Usuario> Procesar(string nombre)
        {
            return new Resultado<Usuario>(true, new Usuario { Id = 1, Nombre = nombre, Roles = { "ABC", "DEF" } });
        }
    }
}
