namespace Lunar_Veil.ViewModels
{
    public class EmployeeUploadViewModel
    {
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }

        public IFormFile? Photo { get; set; }  // ảnh upload
    }
}
