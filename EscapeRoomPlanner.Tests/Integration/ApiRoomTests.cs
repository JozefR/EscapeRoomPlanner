using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using EscapeRoomPlanner.Data.EntityFramework;
using EscapeRoomPlanner.Data.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.OData.Query.SemanticAst;
using Xunit;

namespace EscapeRoomPlanner.Tests.Integration
{
    public class RoomTests
    {
        private readonly HttpClient _client;

        public RoomTests()
        {
            IntegrityFactory<Startup> factory = new IntegrityFactory<Startup>();
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

            var rooms = response.Content.ReadAsAsync<List<DTO.Room>>().Result;

            foreach (var room in rooms)
            {
                var hourInterval = room.ClosingTime - room.OpeningTime;
                var availableHours = room.AvailableHours;

                Assert.Equal(hourInterval, availableHours.Count);
            }
        }
    }
}