using CadenaDeMontaje.Interfaces;
using CadenaDeMontaje.PasosDeCadena;
using CadenaDeMontaje.Productos;

namespace CadenaDeMontaje.CadenasDeMontaje
{
    public class CadenaDeMontajeDeCoche : ICadenaDeMontaje<Coche>
    {
        IConfiguraciónDeCoche _configuración;

        IPasoDeCadena _pasoActual;

        Coche _cocheEnMontaje;

        int _cochesMontados;

        bool _pendienteDePasoFinal;

        public bool Completada { get; private set; }

        public bool ProductoListo { get; private set; }

        public string Estado { get; private set; }

        public int TotalCoches { get; set; }

        public Coche ProductoMontado { get; private set; }

        public CadenaDeMontajeDeCoche(IConfiguraciónDeCoche configuracióndeCoche)
        {
            _cochesMontados = 0;
            _configuración = configuracióndeCoche;
            _cocheEnMontaje = new Coche();
            _pasoActual = new AñadirChasis(_configuración, _cocheEnMontaje);
            Estado = "Estoy listo estoy listo estoy listo";
            TotalCoches = 1;
        }

        public void SiguientePaso()
        {
            ProductoListo = false;

            if (_pendienteDePasoFinal)
            {
                _pasoActual.EjecutarPaso();
                Estado = _pasoActual.Estado;
                Completada = true;

                ProductoMontado = null;
                _cocheEnMontaje = null;
                _pendienteDePasoFinal = false;
                _pasoActual = null;
                return;
            }

            if (Completada)
            {
                Estado = "Se ha intentado ejecutar un paso cuando la cadena estaba completada.";
                return;
            }

            var siguientePaso = _pasoActual.EjecutarPaso();
            Estado = _pasoActual.Estado;

            if (_pasoActual.EjecuciónCorrecta)
            {
                if (_pasoActual.EsFinal)
                {
                    _cochesMontados++;
                    ProductoListo = true;
                    ProductoMontado = _cocheEnMontaje;
                    _cocheEnMontaje = new Coche();

                    if (_cochesMontados == TotalCoches)
                    {
                        _pendienteDePasoFinal = true;
                        _pasoActual = new FinalizarMontaje();
                    }
                    else
                    {
                        _pasoActual = new AñadirChasis(_configuración, _cocheEnMontaje);
                    }
                }
                else
                {
                    _pasoActual = siguientePaso;
                }
            }
            else
            {
                throw new System.Exception(string.Format("Se ha producido un error: {0}", Estado));
            }
        }
    }
}