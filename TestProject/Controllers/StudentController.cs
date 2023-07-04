using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.DTOs;
using TestProject.Interfaces;
using TestProject.Models;
using TestProject.Services;
using Humanizer;
using TestProject.ViewModels;

namespace TestProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        private readonly UserManager<AppUser> _userManager;
        public StudentController(ApplicationDbContext context, IImageService imageService, UserManager<AppUser> userManager)
        {
            _context = context;
            _imageService = imageService;
            _userManager = userManager;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
              return _context.Student != null ? 
                          View(await _context.Student.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Student'  is null.");
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDTO dto)
        {
            var ProfileImage = dto.ProfileImage;
            var IDImage = dto.IDImage;            

            if (ModelState.IsValid)
            {
                // User Creation
                var user = new AppUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    Active = true,
                    // DEBUG ONLY
                    EmailConfirmed = true
                };

                var insertreq = await _userManager.CreateAsync(user, dto.Password);

                if (!insertreq.Succeeded)
                {
                    foreach (var error in insertreq.Errors)
                    {
                        ModelState.AddModelError("", "User Creation Failed");
                    }
                    return View(dto);
                }

                // Mapping
                var config = new MapperConfiguration(cfg => cfg.CreateMap<StudentDTO, Student>());
                var mapper = config.CreateMapper();
                Student student = mapper.Map<Student>(dto);
                

                // Image Upload
                string profileUrl = await _imageService.ImageUpload(ProfileImage);
                string idUrl = await _imageService.ImageUpload(IDImage);

                student.ProfileImageUrl = profileUrl;
                student.IDImageUrl = idUrl;

                // Assign User
                var appuser = await _userManager.FindByEmailAsync(user.Email);
                student.UserId = appuser.Id;
                student.User = user;

                // Adding to Db
                _context.Add(student);
                await _context.SaveChangesAsync();
            
                // Complete Relation
                appuser.Student = student;
                await _userManager.UpdateAsync(appuser);

                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // Mapping
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, EditVM>());
            var mapper = config.CreateMapper();
            EditVM vm = mapper.Map<EditVM>(student);

            return View(vm);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditVM dto)
        {
            if(id != dto.Id)
            {
                return NotFound(dto.Id);
            }

            // Mapping
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EditVM, Student>());
            var mapper = config.CreateMapper();
            Student student = mapper.Map<Student>(dto);

            // Image action
            var ProfileImage = dto.ProfileImage;
            var IDImage = dto.IDImage;

            if (ModelState.IsValid)
            {

                // Image Check and Upload
                if (ProfileImage != null)
                {
                    string profileUrl = await _imageService.ImageUpload(ProfileImage);
                    student.ProfileImageUrl = profileUrl;

                }
                if (IDImage != null)
                {
                    string idUrl = await _imageService.ImageUpload(IDImage);
                    student.IDImageUrl = idUrl;
                }

                // Update Db
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(dto);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
