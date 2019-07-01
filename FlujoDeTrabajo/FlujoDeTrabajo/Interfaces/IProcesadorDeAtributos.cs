namespace FlujoDeTrabajo.Interfaces
{
    using Atributos;
    using Nucelo;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IProcesadorDeAtributos
    {
        void EstablecerFlujo(Flujo flujo);

        object[] Procesar(AtributoDeMétodoDeFlujo atributo, MethodInfo método, List<IResultado<IEntidad>> resultados);
    }
}
