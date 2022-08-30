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
    
    /* Pruebas de otras validaciones por defecro*/
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [EmailAddress(ErrorMessage = "El campo debe ser un correo electronico válido")]
    public string Email { get; set; }
    [Range(minimum:18, maximum:130, ErrorMessage = "El valor debe estar entre {1} y {2}")]
    public int Edad { get; set; }
    [Url(ErrorMessage = "El campo deber ser una URL válida")]
    public string URL { get; set; }
    [CreditCard(ErrorMessage = "La tarjeta de crédito no es valida")]
    [Display(Name = "Tarjeta De Crédito")]
    public string TarjetaDeCredito { get; set; }
}