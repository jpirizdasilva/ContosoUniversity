using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class OficinaAsignada
    {
        [Key]
        public int InstructorID { get; set; }
        [StringLength(50)]
        [Display(Name = "Ubicación de la oficina")]
        public string Ubicacion { get; set; }

        [Required]
        public Instructor Instructor { get; set; }
    }
}
