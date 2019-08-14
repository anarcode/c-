namespace FlujoDeTrabajo.Nucelo
{
    using Atributos;
    using Excepciones;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    // Tengo que poder añadir flujos
    // Faltan observadores de eventos

    // Puedo crear un GestorDeFlujo que le inyecto al que lo quiera que permita el salto de fases, etc.
    // Tendré que poner métodos como SaltarFase(nDeFases), IrAFase<T>, Finalizar...

    // La idea es que cuando acabe un subflujo, se inserte un tipo ResutltadoDeSubflujo con su resultado interno
    // que lo podrá mirar el DatosDeEjecución
    public abstract class Flujo
    {
        // CUIDADO: Ahora los resultados van por flujo, asi que no hay uno general

        internal Flujo _Padre { get; set; }

        internal IConfiguraciónDeFlujo Configuración { get; private set; }

        public string Nombre { get; private set; }

        public virtual List<IFase> Fases { get; private set; }

        internal List<Flujo> Flujos { get; private set; }

        internal IFase FaseActual { get; set; }

        internal List<IResultado<IEntidad>> ResultadoActual { get; private set; }

        [DatoDeFlujo("gestorDeFlujo")]
        internal GestorDeFlujo GestorDeFlujo { get; private set; }

        [DatoDeFlujo("datosDeEjecución")]
        internal DatosDeEjecución DatosDeEjecución { get; private set; }

        public List<IObservadorDeFlujo> Observadores { get; private set; }

        public Flujo(string nombre, IConfiguraciónDeFlujo configuración)
        {
            Nombre = nombre;
            Configuración = configuración;
            Configuración.ProcesadorDeParámetros.EstablecerFlujo(this);
            Configuración.ProcesadorDeCriticidad.EstablecerFlujo(this);
            Configuración.EjecutorDeFlujo.EstablecerFlujo(this);

            Fases = new List<IFase>();
            Flujos = new List<Flujo>();
            Observadores = new List<IObservadorDeFlujo>();
            GestorDeFlujo = new GestorDeFlujo(this);
            ResultadoActual = new List<IResultado<IEntidad>>();
            DatosDeEjecución = new DatosDeEjecución();
        }

        public Flujo(string nombre) : this(nombre, new ConfiguraciónBase()) { }

        public void AñadirObservador(IObservadorDeFlujo observador)
        {
            if (!Observadores.Contains(observador))
            {
                Observadores.Add(observador);
            }
        }

        public void AñadirDefiniciónDeFlujo(Flujo flujo)
        {
            flujo._Padre = this;
            if(Flujos.Where(f => f.Nombre == flujo.Nombre).ToList().Count == 0)
            {
                Flujos.Add(flujo);
            }
        }

        public DatosDeEjecución Ejecutar()
        {
            ResultadoActual = new List<IResultado<IEntidad>>();
            List<ResultadoDeEjecuciónDeFase<IEntidad>> resultadosInternos = new List<ResultadoDeEjecuciónDeFase<IEntidad>>();
            DatosDeEjecución = new DatosDeEjecución();

            try
            {
                Configuración.EjecutorDeFlujo.Ejecutar();
                return DatosDeEjecución;
            }
            catch(QuemarloTodo excepción)
            {
                throw new ExcepciónDeFlujo(excepción.Error, $"No se ha podido completar el flujo {this.GetType().Name}", excepción);
            }
        }
    }
}
