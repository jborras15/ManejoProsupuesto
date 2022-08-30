using System.ComponentModel.DataAnnotations;
using ManejoProsupuesto.Validaciones;

namespace ManejoProsupuesto.Models;

public class TipoCuenta
{
    public int Id { get; set; }
    // {0} hace referencia al campo actual en la vista
    // con display podemos colocar el nombre del label 
    [Required (ErrorMessage = "EL campo {0} es requerido")]
    [PrimeraLetraMayuscula]
    public string Nombre { get; set; }
    public int UsuarioId { get; set; }
    public int Orden { get; set; }
    
}