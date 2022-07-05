using System.Collections.Generic;

namespace API.Application.ViewModels
{
    public class ListFlightWithLowestPriceViewModel
    {
        public decimal LowestFlightPrice { get; set; }
        public List<FlightViewModel> FlightsDetails { get; set; }
    }
}