using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class EscuelaContext: DbContext
    {
        public EscuelaContext(DbContextOptions<EscuelaContext> options): base(options)
        {
        }

        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instructor> Instructores { get; set; }
        public DbSet<OficinaAsignada> OficinasAsignadas { get; set; }
        public DbSet<CursoAsignado> CursosAsignados { get; set; }

        //Establecer el nombre que tendran las tablas en la BBDD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().ToTable("Curso");
            modelBuilder.Entity<Inscripcion>().ToTable("Inscripcion");
            modelBuilder.Entity<Estudiante>().ToTable("Estudiante");
            modelBuilder.Entity<Departamento>().ToTable("Departamento");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OficinaAsignada>().ToTable("OficinaAsignada");
            modelBuilder.Entity<CursoAsignado>().ToTable("CursoAsignado");

            modelBuilder.Entity<CursoAsignado>()
                .HasKey(c => new { c.CursoID, c.InstructorID });
        }
    }
}
