using System.ComponentModel.DataAnnotations;

namespace ManejoProsupuesto.Models;

public class TipoCuenta
{
    public int Id { get; set; }
    // {0} hace referencia al campo actual en la vista
    // con display podemos colocar el nombre del label 
    [Required (ErrorMessage = "EL campo {0} es requerido")]
    [StringLength(maximumLength:50, MinimumLength = 3,ErrorMessage = 
        "La longitud del {0} debe estar entre {1} y {2}")]
    [Display(Name = "El nombre de tipo cuenta")]
    public string Nombre { get; set; }
    public int UsuarioId { get; set; }
    public int Orden { get; set; }
}