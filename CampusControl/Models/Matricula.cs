using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CampusControl.Models
{
    public class Matricula
    {
        // Identificador único de la matrícula
        public int MatriculaId { get; set; }

        // Clave foránea del estudiante

        [Required]
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        // Clave foránea del curso
        [Required]
        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        // Año o semestre de la matrícula
        [Required(ErrorMessage = "El año es obligatorio.")]
        [Range(2000, 2100, ErrorMessage = "El año debe estar entre 2000 y 2100.")]
        [DisplayName("Año")]
        public int Anio { get; set; }

        // Puede incluir información adicional, como el estado de la matrícula (activa, inactiva, etc.)
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; } // Ejemplo: "Activa", "Inactiva"
    }
}
