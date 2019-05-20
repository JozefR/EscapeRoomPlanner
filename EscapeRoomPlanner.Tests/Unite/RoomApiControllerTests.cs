using EscapeRoomPlanner.Controllers.API;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EscapeRoomPlanner.Tests.Unite
{
    public class RoomApiControllerTests
    {
        public class RoomApiController
        {
            public RoomApiController()
            {
                _roomRepository = new Mock<IRoomRepository>();
            }

            private readonly Mock<IRoomRepository> _roomRepository;

            [Fact]
            public void RoomGet_BadDateFormat_ReturnBadRequest()
            {
                var controller = new RoomController(_roomRepository.Object);

                var result = controller.Get(1, selectedDate: "BadDateFormat").Result;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }
    }
}