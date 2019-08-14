namespace FlujoDeTrabajo.Nucelo
{
    using System.Reflection;
    using Interfaces;
    using Atributos;
    using System.Collections.Generic;
    using Excepciones;
    using System.Linq;
    using System;

    // Con un Visitor podría crear distintos tipos de error???
    internal sealed class ProcesadorDeCriticidad : IProcesadorDeCriticidad
    {
        private delegate Error<IEntidad> ProcesarCriticidad(IFase fase, Error<IEntidad> error, Exception excepción);
        private readonly Dictionary<Criticidad, ProcesarCriticidad> _mapeo;
        private Flujo _flujo;

        private Error<IEntidad> QuemarloTodo(IFase fase, Error<IEntidad> error, Exception excepción)
        {
            throw new QuemarloTodo(error, error.Mensaje, excepción);
        }

        private Error<IEntidad> AvisarUnPoco(IFase fase, Error<IEntidad> error, Exception excepción)
        {
            // Si es avisable, tengo que buscar un método (de los observadores) con el atributo y el tipo de fase
            // si existe, lo invoco
            _flujo.Observadores.ForEach(o =>
            {
                o.GetType().GetMethods().ToList().ForEach(m =>
                {
                    var atributo = m.GetCustomAttribute(typeof(ErrorAvisable), true) as ErrorAvisable;
                    if (atributo == null)
                    {
                        // ERROR
                        return;
                    }

                    // Si creo un delegado con la firma que quiero, puedo hacer el cast y si falla genero un error diciendo que no es válido??
                    // Nopes, porque no resuleve bien el cast de IEntidad al tipo específico
                    // Igual puedo yo mirar los tipos...

                    // Tengo que mirar los parámetros
                    // SIEMPRE: El primero IFase, el segundo Error. Si hay tercero y es datosDeEjecución, se lo enchufo
                    
                    Type tipoDeFase = m.GetParameters()[0].ParameterType;
                    if (tipoDeFase == fase.GetType())
                    {
                        var parámetros = new List<object> { fase, error };
                        if(m.GetParameters().Length == 3 && m.GetParameters()[0].ParameterType == typeof(DatosDeEjecución))
                        {
                            parámetros.Add(_flujo.DatosDeEjecución);
                        }

                        error = m.Invoke(o, parámetros.ToArray()) as Error<IEntidad>;
                        return;
                    }
                });
           });

            _flujo.Observadores.ForEach(o => o.ErrorAvisable(error));
            return ElCambioClimáticoYaEstáAquí(fase, error, excepción);
        }

        private Error<IEntidad> ElCambioClimáticoYaEstáAquí(IFase fase, Error<IEntidad> error, Exception excepción)
        {
            return new Error<IEntidad>(null, "Se ha producido un error pero parece que no es importante...", error);
        }

        public ProcesadorDeCriticidad()
        {
            _mapeo = new Dictionary<Criticidad, ProcesarCriticidad>
            {
                { Criticidad.QueArdaTodoElFuegoPurifica, QuemarloTodo },
                { Criticidad.AvisarUnPoco, AvisarUnPoco},
                { Criticidad.ElCambioClimáticoYaEstáAquí, ElCambioClimáticoYaEstáAquí }
            };
        }

        public void EstablecerFlujo(Flujo flujo)
        {
            _flujo = flujo;
        }

        public Error<IEntidad> Procesar(IFase fase, MethodInfo método, Error<IEntidad> error, Exception excepción)
        {
            var atributo = método.GetCustomAttribute(typeof(MétodoDeFlujo)) as MétodoDeFlujo;
            if(atributo == null)
            {
                throw new QuemarloTodo(error, $"El método {método.Name} de la clase {fase.GetType().Name} no es un método de flujo.");
            }

            return _mapeo[atributo.Criticidad](fase, error, excepción);
        }
    }
}
