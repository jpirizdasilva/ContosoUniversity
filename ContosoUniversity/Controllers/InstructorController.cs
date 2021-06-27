using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.Escuela_ViewModels;

namespace ContosoUniversity.Controllers
{
    public class InstructorController : Controller
    {
        private readonly EscuelaContext _context;

        public InstructorController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: Instructor
        public async Task<IActionResult> Index(int? id, int? cursoID)
        {
            var viewModel = new InstructorIndexData();
            viewModel.Instructores = await _context.Instructores
                  .Include(i => i.OficinaAsignada)
                  .Include(i => i.CursosAsignados)
                    .ThenInclude(i => i.Curso)
                        .ThenInclude(i => i.Inscripciones)
                            .ThenInclude(i => i.Estudiante)
                  .Include(i => i.CursosAsignados)
                    .ThenInclude(i => i.Curso)
                        .ThenInclude(i => i.Departamento)
                  .AsNoTracking()
                  .OrderBy(i => i.Apellido)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = viewModel.Instructores.Where(
                    i => i.ID == id.Value).Single();
                viewModel.Cursos = instructor.CursosAsignados.Select(s => s.Curso);
            }

            if (cursoID != null)
            {
                ViewData["CourseID"] = cursoID.Value;
                viewModel.Inscripciones = viewModel.Cursos.Where(
                    x => x.CursoID == cursoID).Single().Inscripciones;
            }

            return View(viewModel);
        }

        // GET: Instructor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructor/Create
        public IActionResult Create()
        {
            var instructor = new Instructor();
            instructor.CursosAsignados = new List<CursoAsignado>();
            PopulateAssignedCourseData(instructor);

            return View();
        }

        // POST: Instructor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OficinaAsignada,Apellido,Nombre,FechaContratacion")] Instructor instructor, string[] cursosSeleccionados)
        {
            if (cursosSeleccionados != null)
            {
                instructor.CursosAsignados = new List<CursoAsignado>();
                foreach (var curso in cursosSeleccionados)
                {
                    var cursoAAgregar = new CursoAsignado { InstructorID = instructor.ID, CursoID = int.Parse(curso) };
                    instructor.CursosAsignados.Add(cursoAAgregar);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // GET: Instructor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructores
               .Include(i => i.OficinaAsignada)
               .Include(i => i.CursosAsignados).ThenInclude(i => i.Curso)
               .AsNoTracking()
               .FirstOrDefaultAsync(m => m.ID == id);

            PopulateAssignedCourseData(instructor);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }
        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var cursosTodos = _context.Cursos;
            var cursosInstructor = new HashSet<int>(instructor.CursosAsignados.Select(c => c.CursoID));
            var viewModel = new List<DatosCursosAsignados>();
            foreach (var curso in cursosTodos)
            {
                viewModel.Add(new DatosCursosAsignados
                {
                    CursoID = curso.CursoID,
                    Titulo = curso.Titulo,
                    Asignado = cursosInstructor.Contains(curso.CursoID)
                });
            }
            ViewData["Cursos"] = viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] cursosSeleccionados)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructorAActualizar = await _context.Instructores
                .Include(i => i.OficinaAsignada)
                .Include(i => i.CursosAsignados).ThenInclude(i => i.Curso)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<Instructor>(
                instructorAActualizar,
                "",
                i => i.Nombre, i => i.Apellido, i => i.FechaContratacion, i => i.OficinaAsignada))
            {
                if (String.IsNullOrWhiteSpace(instructorAActualizar.OficinaAsignada?.Ubicacion))
                {
                    instructorAActualizar.OficinaAsignada = null;
                }
                UpdateInstructorCourses(cursosSeleccionados, instructorAActualizar);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateInstructorCourses(cursosSeleccionados, instructorAActualizar);
            PopulateAssignedCourseData(instructorAActualizar);
            return View(instructorAActualizar);
        }
        private void UpdateInstructorCourses(string[] cursosSeleccionados, Instructor instructorAActualizar)
        {
            if (cursosSeleccionados == null)
            {
                instructorAActualizar.CursosAsignados = new List<CursoAsignado>();
                return;
            }

            var cursosSeleccionadosHS = new HashSet<string>(cursosSeleccionados);
            var instructorCursos = new HashSet<int>
                (instructorAActualizar.CursosAsignados.Select(c => c.Curso.CursoID));

            foreach (var curso in _context.Cursos)
            {
                if (cursosSeleccionadosHS.Contains(curso.CursoID.ToString()))
                {
                    if (!instructorCursos.Contains(curso.CursoID))
                    {
                        instructorAActualizar.CursosAsignados.Add(new CursoAsignado { InstructorID = instructorAActualizar.ID, CursoID = curso.CursoID });
                    }
                }
                else
                {

                    if (instructorCursos.Contains(curso.CursoID))
                    {
                        CursoAsignado cursoARemover = instructorAActualizar.CursosAsignados.FirstOrDefault(i => i.CursoID == curso.CursoID);
                        _context.Remove(cursoARemover);
                    }
                }
            }
        }

        // GET: Instructor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var instructor = await _context.Instructores.FindAsync(id);
            Instructor instructor = await _context.Instructores
                .Include(i => i.CursosAsignados)
                .SingleAsync(i => i.ID == id);

            var departments = await _context.Departamentos
                .Where(d => d.InstructorID == id)
                .ToListAsync();
            departments.ForEach(d => d.InstructorID = null);
            _context.Instructores.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructores.Any(e => e.ID == id);
        }
    }
}
