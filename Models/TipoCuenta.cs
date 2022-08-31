using System.ComponentModel.DataAnnotations;
using ManejoProsupuesto.Validaciones;
using Microsoft.AspNetCore.Mvc;

namespace ManejoProsupuesto.Models;

public class TipoCuenta//:IValidatableObject
{
    public int Id { get; set; }
    // {0} hace referencia al campo actual en la vista
    // con display podemos colocar el nombre del label 
    [Required (ErrorMessage = "EL campo {0} es requerido")]
    [PrimeraLetraMayuscula]
    [Remote(action:"VerificarExisteTipoCuenta", controller:"TiposCuentas")]
    public string Nombre { get; set; }
    public int UsuarioId { get; set; }
    public int Orden { get; set; }
    
    /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Nombre != null && Nombre.Length > 0)
        {
            var primeraLetra = Nombre[0].ToString();
            if (primeraLetra != primeraLetra.ToUpper())
            {
                yield return new ValidationResult("La primera letra debe ser mayuscula",
                    new[] { nameof(Nombre) });
                // si se borra esta parte new[] { nameof(Nombre) } el mensaje del error
                // sale ariiba donde esta el div con asp-validation-summary="ModelOnly"

            }
        }
    }*/
}