using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        public int ID { get; set; }

        [Required]       
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [Column("PrimerNombre")]    
        [StringLength(50)]
        public string Nombre { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Contratación")]
        public DateTime FechaContratacion { get; set; } //HireDate

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto
        {
            get { return Nombre + Apellido; }
        }

        public ICollection<CursoAsignado> CursosAsignados { get; set; }
        public OficinaAsignada OficinaAsignada { get; set; }
    }
}