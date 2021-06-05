
using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.Escuela_ViewModels
{
    public class InscripcionFechaGrupo
    {
        [DataType(DataType.Date)]
        public DateTime? FechaInscripcion { get; set; }

        public int CantidadDeEstudiantes { get; set; }
    }
}
