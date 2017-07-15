using CadenaDeMontaje.Core;
using System;

namespace CadenaDeMontaje.Configuraciones
{
    public class ConfiguraciónDeCocheBaseConColoresAleatorios : ConfiguraciónDeCocheBase
    {
        Random _generador;

        readonly string[] COLORES = new string[] { "morango", "sepia", "salmón" };

        public override string Color
        {
            get { return COLORES[_generador.Next(0, COLORES.Length)]; }
        }

        public ConfiguraciónDeCocheBaseConColoresAleatorios()
        {
            _generador = new Random();
        }
    }
}