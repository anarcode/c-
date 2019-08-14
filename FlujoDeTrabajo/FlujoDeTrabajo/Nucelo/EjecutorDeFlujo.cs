namespace FlujoDeTrabajo.Nucelo
{
    using Atributos;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class EjecutorDeFlujo : IEjecutorDeFlujo
    {
        Flujo _flujo;
        // No me vale sólo con la fase, necesito el flujo para saber sobre que instancia se sacan los datos
        Stack<IFase> _fasesDeEjecución;

        internal EjecutorDeFlujo()
        {
            _fasesDeEjecución = new Stack<IFase>();
        }

        public void EstablecerFlujo(Flujo flujo)
        {
            _flujo = flujo;
        }

        public DatosDeEjecución Ejecutar()
        {
            // Lo mejor es montar una pila de fases e ir desapilando y ejecutando
            // Así puedo cargar acciones de submétodos apilando sin perder el control
            // Además me interesa un puntero a fase en cada Flujo para poder volver tras una subejecución
            List<ResultadoDeEjecuciónDeFase<IEntidad>> resultadosInternos = new List<ResultadoDeEjecuciónDeFase<IEntidad>>();

            var nuevaLista = new List<IFase>(_flujo.Fases);
            nuevaLista.Reverse();

            // REVERSES???
            nuevaLista.ForEach(f => _fasesDeEjecución.Push(f));
            _flujo.GestorDeFlujo.EstablecerColaDeEjecución(_fasesDeEjecución);

            while(_fasesDeEjecución.Count > 0)
            {
                IFase fase = _fasesDeEjecución.Pop();
                _flujo.FaseActual = fase;
                object[] parámetros = null;
                bool errorInsertado = false;

                var método = fase.GetType().GetMethods()
                    .Where(m => {
                        var atributos = m.GetCustomAttributes(typeof(AtributoDeMétodoDeFlujo), true);
                        if (atributos.Length == 0)
                        {
                            return false;
                        }
                        
                        try
                        {
                            // El procesador de parámetros tendrá en cuenta el flujo actual y el padre recursivamente
                            parámetros = _flujo.Configuración.ProcesadorDeParámetros.Procesar(atributos[0] as AtributoDeMétodoDeFlujo, m, _flujo.ResultadoActual);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            var error = new Error<IEntidad>(null, $"No se han podido obtener los parámetros del método {m.Name} (declarado como MétodoDeFlujo) de la clase {fase.GetType().Name}: {ex.Message}");
                            error = _flujo.Configuración.ProcesadorDeCriticidad.Procesar(fase, m, error, ex);
                            _flujo.ResultadoActual.Add(error);
                            errorInsertado = true;
                            return false;
                        }
                    })
                    .First();

                if (método == null)
                {
                    if (!errorInsertado)
                    {
                        _flujo.ResultadoActual.Add(new Error<IEntidad>(null, $"No se ha encontrado ningún método de flujo válido en la clase {fase.GetType().Name}"));
                        errorInsertado = true;
                    }
                    continue;
                }

                _flujo.Observadores.ForEach(o => o.AntesDeEjecutarFase(fase, fase.GetType(), parámetros));

                try
                {
                    var resultadoDeEjecución = método.Invoke(fase, parámetros) as IResultado<IEntidad>;
                    _flujo.Observadores.ForEach(o => o.DespuésDeEjecutarFase(fase, fase.GetType(), parámetros, resultadoDeEjecución));
                    _flujo.ResultadoActual.Add(resultadoDeEjecución);
                    _flujo.DatosDeEjecución.AñadirResultado(new ResultadoDeEjecuciónDeFase<IEntidad>(resultadoDeEjecución, fase.GetType()));

                    // Por si hay saltos, etc...
                    _flujo.GestorDeFlujo.EjecutarAcción();

                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    // AQUÍ DEBERÍA ACABAR LA CLASE
                    /////////////////////////////////////////////////////////////////////////////////////////////////

                }
                // Como es una invocación a un método externo, me interesa que sea genérico, aunque huela
                // Si la fase implementa un método de captura de errores lo llamo desde aquí
                catch (Exception ex)
                {
                    var error = new Error<IEntidad>(null, $"Error al invocar al método {método.Name}", new Error<IEntidad>(null, ex.InnerException.Message), parámetros);
                    _flujo.Configuración.ProcesadorDeCriticidad.Procesar(fase, método, error, ex.InnerException);
                }
            }

            return _flujo.DatosDeEjecución;
        }
    }
}
