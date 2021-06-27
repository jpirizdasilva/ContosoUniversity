using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly EscuelaContext _context;

        public DepartamentoController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: Departamento
        public async Task<IActionResult> Index()
        {
            var escuelaContext = _context.Departamentos.Include(d => d.Administrador);
            return View(await escuelaContext.ToListAsync());
        }

        // GET: Departamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .Include(d => d.Administrador)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartamentoID == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamento/Create
        public IActionResult Create()
        {
            ViewData["InstructorID"] = new SelectList(_context.Instructores, "ID", "NombreCompleto");
            return View();
        }

        // POST: Departamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartamentoID,Nombre,Presupuesto,FechaInicio,InstructorID,RowVersion")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructores, "ID", "NombreCompleto", departamento.InstructorID);
            return View(departamento);
        }

        // GET: Departamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .Include(i => i.Administrador)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartamentoID == id);

            if (departamento == null)
            {
                return NotFound();
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructores, "ID", "NombreCompleto", departamento.InstructorID);
            return View(departamento);
        }

        // POST: Departamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
        {
            if (id == null)
            {
                return NotFound();
            }


            var departamentoAActualizar = await _context.Departamentos.Include(i => i.Administrador).FirstOrDefaultAsync(m => m.DepartamentoID == id);

            if (departamentoAActualizar == null)
            {
                Departamento DepartamentoEliminado = new Departamento();
                await TryUpdateModelAsync(DepartamentoEliminado);
                ModelState.AddModelError(string.Empty,
                    "No se pueden guardar los cambios. El departamento fue eliminado por otro usuario.");
                ViewData["InstructorID"] = new SelectList(_context.Instructores, "ID", "NombreCompleto", DepartamentoEliminado.InstructorID);
                return View(DepartamentoEliminado);
            }

            _context.Entry(departamentoAActualizar).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<Departamento>(
                departamentoAActualizar,
                "",
                s => s.Nombre, s => s.FechaInicio, s => s.Presupuesto, s => s.InstructorID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Departamento)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Imposible guardar los cambios. El departamento fue eliminado por otro usuario.");
                    }
                    else
                    {
                        var databaseValues = (Departamento)databaseEntry.ToObject();

                        if (databaseValues.Nombre != clientValues.Nombre)
                        {
                            ModelState.AddModelError("Nombre", $"Current value: {databaseValues.Nombre}");
                        }
                        if (databaseValues.Presupuesto != clientValues.Presupuesto)
                        {
                            ModelState.AddModelError("Presupuesto", $"Current value: {databaseValues.Presupuesto:c}");
                        }
                        if (databaseValues.FechaInicio != clientValues.FechaInicio)
                        {
                            ModelState.AddModelError("FechaInicio", $"Current value: {databaseValues.FechaInicio:d}");
                        }
                        if (databaseValues.InstructorID != clientValues.InstructorID)
                        {
                            Instructor databaseInstructor = await _context.Instructores.FirstOrDefaultAsync(i => i.ID == databaseValues.InstructorID);
                            ModelState.AddModelError("InstructorID", $"Current value: {databaseInstructor?.NombreCompleto}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, click "
                                + "the Save button again. Otherwise click the Back to List hyperlink.");
                        departamentoAActualizar.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructores, "ID", "FullName", departamentoAActualizar.InstructorID);
            return View(departamentoAActualizar);
        }

        // GET: Departamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .Include(d => d.Administrador)
                .FirstOrDefaultAsync(m => m.DepartamentoID == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamentos.Any(e => e.DepartamentoID == id);
        }
    }
}
