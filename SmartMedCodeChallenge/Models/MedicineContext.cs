using Microsoft.EntityFrameworkCore;

namespace SmartMedCodeChallenge.Models
{
    public class MedicineContext : DbContext
    {
        public MedicineContext(DbContextOptions<MedicineContext> options) : base(options)
        {
        }

        public DbSet<Medicine> SmartMedMedicines { get; set; } = null!;
    }
}
