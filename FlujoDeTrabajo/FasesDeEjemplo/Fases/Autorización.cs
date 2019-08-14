namespace FasesDeEjemplo.Fases
{
    using Entidades;
    using FlujoDeTrabajo.Atributos;
    using FlujoDeTrabajo.Interfaces;
    using FlujoDeTrabajo.Nucelo;
    using System.Collections.Generic;
    using System.Linq;
    //using Flujos = Nucelo.Constantes.Flujos;

    public class Autorización : IFase
    {
        [MétodoDeFlujo(Criticidad.AvisarUnPoco)]
        public IResultado<Usuario> Procesar(Usuario usuario, List<string> rolesPermitidos, GestorDeFlujo gestorDeFlujo)
        {
            // Para probar la gestión de errores
            // throw new System.Exception("Soy un chemisky");
            
            if (usuario.Roles.Any(r => rolesPermitidos.Contains(r)))
            {
                //gestorDeFlujo.AñadirFlujo(Flujos.CREAR_ASUNTO);
                return new Correcto<Usuario>(usuario);
            }
            else
            {
                return new Error<Usuario>(usuario, "El usuario no tiene permisos");
            }
        }
    }
}
