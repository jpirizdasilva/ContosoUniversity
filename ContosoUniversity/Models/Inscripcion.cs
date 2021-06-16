using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public enum Calificacion //Grade
    {
        uno=1, dos=2, tres=3, cuatro=4, cinco=5, seis=6, siete=7, ocho=8, nueve=9, diez=10        
    }
    public class Inscripcion
    {   

        public int InscripcionID { get; set; }//EnrollmentID
        public int CursoID { get; set; }
        public int EstudianteID { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Calificación")]     
        public Calificacion? Calificacion { get; set; }

        public Curso Curso { get; set; }
        public Estudiante Estudiante { get; set; }
    }
}

