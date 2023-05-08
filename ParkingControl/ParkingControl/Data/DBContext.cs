using Microsoft.EntityFrameworkCore;
using ParkingControl.Models;

namespace ParkingControl.data
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        public DbSet<ParkedCar> ParkedCars { get; set; }
    }
}
