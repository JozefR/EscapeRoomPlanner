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
            private readonly Mock<IRoomRepository> _roomRepository;

            public RoomApiController()
            {
                _roomRepository = new Mock<IRoomRepository>();
            }

            [Fact]
            public void RoomGet_BadDateFormat_ReturnBadRequest()
            {
                var controller = new RoomController(_roomRepository.Object);

                var result = controller.Get(1, "BadDateFormat").Result;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }
    }
}