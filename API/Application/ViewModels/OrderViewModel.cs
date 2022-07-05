using System;

namespace API.Application.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPassangers { get; set; }
    }
}