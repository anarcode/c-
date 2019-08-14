namespace FlujoDeTrabajo.Nucelo
{
    using Atributos;
    using Interfaces;

    public class SubFlujo : IFase, ISubFlujo
    {
        public string Nombre { get; private set; }

        public SubFlujo(string nombre)
        {
            Nombre = nombre;
        }

        [MétodoDeFlujo(Criticidad.AvisarUnPoco)]
        public IResultado<ISubFlujo> Procesar(GestorDeFlujo gestorDeFlujo)
        {
            // Aqui tendré que hacer cosas para apilar el subflujo ed alguna forma
            gestorDeFlujo.AñadirFlujo(Nombre);
            return new Correcto<ISubFlujo>(this);
        }
    }
}
