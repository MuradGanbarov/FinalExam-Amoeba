using FinalExam_Amoeba.Areas.Admin.ViewModels;
using FinalExam_Amoeba.DAL;
using FinalExam_Amoeba.Models;
using FinalExam_Amoeba.Utilities.Enums;
using FinalExam_Amoeba.Utilities.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam_Amoeba.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page)
        {
            double count = await _context.Employees.Include(e => e.Position).CountAsync();
            List<Employee> employees = await _context.Employees.Include(e => e.Position).Skip(page * 3).Take(3).ToListAsync();
            PaginationVM<Employee> vm = new()
            {
                CurrentPage = page + 1,
                TotalPage = Math.Ceiling(count / 3),
                Items = employees
            };

            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            EmployeeCreateVM vm = new()
            {
                Positions = await _context.Positions.ToListAsync()
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Positions = await _context.Positions.ToListAsync();
                return View(vm);
                    
            };
            if (!vm.Photo.IsValidType(FileType.Image))
            {
                vm.Positions = await _context.Positions.ToListAsync();
                ModelState.AddModelError("Photo", "Photo should be image type");
                return View(vm);
            }
            if (!vm.Photo.IsValidSize(5, FileSize.Megabyte))
            {
                vm.Positions = await _context.Positions.ToListAsync();
                ModelState.AddModelError("Photo", "Photo can be less or equal 5mb");
                return View(vm);
            }
            Employee employee = new()
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Description = vm.Description,
                Facebook = vm.Facebook,
                Instagram = vm.Instagram,
                LinkedIn = vm.LinkedIn,
                PositionId = vm.PositionId,
                Twitter = vm.Twitter,
                ImageURL = await vm.Photo.CreateAsync(_env.WebRootPath, "assets", "img", "team")
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Employee existed = await _context.Employees.Include(e => e.Position).FirstOrDefaultAsync(e => e.Id == id);
            if (existed is null) return NotFound();
            EmployeeUpdateVM vm = new()
            {
                Name = existed.Name,
                Surname = existed.Surname,
                Description = existed.Description,
                Facebook = existed.Facebook,
                Instagram = existed.Instagram,
                LinkedIn = existed.LinkedIn,
                PositionId = existed.PositionId,
                Twitter = existed.Twitter,
                ImageURL = existed.ImageURL,
                Positions = await _context.Positions.ToListAsync(),
            };
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,EmployeeUpdateVM vm)
        {
            if (!ModelState.IsValid) return View();
            Employee existed = await _context.Employees.Include(e => e.Position).FirstOrDefaultAsync(e => e.Id == id);
            if (existed is null) return NotFound();
            if(vm.Photo is not null)
            {
                if (!vm.Photo.IsValidType(FileType.Image))
                {
                    vm.Positions = await _context.Positions.ToListAsync();
                    ModelState.AddModelError("Photo", "Photo should be image type");
                    return View(vm);
                }
                if (!vm.Photo.IsValidSize(5, FileSize.Megabyte))
                {
                    vm.Positions = await _context.Positions.ToListAsync();
                    ModelState.AddModelError("Photo", "Photo can be less or equal 5mb");
                    return View(vm);
                }
                existed.ImageURL.Delete(_env.WebRootPath, "assets", "img", "team");
                existed.ImageURL = await vm.Photo.CreateAsync(_env.WebRootPath, "assets", "img", "team");
            }
            existed.Name = vm.Name;
            existed.Surname = vm.Surname;
            existed.Description = vm.Description;
            existed.Facebook = vm.Facebook;
            existed.Twitter = vm.Twitter;
            existed.PositionId = vm.PositionId;
            existed.Instagram = vm.Instagram;
            existed.Facebook = vm.Facebook;
            existed.LinkedIn = vm.LinkedIn;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {

            if (id <= 0) return BadRequest();
            Employee existed = await _context.Employees.Include(e => e.Position).FirstOrDefaultAsync(e => e.Id == id);
            if (existed is null) return NotFound();
            existed.ImageURL.Delete(_env.WebRootPath, "assets", "img", "team");
            _context.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }
}
