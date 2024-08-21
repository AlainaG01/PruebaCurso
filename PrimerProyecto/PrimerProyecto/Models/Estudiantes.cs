using System.ComponentModel.DataAnnotations;

namespace PrimerProyecto.Models;

public class Estudiantes
{
    [Key]
    public int EstudianteId { get; set; }

    [Required(ErrorMessage ="Nombre obligatorio")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Solo se permiten letras")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Matricula obligatoria")]
    public int? Matricula { get; set; }

    [Required(ErrorMessage = "Carrera obligatorio")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Solo se permiten letras")]
    public string? Carrera { get; set; }
}
