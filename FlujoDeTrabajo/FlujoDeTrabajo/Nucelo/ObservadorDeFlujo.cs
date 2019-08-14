namespace FlujoDeTrabajo.Nucelo
{
    using System;
    using Interfaces;

    public class ObservadorDeFlujo : IObservadorDeFlujo
    {
        public virtual void AntesDeEjecutarFase(object instanciaDeFase, Type tipoDeFase, object[] parámetros) { }

        public virtual void DespuésDeEjecutarFase(object instanciaDeFase, Type tipoDeFase, object[] parámetros, object resultado) { }

        public virtual void ErrorCrítico() { }

        public virtual void ErrorAvisable(Error<IEntidad> error) { }
    }
}
