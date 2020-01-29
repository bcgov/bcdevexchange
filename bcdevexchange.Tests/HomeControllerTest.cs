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
    public class HomeControllerTest
    {
        //Arrange
        Mock<IEventBriteService> eventbriteMock = new Mock<IEventBriteService>();
        IMemoryCache memCache = new MemoryCache(new MemoryCacheOptions());
        List<Event> events = new List<Event>
        {
         new Event(){Name = new EventBriteString(){Text = "dummy1" } },
         new Event(){Name = new EventBriteString(){Text = "dummy2" } },
         new Event(){Name = new EventBriteString(){Text = "dummy3" } },
        };
        List<Event> courses = new List<Event>
        {
         new Event(){Name = new EventBriteString(){Text = "dummy1" } },
         new Event(){Name = new EventBriteString(){Text = "dummy2" } },
         new Event(){Name = new EventBriteString(){Text = "dummy3" } },
         new Event(){Name = new EventBriteString(){Text = "dummy4" } }
        };
        private void APISetup()
        {
            eventbriteMock.Setup(m => m.GetAllEventsAsync()).Returns(Task.FromResult( events.AsEnumerable()));
            eventbriteMock.Setup(m => m.GetAllCoursesAsync()).Returns(Task.FromResult(courses.AsEnumerable()));
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_Test() //Confirms route returns view
        {
            //Arrange
            APISetup();

            HomeController controller = new HomeController(memCache, eventbriteMock.Object, new NullLogger<HomeController>());

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public async Task Mock_GetViewResultLearning_Test() //Confirms route returns view
        {
            //Arrange
            APISetup();
            HomeController controller = new HomeController(memCache, eventbriteMock.Object, new NullLogger<HomeController>());

            //Act
            var result = await controller.GetEvents() as ViewResult;
            var model = result.ViewData.Model as Dictionary<string, IList<Event>>;
            //Assert
            Assert.IsNotNull(model);
            var events = model["events"];
            var courses = model["courses"];
            Assert.IsNotNull(events);
            Assert.IsNotNull(courses);
            Assert.AreEqual(3, events.Count);
            Assert.AreEqual(4, courses.Count);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }
    }
}
