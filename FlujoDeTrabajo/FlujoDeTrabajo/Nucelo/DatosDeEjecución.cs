namespace FlujoDeTrabajo.Nucelo
{
    using Entidades;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class DatosDeEjecución // Algo de IConfiguración devuelve esto??? Porque hay métodos internos... y sobreescribir es complicado
    {
        private List<ResultadoDeEjecuciónDeFase<IEntidad>> _resultados;

        internal DatosDeEjecución()
        {
            _resultados = new List<ResultadoDeEjecuciónDeFase<IEntidad>>();
        }

        internal void AñadirResultado(ResultadoDeEjecuciónDeFase<IEntidad> resultado)
        {
            _resultados.Add(resultado);
        }

        // Necesito una forma de traerme los parametros de entrada de una fase
        public List<IResultado<P>> ObtenerPorFase<T, P>() where T : IFase where P : class, IEntidad
        {
            var resultadosPorInstancia = _resultados.Where(r => r.TipoDeFase == typeof(T)).ToList();
            if (resultadosPorInstancia.Count == 0)
            {
                // ERROR
                return null;
            }
            return resultadosPorInstancia.Select(r => r.Resultado as IResultado<P>).ToList();
        }

        public T ObtenerÚltimo<T>() where T : class, IEntidad
        {
            return ObtenerPorTipo<T>().Last();
        }

        public ICollection<T> ObtenerPorTipo<T>() where T : class, IEntidad
        {
            var resultadosPorInstancia = _resultados.Where(r => r.Resultado.ObtenerInstancia().GetType() == typeof(T)).ToList();
            if (resultadosPorInstancia.Count == 0)
            {
                // ERROR
                return null;
            }

            return resultadosPorInstancia.Select(r => r.Resultado.ObtenerInstancia() as T).ToList();
        }

        public DatosDeEjecución ObtenerPorFlujo(string nombre)
        {
            // Busco el primer EjecuciónDeFlujo con el nombre que me mandan
            // El siguiente FinalizarSubFlujo contiene todos los resultados
            
            bool encontrado = false;
            int i = -1;
            while(!encontrado && i++ <= _resultados.Count)
            {
                var resultadoActual = _resultados[i];
                if (typeof(SubFlujo).IsAssignableFrom(resultadoActual.Resultado.ObtenerInstancia().GetType()))
                {
                    SubFlujo subFlujo = resultadoActual.Resultado.ObtenerInstancia() as SubFlujo;
                    if (subFlujo.Nombre == nombre)
                    {
                        encontrado = true;
                    }
                }
            }

            var resultado = new DatosDeEjecución();
            if (encontrado)
            {
                encontrado = false;
                int subFlujosEncontrados = 0;
                while (!encontrado && ++i < _resultados.Count)
                {
                    // Cuidado con los resultados apilados
                    // Si encuentro más aperturas tengo que tenerlas en cuenta a la hora de los cierres
                    var resultadoActual = _resultados[i];

                    if (typeof(SubFlujo).IsAssignableFrom(resultadoActual.Resultado.ObtenerInstancia().GetType()))
                    {
                        subFlujosEncontrados++;
                    }

                    if (typeof(EjecuciónDeFlujo).IsAssignableFrom(resultadoActual.Resultado.ObtenerInstancia().GetType()))
                    {
                        if(subFlujosEncontrados == 0)
                        {
                            EjecuciónDeFlujo ejecución = resultadoActual.Resultado.ObtenerInstancia() as EjecuciónDeFlujo;
                            ejecución.DatosDeEjecución.VolcarDatos(resultado);
                            encontrado = true;
                        }
                        else
                        {
                            subFlujosEncontrados--;
                        }
                    }
                }
            }

            return resultado;
        }

        // ObtenerErrores, ResultadosDeEjecución (para ver lo que ha ido pasando)

        internal void VolcarDatos(DatosDeEjecución destinatario)
        {
            foreach(var resultado in _resultados)
            {
                destinatario._resultados.Add(resultado);
            }
        }
    }
}
