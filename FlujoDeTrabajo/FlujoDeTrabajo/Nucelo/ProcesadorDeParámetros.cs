namespace FlujoDeTrabajo.Nucelo
{
    using Atributos;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal sealed class ProcesadorDeParámetros : IProcesadorDeParámetros
    {
        private Flujo _flujo;

        public delegate object[] ObtenerParámetros(Flujo flujo, MethodInfo método);

        private Dictionary<Type, ObtenerParámetros> Tipos { get; set; }

        // Voy a necesitar un diccionario de resultados por flujo o similar
        private object[] ObtenerParámetrosDeMétodoDeFlujoBase(Flujo flujo, MethodInfo método)
        {
            var listaDeParámetros = new List<object>();
            foreach (var parámetro in método.GetParameters())
            {
                var lista = flujo.ResultadoActual.Where(r => parámetro.ParameterType == r.ObtenerInstancia().GetType()).ToList();
                object objeto = null;
                if(lista.Count > 0)
                {
                    objeto = lista.Last().ObtenerInstancia();
                }
                else
                {
                    var propiedades = flujo.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    if (propiedades.Length > 0)
                    {
                        var propiedadesDeFlujo = propiedades.Where(p =>
                        {
                            var atributosDatosDeFlujo = p.GetCustomAttribute(typeof(DatoDeFlujo), true) as DatoDeFlujo;
                            if (atributosDatosDeFlujo != null)
                            {
                                return atributosDatosDeFlujo.NombreDeParámetro == parámetro.Name;
                            }
                            return false;
                        })
                        .ToList();

                        if (propiedadesDeFlujo.Count > 0)
                        {
                            objeto = propiedadesDeFlujo[0].GetValue(flujo);
                        }

                        if(objeto == null)
                        {
                            // Me toca mirar en el padre del flujo recursivamente, al final sin o hay na, pues na
                            if (flujo._Padre != null)
                            {
                                var recursivo = ObtenerParámetrosDeMétodoDeFlujoBase(_flujo._Padre, método);
                                if(recursivo.Length > 0)
                                {
                                    var parámetrosDelMismoTipo = recursivo.Where(r => r.GetType() == parámetro.ParameterType).ToList();
                                    if(parámetrosDelMismoTipo.Count > 0)
                                    {
                                        objeto = recursivo.Where(r => r.GetType() == parámetro.ParameterType).First();
                                    }
                                    
                                }
                            }
                        }
                    }
                }

                if (objeto == null)
                {
                    throw new InvalidOperationException($"No se ha encontrado ningún valor del tipo solicitado: {parámetro.ParameterType}");
                }
                listaDeParámetros.Add(objeto);
            }

            return listaDeParámetros.ToArray();
        }

        public ProcesadorDeParámetros()
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
                return Tipos[atributo.GetType()](_flujo, método);
            }
            return null;
        }
    }
}
