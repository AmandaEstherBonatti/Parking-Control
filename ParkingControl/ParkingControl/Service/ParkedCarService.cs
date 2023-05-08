using ParkingControl.Models;
using ParkingControl.Repository;
using System.Numerics;

namespace ParkingControl.Service
{
    public class ParkedCarService
    {
        private readonly ParkedCarRepository parkedCarRepository;

        public ParkedCarService()
        {
            parkedCarRepository = new ParkedCarRepository();
        }

        public async Task<List<ParkedCar>> FindAllCars()
        {
            return await this.parkedCarRepository.FindAll();
        }

        public async Task<ParkedCar> FindByIdCar(int id)
        {
            return await this.parkedCarRepository.FindById(id);
        }

        public async Task<List<ParkedCar>> FindByCarLicensePlate(string LicensePlate)
        {
            return await this.parkedCarRepository.FindByLicensePlate(LicensePlate);
        }


        public async Task<ParkedCar> InsertCar(string LicensePlate)
        {
            bool validate = ValidatePlate(LicensePlate);
            if (validate)
            {
                ParkedCar car = new ParkedCar()
                {
                    LicensePlate = LicensePlate,
                    ArrivalTime = DateTime.Now,
                    SnakeTime = 1,
                    Price = 2,
                    AmountToPay = 2

                };

            return await this.parkedCarRepository.Insert(car); ;
        }
            return null;

        }

       public async Task<ParkedCar> Close(ParkedCar car)
        {
            DateTime departureTime = DateTime.Now;
            TimeSpan totalTime = departureTime - car.ArrivalTime;

            int hours = totalTime.Hours;
            int minutes = totalTime.Minutes;

            double additionalHourValue = 0.0;
            int hoursNoTolerance = hours;

            if (minutes > (hours * 10))
            {
                hoursNoTolerance++;  
            }
            if (hoursNoTolerance <= 1)
            {
                additionalHourValue = 1.0;
            }
            else if (hoursNoTolerance <= 2)
            {
                additionalHourValue = 2.0;
            }
            else
            {
                additionalHourValue = 2.0 + (hoursNoTolerance - 2) * 1.0;
            }

            double amount = additionalHourValue * hoursNoTolerance;

            car.DepartureTime = departureTime;
            car.Duration = totalTime.ToString("hh\\:mm");
            car.SnakeTime = totalTime.Hours;
            car.AmountToPay = float.Parse(amount.ToString());

            await UpdateCar(car.Id,car);
            return car;

        }
        public async Task<ParkedCar> UpdateCar(int id, ParkedCar car)
        {
            await this.parkedCarRepository.Update(id,car);
            return car ;
        }

        public async Task<bool> DeleteCar(int id)
        {
           return await this.parkedCarRepository.Delete(id);
        }


        static bool ValidatePlate(string Plate)
        {
            Plate = Plate.Replace(" ", "");

            char[] punctuation = new char[] { '.', '-', ' ' };

            foreach (char p in punctuation)
            {
                Plate = Plate.Replace(p.ToString(), "");
            }


            if (Plate.Length != 7)
            {
                return false;
            }

            for (int i = 0; i < 3; i++)
            {
                if (!Char.IsLetter(Plate[i]))
                {
                    return false;
                }
            }

            for (int i = 3; i < 7; i++)
            {
                if (!Char.IsDigit(Plate[i]))
                {
                    return false;
                }
            }

            if (Plate == "AAA0A00" || Plate == "AAA00A0" || Plate == "AAA000A")
            {
                return false;
            }

            return true;
        }
    }
}
