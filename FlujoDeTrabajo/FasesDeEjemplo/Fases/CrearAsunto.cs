namespace FasesDeEjemplo.Fases
{
    using Entidades;
    using FlujoDeTrabajo.Atributos;
    using FlujoDeTrabajo.Interfaces;
    using FlujoDeTrabajo.Nucelo;

    public class CrearAsunto : IFase
    {
        [MétodoDeFlujo]
        public IResultado<Asunto> Procesar(Usuario usuario, DatosDeEjecución datosDeEjecución)
        {
            return new Resultado<Asunto>(true, new Asunto(usuario.Id));
        }
    }
}
