using Exam8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Exam8.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            var emp = _context.Student.FromSqlRaw($"sp_GetAll").AsEnumerable().ToList();
            return View(emp);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                string data = $"sp_Insert '{student.StName}','{student.Age}','{student.Marks}','{student.DoB}','{student.IsActive}'";
                _context.Database.ExecuteSqlRaw(data);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var std = _context.Student.FromSqlRaw($"sp_GetStudent {id}").AsEnumerable().FirstOrDefault();
            if (std == null)
            {
                return NotFound();
            }
            return View(std); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                string data = $"sp_Update '{student.Id}','{student.StName}','{student.Age}','{student.Marks}','{student.DoB}','{student.IsActive}'";
                _context.Database.ExecuteSqlRaw(data);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var std = _context.Student.FromSqlRaw($"sp_GetStudent {id}").AsEnumerable().FirstOrDefault();
            if (std != null)
            {
                return View(std);
            }
            return NotFound();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var std = _context.Student.FromSqlRaw($"sp_GetStudent {id}").AsEnumerable().FirstOrDefault();
            if (std != null)
            {
                return View(std);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw($"sp_Delete {student.Id}");
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public IActionResult StudentStat()
        {
            int count = _context.Student.Count();
            ViewBag.Count = count;

            int min = _context.Student.Min(s => s.Age);
            ViewBag.Min = min;

            int max = _context.Student.Max(s => s.Age);
            ViewBag.Max = max;

            decimal avg = (decimal)_context.Student.Average(s => s.Age);
            ViewBag.Avg = Math.Round(avg, 2);

            int sum = _context.Student.Sum(s => s.Marks);
            ViewBag.Sum = sum;

            return View();
        }


    }
}
