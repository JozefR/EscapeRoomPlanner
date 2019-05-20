using System.Collections.Generic;
using System.Net.Http;
using EscapeRoomPlanner.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EscapeRoomPlanner.Tests.Integration
{
    public class RoomTests
    {
        private readonly HttpClient _client;

        public RoomTests()
        {
            var factory = new IntegrityFactory<Startup>();
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Theory]
        [InlineData("01.01.2019")]
        public void ApiRoomsGet_AvailableHours_ReturnEqual(string date)
        {
            var response = _client.GetAsync($"/api/room/{date}").Result;

            response.EnsureSuccessStatusCode();

            var rooms = response.Content.ReadAsAsync<List<Room>>().Result;

            foreach (var room in rooms)
            {
                var hourInterval = room.ClosingTime - room.OpeningTime;
                var availableHours = room.AvailableHours;

                Assert.Equal(hourInterval, availableHours.Count);
            }
        }
    }
}