using ContosoUniversity.Models;
using System;
using System.Linq;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EscuelaContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Estudiantes.Any())
            {
                return;   // DB has been seeded
            }

            var estudiantes = new Estudiante[]
            {
            new Estudiante{Nombre="Carson",Apellido="Alexander",FechaInscripcion=DateTime.Parse("2005-09-01")},
            new Estudiante{Nombre="Meredith",Apellido="Alonso",FechaInscripcion=DateTime.Parse("2002-09-01")},
            new Estudiante{Nombre="Arturo",Apellido="Anand",FechaInscripcion=DateTime.Parse("2003-09-01")},
            new Estudiante{Nombre="Gytis",Apellido="Barzdukas",FechaInscripcion=DateTime.Parse("2002-09-01")},
            new Estudiante{Nombre="Yan",Apellido="Li",FechaInscripcion=DateTime.Parse("2002-09-01")},
            new Estudiante{Nombre="Peggy",Apellido="Justice",FechaInscripcion=DateTime.Parse("2001-09-01")},
            new Estudiante{Nombre="Laura",Apellido="Norman",FechaInscripcion=DateTime.Parse("2003-09-01")},
            new Estudiante{Nombre="Nino",Apellido="Olivetto",FechaInscripcion=DateTime.Parse("2005-09-01")}
            };
            foreach (Estudiante s in estudiantes)
            {
                context.Estudiantes.Add(s);
            }
            context.SaveChanges();

            var cursos = new Curso[]
            {
            new Curso{CursoID=1050,Titulo="Chemistry",Creditos=3},
            new Curso{CursoID=4022,Titulo="Microeconomics",Creditos=3},
            new Curso{CursoID=4041,Titulo="Macroeconomics",Creditos=3},
            new Curso{CursoID=1045,Titulo="Calculus",Creditos=4},
            new Curso{CursoID=3141,Titulo="Trigonometry",Creditos=4},
            new Curso{CursoID=2021,Titulo="Composition",Creditos=3},
            new Curso{CursoID=2042,Titulo="Literature",Creditos=4}
            };
            foreach (Curso c in cursos)
            {
                context.Cursos.Add(c);
            }
            context.SaveChanges();

            var inscripciones = new Inscripcion[]
            {
            new Inscripcion{EstudianteID=1,CursoID=1050,Calificacion=Calificacion.diez},
            new Inscripcion{EstudianteID=1,CursoID=4022,Calificacion=Calificacion.siete},
            new Inscripcion{EstudianteID=1,CursoID=4041,Calificacion=Calificacion.nueve},
            new Inscripcion{EstudianteID=2,CursoID=1045,Calificacion=Calificacion.nueve},
            new Inscripcion{EstudianteID=2,CursoID=3141,Calificacion=Calificacion.cuatro},
            new Inscripcion{EstudianteID=2,CursoID=2021,Calificacion=Calificacion.seis},
            new Inscripcion{EstudianteID=3,CursoID=1050},
            new Inscripcion{EstudianteID=4,CursoID=1050},
            new Inscripcion{EstudianteID=4,CursoID=4022,Calificacion=Calificacion.cuatro},
            new Inscripcion{EstudianteID=5,CursoID=4041,Calificacion=Calificacion.siete},
            new Inscripcion{EstudianteID=6,CursoID=1045},
            new Inscripcion{EstudianteID=7,CursoID=3141,Calificacion=Calificacion.diez},
            };
            foreach (Inscripcion e in inscripciones)
            {
                context.Inscripciones.Add(e);
            }
            context.SaveChanges();
        }
    }
}
