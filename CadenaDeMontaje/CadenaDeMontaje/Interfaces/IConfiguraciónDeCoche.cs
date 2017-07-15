namespace CadenaDeMontaje.Interfaces
{
    public interface IConfiguraciónDeCoche
    {
        bool NecesitaRuedas { get; }

        int NúmeroDeRuedas { get; }

        string Color { get; }
    }
}