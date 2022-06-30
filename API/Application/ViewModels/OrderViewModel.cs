using Domain.Aggregates.FlightAggregate;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Text.Json.Serialization;

namespace API.Application.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
        public int NumberOfPassangers { get; set; }
    }
}
