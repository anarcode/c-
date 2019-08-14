namespace FlujoDeTrabajo.Interfaces
{
    using Atributos;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IProcesadorDeParámetros : IAsociableAFlujo
    {
        object[] Procesar(AtributoDeMétodoDeFlujo atributo, MethodInfo método, List<IResultado<IEntidad>> resultados);
    }
}
