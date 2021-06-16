/*
 * Tabla intermedia de relación varios a varios entre 
 * Curso e Instructor
 * */


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class CursoAsignado
    {
        public int InstructorID { get; set; }
        public int CursoID { get; set; }
        public Instructor Instructor { get; set; }
        public Curso Curso { get; set; }
    }
}