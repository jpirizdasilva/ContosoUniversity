using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Curso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name= "Número")]
        public int CursoID { get; set; }

        [StringLength(50, MinimumLength =3)]
        public string Titulo { get; set; }

        [Range(0,5)]
        public int Creditos { get; set; }

        public int DepartamentoID { get; set; }

        public Departamento Departamento { get; set; }
        public ICollection<Inscripcion> Inscripciones { get; set; }

        public ICollection<CursoAsignado> CursoAsignados { get; set; }
    }
}
