namespace FlujoDeTrabajo.Nucelo.ComandosDelGestorDeFlujo
{
    using Interfaces;

    internal abstract class ComandoBase: IComando
    {
        protected Flujo Flujo { get; private set; }

        protected ComandoBase(Flujo flujo)
        {
            Flujo = flujo;
        }

        public abstract void Ejecutar();
    }
}
