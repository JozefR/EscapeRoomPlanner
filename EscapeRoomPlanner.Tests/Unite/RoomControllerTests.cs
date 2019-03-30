using EscapeRoomPlanner.Controllers;
using EscapeRoomPlanner.Data.EntityFramework;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EscapeRoomPlanner.Tests.Unite
{
    public class RoomControllerTests
    {
        private readonly Mock<IRoomRepository> _roomRepository;
        private readonly Mock<IDataSeeder> _dataSeeder;

        public RoomControllerTests()
        {
            _dataSeeder = new Mock<IDataSeeder>();
            _roomRepository = new Mock<IRoomRepository>();
        }

        [Fact]
        public void RoomDetail_IdIsZero_ReturnNotFound()
        {
            var controller = new RoomController(_roomRepository.Object, _dataSeeder.Object);

            var result = controller.Detail(0).Result;

            Assert.IsType<NotFoundResult>(result);
        }
    }
}