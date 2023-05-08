using System.Reflection.Metadata.Ecma335;

namespace ParkingControl.Models
{
    public class ParkedCar
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        
        public string? Duration { get; set; }
        public int SnakeTime { get; set; }
        public float Price { get; set; }
        public float? AmountToPay { get; set; }

    }
}
