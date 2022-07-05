using API;
using API.Application.ViewModels;
using API.Routes;
using Domain.Common;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class OrdersControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public OrdersControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder_GivenFlightId_ReturnsCreated()
        {
            //arrange
            var orderViewModel = new OrderViewModel
            {
                Id = Guid.NewGuid(),
                NumberOfPassangers = 30,
                Price = 2500,
                FlightId = new Guid(SeedData.FLIGHT_ID),
                State = OrderEnum.Draft.ToString(),
                Date = DateTime.Now
            };

            var serializeObject = JsonConvert.SerializeObject(orderViewModel);
            var stringContent = new StringContent(serializeObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            var response = await _client.PostAsync(ApiRoutes.Orders.Create, stringContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}