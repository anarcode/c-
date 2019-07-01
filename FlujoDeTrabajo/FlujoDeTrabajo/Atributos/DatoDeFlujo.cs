namespace FlujoDeTrabajo.Atributos
{
    using System;

    public class DatoDeFlujo : Attribute
    {
        public string NombreDeParámetro { get; set; }

        public DatoDeFlujo(string nombreDeParámetro)
        {
            NombreDeParámetro = nombreDeParámetro;
        }
    }
}
