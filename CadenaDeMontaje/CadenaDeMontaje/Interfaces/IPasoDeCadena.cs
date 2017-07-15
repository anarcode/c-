namespace CadenaDeMontaje.Interfaces
{
    public interface IPasoDeCadena
    {
        bool EsFinal { get; }

        string Estado { get; }

        bool EjecuciónCorrecta { get; }

        IPasoDeCadena EjecutarPaso();
    }
}