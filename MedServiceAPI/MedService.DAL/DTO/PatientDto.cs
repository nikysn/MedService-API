using MedService.DAL.Model;

namespace MedService.DAL.DTO
{
    public class PatientDto
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
    }
}
