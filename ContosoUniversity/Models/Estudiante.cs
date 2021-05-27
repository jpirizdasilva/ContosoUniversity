using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Estudiante
    {
        
        public int ID { get; set; }
        public string Apellido { get; set; } //LastName
        public string Nombre { get; set; } //FirstMidName
        public DateTime FechaInscripcion { get; set; } //EnrollmentDate

        public ICollection<Inscripcion> Inscripciones { get; set; }
    }
}


    

