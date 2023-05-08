using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ParkingControl.data;
using ParkingControl.Models;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ParkingControl.Repository
{
    public class ParkedCarRepository
    {
        private readonly DBContext _dbContext;

        public ParkedCarRepository()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Parking;Trusted_Connection=True;")
            .Options;

            _dbContext = new DBContext(options);
        }

        public async Task<ParkedCar> Insert(ParkedCar car)
        {
            try
            {
                await _dbContext.ParkedCars.AddAsync(car);
                await _dbContext.SaveChangesAsync();

                return car;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<ParkedCar> FindById(int id)
        {
            try
            {
                ParkedCar car = await _dbContext.ParkedCars.FirstOrDefaultAsync(p => p.Id == id);
                return car;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ParkedCar>> FindByLicensePlate(string LicensePlate)
        {
            try
            {
                List<ParkedCar> cars = await _dbContext.ParkedCars.Where(c => c.LicensePlate.Contains(LicensePlate)).ToListAsync();

                return cars;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async  Task<List<ParkedCar>> FindAll()
        {
            try
            {
                return await _dbContext.ParkedCars.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ParkedCar> Update(int id, ParkedCar car)
        {
            try
            {
                Console.WriteLine(car);
                ParkedCar existingCar = await FindById(id);
                if (existingCar == null)
                {
                    throw new Exception("Car not found in database");
                }
                existingCar.LicensePlate = car.LicensePlate != null ? car.LicensePlate : existingCar.LicensePlate;
                existingCar.ArrivalTime = car.ArrivalTime != null ? car.ArrivalTime : existingCar.ArrivalTime;
                existingCar.DepartureTime = car.DepartureTime != null ? car.DepartureTime : existingCar.DepartureTime;
                existingCar.Duration = car.Duration != null ? car.Duration : existingCar.Duration;
                existingCar.SnakeTime = car.SnakeTime != null ? car.SnakeTime : existingCar.SnakeTime;
                existingCar.AmountToPay = car.AmountToPay != null ? car.AmountToPay : existingCar.AmountToPay;

                await _dbContext.SaveChangesAsync();
                 return existingCar; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var car = await _dbContext.ParkedCars.FirstOrDefaultAsync(p => p.Id == id);
                if (car != null)
                {
                     _dbContext.ParkedCars.Remove(car);
                    await _dbContext.SaveChangesAsync();

                   
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }

        }

    }
}
