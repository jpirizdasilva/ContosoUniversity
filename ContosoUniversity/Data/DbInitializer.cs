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



            var instructores = new Instructor[]
            {
                new Instructor { Nombre = "Kim",     Apellido = "Abercrombie",
                    FechaContratacion = DateTime.Parse("1995-03-11") },
                new Instructor { Nombre = "Fadi",    Apellido = "Fakhouri",
                    FechaContratacion = DateTime.Parse("2002-07-06") },
                new Instructor { Nombre = "Roger",   Apellido = "Harui",
                    FechaContratacion = DateTime.Parse("1998-07-01") },
                new Instructor { Nombre = "Candace", Apellido = "Kapoor",
                    FechaContratacion = DateTime.Parse("2001-01-15") },
                new Instructor { Nombre = "Roger",   Apellido = "Zheng",
                    FechaContratacion = DateTime.Parse("2004-02-12") }
            };

            foreach (Instructor i in instructores)
            {
                context.Instructores.Add(i);
            }
            context.SaveChanges();


            var departments = new Departamento[]
           {
                new Departamento { Nombre = "English",     Presupuesto = 350000,
                    FechaInicio = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructores.Single( i => i.Apellido == "Abercrombie").ID },
                new Departamento { Nombre = "Mathematics", Presupuesto = 100000,
                    FechaInicio = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructores.Single( i => i.Apellido == "Fakhouri").ID },
                new Departamento { Nombre = "Engineering", Presupuesto = 350000,
                    FechaInicio = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructores.Single( i => i.Apellido == "Harui").ID },
                new Departamento { Nombre = "Economics",   Presupuesto = 100000,
                    FechaInicio = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructores.Single( i => i.Apellido == "Kapoor").ID }
           };

            foreach (Departamento d in departments)
            {
                context.Departamentos.Add(d);
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



            var oficinasAsignadas = new OficinaAsignada[]
           {
                new OficinaAsignada {
                    InstructorID = instructores.Single( i => i.Apellido == "Fakhouri").ID,
                    Ubicacion = "Smith 17" },
                new OficinaAsignada {
                    InstructorID = instructores.Single( i => i.Apellido == "Harui").ID,
                    Ubicacion = "Gowan 27" },
                new OficinaAsignada {
                    InstructorID = instructores.Single( i => i.Apellido == "Kapoor").ID,
                    Ubicacion = "Thompson 304" },
           };

            foreach (OficinaAsignada o in oficinasAsignadas)
            {
                context.OficinasAsignadas.Add(o);
            }
            context.SaveChanges();


            var CursoInstructores = new CursoAsignado[]
           {
                new CursoAsignado {
                    CursoID = cursos.Single(c => c.Titulo == "Chemistry" ).CursoID,
                    InstructorID = instructores.Single(i => i.Apellido == "Kapoor").ID
                    },
                new CursoAsignado {
                    CursoID = cursos.Single(c => c.Titulo == "Chemistry" ).CursoID,
                    InstructorID = instructores.Single(i => i.Apellido == "Harui").ID
                    },
                new CursoAsignado {
                    CursoID = cursos.Single(c => c.Titulo == "Microeconomics" ).CursoID,
                    InstructorID = instructores.Single(i => i.Apellido == "Zheng").ID
                    },
                new CursoAsignado {
                    CursoID = cursos.Single(c => c.Titulo == "Macroeconomics" ).CursoID,
                    InstructorID = instructores.Single(i => i.Apellido == "Zheng").ID
                    },
                new CursoAsignado {
                    CursoID = cursos.Single(c => c.Titulo == "Calculus" ).CursoID,
                    InstructorID = instructores.Single(i => i.Apellido == "Fakhouri").ID
                    },
                new CursoAsignado {
                    CursoID = cursos.Single(c => c.Titulo == "Trigonometry" ).CursoID,
                    InstructorID = instructores.Single(i => i.Apellido == "Harui").ID
                    },
                new CursoAsignado {
                    CursoID = cursos.Single(c => c.Titulo == "Composition" ).CursoID,
                    InstructorID = instructores.Single(i => i.Apellido == "Abercrombie").ID
                    },
                new CursoAsignado {
                    CursoID = cursos.Single(c => c.Titulo == "Literature" ).CursoID,
                    InstructorID = instructores.Single(i => i.Apellido == "Abercrombie").ID
                    },
           };

            foreach (CursoAsignado ci in CursoInstructores)
            {
                context.CursosAsignados.Add(ci);
            }
            context.SaveChanges();


            var inscripciones = new Inscripcion[]
            {
                    new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellido == "Alexander").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Chemistry" ).CursoID,
                    Calificacion = Calificacion.diez
                    },
                    new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellido == "Alexander").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Microeconomics" ).CursoID,
                    Calificacion = Calificacion.siete
                    },
                    new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellido == "Alexander").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Macroeconomics" ).CursoID,
                    Calificacion = Calificacion.nueve
                    },
                    new Inscripcion {
                        EstudianteID = estudiantes.Single(s => s.Apellido == "Alonso").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Calculus" ).CursoID,
                    Calificacion = Calificacion.nueve
                    },
                    new Inscripcion {
                        EstudianteID = estudiantes.Single(s => s.Apellido == "Alonso").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Trigonometry" ).CursoID,
                    Calificacion = Calificacion.ocho
                    },
                    new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellido == "Alonso").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Composition" ).CursoID,
                    Calificacion = Calificacion.nueve
                    },
                    new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellido == "Anand").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Chemistry" ).CursoID
                    },
                    new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellido == "Anand").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Microeconomics").CursoID,
                    Calificacion = Calificacion.siete
                    },
                    new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellido == "Barzdukas").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Chemistry").CursoID,
                    Calificacion = Calificacion.seis
                    },
                    new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellido == "Li").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Composition").CursoID,
                    Calificacion = Calificacion.nueve
                    },
                    new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellido == "Justice").ID,
                    CursoID = cursos.Single(c => c.Titulo == "Literature").CursoID,
                    Calificacion = Calificacion.cinco
                    }
            };
            foreach (Inscripcion e in inscripciones)
            {
                var inscripcionEnBaseDeDatos = context.Inscripciones.Where(
                    s =>
                            s.Estudiante.ID == e.EstudianteID &&
                            s.Curso.CursoID == e.CursoID).SingleOrDefault();
                if (inscripcionEnBaseDeDatos == null)
                {
                    context.Inscripciones.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
