using System.ComponentModel.DataAnnotations;

namespace CampusControl.Models
{
    public class Curso
    {
        // Identificador único del curso
        public int Id { get; set; }

        // Nombre del curso
        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del curso no puede exceder los 100 caracteres.")]
        public string NombreCurso { get; set; }

        // Número de créditos del curso
        [Required(ErrorMessage = "Los créditos son obligatorios.")]
        [Range(1, 10, ErrorMessage = "El número de créditos debe estar entre 1 y 10.")]
        public int Creditos { get; set; }

        // Descripción del curso
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; }
    }
}