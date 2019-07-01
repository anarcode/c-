namespace FlujoDeTrabajo.Nucelo
{
    using Atributos;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ProcesadorDeAtributos : IProcesadorDeAtributos
    {
        private Flujo _flujo;

        public delegate object[] ObtenerParámetros(MethodInfo método, List<IResultado<IEntidad>> resultados);

        protected Dictionary<Type, ObtenerParámetros> Tipos { get; private set; }

        private object[] ObtenerParámetrosDeMétodoDeFlujoBase(MethodInfo método, List<IResultado<IEntidad>> resultados)
        {
            var listaDeParámetros = new List<object>();
            foreach (var parámetro in método.GetParameters())
            {
                var lista = resultados.Where(r => r.Instancia().GetType() == parámetro.ParameterType).ToList();
                object objeto = null;
                if(lista.Count == 0)
                {
                    var propiedades = _flujo.GetType().GetProperties();
                    if(propiedades.Length > 0)
                    {
                        var propiedadesDeFlujo = propiedades.Where(p =>
                        {
                            var atributosDatosDeFlujo = p.GetCustomAttribute(typeof(DatoDeFlujo)) as DatoDeFlujo;
                            if (atributosDatosDeFlujo != null)
                            {
                                return atributosDatosDeFlujo.NombreDeParámetro == parámetro.Name;
                            }
                            return false;
                        })
                        .ToList();

                        if(propiedadesDeFlujo.Count > 0)
                        {
                            objeto = propiedadesDeFlujo[0].GetValue(_flujo);
                        }
                    }
                }
                else
                {
                    objeto = lista[0].Instancia();
                }

                if (objeto == null)
                {
                    throw new InvalidOperationException($"No se ha encontrado ningún valor del tipo solicitado: {parámetro.ParameterType}");
                }
                listaDeParámetros.Add(objeto);
            }

            return listaDeParámetros.ToArray();
        }

        public ProcesadorDeAtributos()
        {
            Tipos = new Dictionary<Type, ObtenerParámetros>
            {
                { typeof(MétodoDeFlujo), ObtenerParámetrosDeMétodoDeFlujoBase }
            };
        }

        public void EstablecerFlujo(Flujo flujo)
        {
            _flujo = flujo;
        }

        public object[] Procesar(AtributoDeMétodoDeFlujo atributo, MethodInfo método, List<IResultado<IEntidad>> resultados)
        {
            if (Tipos.ContainsKey(atributo.GetType()))
            {
                return Tipos[atributo.GetType()](método, resultados);
            }
            return null;
        }
    }
}
