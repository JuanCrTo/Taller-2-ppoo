using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampusControl.Data;
using CampusControl.Models;

namespace CampusControl.Controllers
{
    public class MatriculasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MatriculasController> _logger;

        public MatriculasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matricula
        public async Task<IActionResult> Index()
        {
            var matriculas = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Curso)
                .ToListAsync();
            return View(matriculas);
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.Curso)
                .Include(m => m.Estudiante)
                .FirstOrDefaultAsync(m => m.MatriculaId == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // GET: Matricula/Create
        public IActionResult Create()
        {
            // Cargar las listas desplegables para Estudiantes y Cursos
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes.Select(e =>
                new { e.Id, NombreCompleto = e.Nombre + " " + e.Apellido }), "Id", "NombreCompleto");
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "NombreCurso");

            // Establecer el año actual como valor predeterminado
            var matricula = new Matricula
            {
                Anio = DateTime.Now.Year,
                Estado = "Activa" // Valor predeterminado para Estado
            };

            return View(matricula);
        }

        // POST: Matriculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Matricula/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatriculaId,EstudianteId,CursoId,Anio,Estado")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                // Verificar que el estudiante no esté ya matriculado en el mismo curso
                var matriculaExistente = await _context.Matriculas
                    .AnyAsync(m => m.EstudianteId == matricula.EstudianteId &&
                                 m.CursoId == matricula.CursoId &&
                                 m.Anio == matricula.Anio);

                if (matriculaExistente)
                {
                    ModelState.AddModelError("", "El estudiante ya está matriculado en este curso para el año seleccionado.");
                    ViewData["EstudianteId"] = new SelectList(_context.Estudiantes.Select(e =>
                        new { e.Id, NombreCompleto = e.Nombre + " " + e.Apellido }), "Id", "NombreCompleto");
                    ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "NombreCurso");
                    return View(matricula);
                }

                _context.Add(matricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes.Select(e =>
                new { e.Id, NombreCompleto = e.Nombre + " " + e.Apellido }), "Id", "NombreCompleto");
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "NombreCurso");
            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "NombreCurso", matricula.CursoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Apellido", matricula.EstudianteId);
            return View(matricula);
        }

        // POST: Matriculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatriculaId,EstudianteId,CursoId,Anio,Estado")] Matricula matricula)
        {
            if (id != matricula.MatriculaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists(matricula.MatriculaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "NombreCurso", matricula.CursoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Apellido", matricula.EstudianteId);
            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.Curso)
                .Include(m => m.Estudiante)
                .FirstOrDefaultAsync(m => m.MatriculaId == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula != null)
            {
                _context.Matriculas.Remove(matricula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.MatriculaId == id);
        }
    }
}
