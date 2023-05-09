using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingControl.data;
using ParkingControl.Models;
using ParkingControl.Repository;
using ParkingControl.Service;
using System.Collections.Generic;
using System.Diagnostics;

namespace ParkingControl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static List<ParkedCar> cars = new List<ParkedCar>();
        private static ParkedCar car = new ParkedCar();
        private readonly ParkedCarService parkedCarService = new ParkedCarService();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public  async Task<IActionResult> Index()
        {
                cars = await this.parkedCarService.FindAllCars();
 
            return View(cars);
        }

        public async Task<IActionResult> CreateControlCar(string LicensePlate)
        {
             car = await this.parkedCarService.InsertCar(LicensePlate);
            cars.Add(car);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CloseCar(int id)
        {
            car = await this.parkedCarService.FindByIdCar(id);
            await this.parkedCarService.Close(car);
            cars.Add(car);
            return RedirectToAction("Index");
        }
        public async Task<List<ParkedCar>> SearchCars(string LicensePlate)
        {
            if (LicensePlate != null)
            {
                cars = await this.parkedCarService.FindByCarLicensePlate(LicensePlate);
            }
            else
            {
                cars = await this.parkedCarService.FindAllCars();
            }

            return cars;
        }

        public async Task<IActionResult> CarDetails(int id)
        {
            ParkedCar carFound = await this.parkedCarService.FindByIdCar(id);
            return View(carFound);
        }

        public async Task<IActionResult> DeleteCar(int id)
        {
           var res =  await this.parkedCarService.DeleteCar(id);
            Console.WriteLine(res);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateCar(int id, ParkedCar carEdit)
        {
            ParkedCar car = await this.parkedCarService.VerifyUpdate(id, carEdit);
            cars.Add(car);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}