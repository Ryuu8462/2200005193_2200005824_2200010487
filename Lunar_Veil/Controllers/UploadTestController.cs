using Lunar_Veil.Data;
using Lunar_Veil.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Lunar_Veil.Controllers
{
    public class UploadTestController : Controller
    {
        private readonly _22bitv02EmployeeContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public UploadTestController(_22bitv02EmployeeContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            
            _webHostEnviroment = webHostEnviroment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = null;

                if (model.Photo != null)
                {
                    // Validate size
                    if (model.Photo.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Photo", "Ảnh quá lớn (tối đa 2MB)");
                        return View(model);
                    }

                    // Validate loại file
                    var ext = Path.GetExtension(model.Photo.FileName).ToLower();
                    if (ext != ".jpg" && ext != ".png")
                    {
                        ModelState.AddModelError("Photo", "Chỉ chấp nhận .jpg và .png");
                        return View(model);
                    }

                    // Tạo tên file duy nhất
                    uniqueFileName = Guid.NewGuid().ToString() + ext;

                    // Lưu vào thư mục wwwroot/images/photos
                    var uploadsFolder = Path.Combine(_webHostEnviroment.WebRootPath, "images/photos");
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Photo.CopyToAsync(stream);
                    }
                }

                // Lưu dữ liệu vào DB
                var employee = new Employee
                {
                    EmployeeName = model.Name,
                    DepartmentId = model.DepartmentId,
                    PhotoImagePath = uniqueFileName
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home"); // hoặc Employee list
            }

            return View(model);
        }
    }
}
