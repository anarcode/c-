namespace FlujoDeTrabajo.Nucelo
{
    using ComandosDelGestorDeFlujo;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    // Si los flujos tienen nombre, puedo ir haciendo saltos entre flujos sin problema
    // (si consigo componer un flujo con todos que sea capaz de gestionarlo).
    // Entre saltos se podría acordar de donde se había quedad en el último y continuar desde ahí
    public sealed class GestorDeFlujo
    {
        Flujo _flujo;
        Stack<IFase> _colaDeEjecución;
        IComando _comando;

        internal GestorDeFlujo(Flujo flujo)
        {
            _flujo = flujo;
        }

        public void Finalizar()
        {
            // Aqui encolo
        }

        internal void EstablecerColaDeEjecución(Stack<IFase> colaDeEjecución)
        {
            _colaDeEjecución = colaDeEjecución;
        }

        internal void EjecutarAcción()
        {
            if(_comando != null)
            {
                // En según depende la acción pues hago cosas
                // Por ejemplo, si necesitan ejecutar un subflujo, desde aqui puedo encolar las acciones del subflujo en cuestión
                _comando.Ejecutar();
                _comando = null;
            }
        }

        public void AñadirFlujo(string nombre)
        {
            // Ahora busco en los flujos a ver cual se llama como el que quiero y cargo sus fases (no hay problema xq la ejecución es stateless)
            var flujosCoincidentes = _flujo.Flujos.Where(f => f.Nombre == nombre).ToList();
            if(flujosCoincidentes.Count == 0)
            {
                // ERROR
                return;
            }
            _comando = new AñadirFlujo(_flujo, _colaDeEjecución, nombre);
        }
        // SaltarFase, Finalizar, . Encolo la acción y desde el flujo la leo y ejecuto
    }
}
