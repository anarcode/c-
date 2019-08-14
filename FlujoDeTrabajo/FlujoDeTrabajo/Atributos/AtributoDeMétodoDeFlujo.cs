namespace FlujoDeTrabajo.Atributos
{
    using Nucelo;
    using System;

    public abstract class AtributoDeMétodoDeFlujo : Attribute
    {
        public Criticidad Criticidad { get; private set; }

        protected AtributoDeMétodoDeFlujo(Criticidad criticidad)
        {
            Criticidad = criticidad;
        }

        protected AtributoDeMétodoDeFlujo() : this(Criticidad.ElCambioClimáticoYaEstáAquí) { }
    }
}
