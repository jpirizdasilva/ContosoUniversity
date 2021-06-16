using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Estudiante
    {
        
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength =2)]     
        public string Apellido { get; set; } //LastName

        [Required]
        [StringLength(50, MinimumLength =2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Column("PrimerNombre")]
        public string Nombre { get; set; } //FirstMidName

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Fecha de Inscripción")]
        public DateTime FechaInscripcion { get; set; } //EnrollmentDate

        [Display(Name ="Nombre Completo")]
        public string NombreCompleto
        {
            get
            {
                return Nombre + Apellido;
            }
        }

        public ICollection<Inscripcion> Inscripciones { get; set; }
    }
}


    

