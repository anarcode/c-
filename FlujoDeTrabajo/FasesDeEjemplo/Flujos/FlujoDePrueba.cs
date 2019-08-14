namespace FasesDeEjemplo.Flujos
{
    using Fases;
    using FlujoDeTrabajo.Atributos;
    using FlujoDeTrabajo.Interfaces;
    using FlujoDeTrabajo.Nucelo;
    using System.Collections.Generic;
    using Flujos = Nucelo.Constantes.Flujos;

    public sealed class FlujoDePrueba : Flujo
    {
        private readonly List<IFase> _fases;

        [DatoDeFlujo("nombre")]
        public string NombreDeUsuario { get; set; }

        [DatoDeFlujo("rolesPermitidos")]
        public List<string> Roles { get; set; }

        public override List<IFase> Fases => _fases;

        public FlujoDePrueba(string nombre) : base(nombre)
        {
            _fases = new List<IFase>
            {
                new Autenticación(),
                new Autorización(),
                new SubFlujo(Flujos.CREAR_ASUNTO)
                //new CrearAsunto() // Y si Flujo : IFase y puedo meter aqui new FlujoDeCreaciónDeAsunto(Flujos.CREAR_ASUNTO)
                // Desde el ejecutor puedo mirar si es un flujo
                // MEJOR: Una fase que se encargue de ejecutar un flujo => new SubFlujo()
            };
            Roles = new List<string>();
            AñadirDefiniciónDeFlujo(new FlujoDeCreaciónDeAsunto(Flujos.CREAR_ASUNTO));

            // Observadores.Add(new Observador(this));
        }
    }
}
