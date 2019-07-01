namespace Consola.Ejemplos
{
    using FasesDeEjemplo.Nucelo;

    public class Primero
    {
        public Primero()
        {
            var flujo = new FlujoDePrueba()
            {
                NombreDeUsuario = "Josito Ortóptero"
            };

            flujo.Ejecutar();
        }
    }
}
