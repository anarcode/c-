namespace FlujoDeTrabajo.Nucelo
{
    using Atributos;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Flujo
    {
        private IConfiguraciónDeFlujo _configuración;

        public virtual List<IFase> Fases { get; private set; }

        public Flujo(IConfiguraciónDeFlujo configuración)
        {
            _configuración = configuración;
            Fases = new List<IFase>();
        }

        public Flujo() : this(new ConfiguraciónBase())
        {
            _configuración.ProcesadorDeAtributos.EstablecerFlujo(this);
        }

        public void Ejecutar()
        {
            List<IResultado<IEntidad>> resultado = new List<IResultado<IEntidad>>();
            Fases.ForEach(f =>
            {
                object[] parámetros = null;

                var método = f.GetType().GetMethods()
                    .Where(m => {
                        var atributos = m.GetCustomAttributes(typeof(AtributoDeMétodoDeFlujo), true);
                        if (atributos.Length == 0)
                        {
                            return false;
                        }

                        parámetros = _configuración.ProcesadorDeAtributos.Procesar(atributos[0] as AtributoDeMétodoDeFlujo, m, resultado);
                        return true;
                    })
                    .First();

                if(método != null)
                {
                    // Aqui igual compensa meter un try, no????
                    var resultadoDeEjecución = método.Invoke(f, parámetros) as IResultado<IEntidad>;
                    resultado.Add(resultadoDeEjecución);
                }
                else
                {
                    resultado.Add(new Error<IEntidad>(null, $"No se ha encontrado el método {método.Name}"));
                }
            });
        }
    }
}
