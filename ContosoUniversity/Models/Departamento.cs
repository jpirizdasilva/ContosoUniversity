using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Presupuesto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        public int? InstructorID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Instructor Administrador { get; set; }
        public ICollection<Curso> Cursos { get; set; }
    }
}