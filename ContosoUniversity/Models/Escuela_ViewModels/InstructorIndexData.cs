using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models.Escuela_ViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructores { get; set; }
        public IEnumerable<Curso> Cursos { get; set; }
        public IEnumerable<Inscripcion> Inscripciones { get; set; }
    }
}