using CadenaDeMontaje.CadenasDeMontaje;
using CadenaDeMontaje.Configuraciones;
using CadenaDeMontaje.Interfaces;
using CadenaDeMontaje.Productos;
using System;
using System.Collections.Generic;

namespace CadenaDeMontaje
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Coche> concesionario = new List<Coche>();
            IConfiguraciónDeCoche configuraciónDeCoche = new ConfiguraciónDeCocheBaseConColoresAleatorios();
            ICadenaDeMontaje<Coche> cadenaDeMontaje = new CadenaDeMontajeDeCoche(configuraciónDeCoche)
            {
                TotalCoches = 4
            };

            Console.WriteLine(cadenaDeMontaje.Estado);
            while (!cadenaDeMontaje.Completada)
            {
                cadenaDeMontaje.SiguientePaso();
                Console.WriteLine(cadenaDeMontaje.Estado);

                if (cadenaDeMontaje.ProductoListo)
                {
                    concesionario.Add(cadenaDeMontaje.ProductoMontado);
                }
            }

            Console.ReadLine();
        }
    }
}