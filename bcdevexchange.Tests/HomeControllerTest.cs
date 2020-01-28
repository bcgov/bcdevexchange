using Microsoft.VisualStudio.TestTools.UnitTesting;
using bcdevexchange.Service;
using bcdevexchange.Models;
using Moq;
using bcdevexchange.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace bcdevexchange.Tests
{
    [TestClass]
    class HomeControllerTest
    {
        //Arrange
        Mock<IEventBriteService> eventbriteMock = new Mock<IEventBriteService>();
        Mock<IMemoryCache> memMock = new Mock<IMemoryCache>();
        List<Event> events = new List<Event>
        {
         new Event(){Name = new EventBriteString(){Text = "dummy1" } },
         new Event(){Name = new EventBriteString(){Text = "dummy2" } },
         new Event(){Name = new EventBriteString(){Text = "dummy3" } },
        };

        private void APISetup()
        {
            eventbriteMock.Setup(m => m.GetAllEventsAsync()).Returns(Task.FromResult( events.AsEnumerable()));

            List<Event> result = new List<Event>();
            memMock.Setup(m => m.TryGetValue(It.IsAny<string>(), out result)).Returns(true);

            memMock.Setup(m => m.Set(It.IsAny<string>(), It.IsAny<List<Event>>(),It.IsAny<MemoryCacheEntryOptions>()));
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_Test() //Confirms route returns view
        {
            //Arrange
            APISetup();

            HomeController controller = new HomeController(memmock.Object,mock.Object,new NullLogger<HomeController>());
            //Act
            var result = controller.Index();
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

    }
}
