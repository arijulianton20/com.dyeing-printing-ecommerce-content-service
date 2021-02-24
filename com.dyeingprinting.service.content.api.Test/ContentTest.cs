using com.dyeingprinting.service.content.api.Controllers;
using com.dyeingprinting.service.content.business;
using com.dyeingprinting.service.content.business.Service;
using com.dyeingprinting.service.content.data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace com.dyeingprinting.service.content.api.Test
{
    [TestClass]
    public class ContentTest
    {
        private Mock<MobileContentController> _mobileController;
        private Mock<WebContentController> _webController;

        private Mock<MobileContentService> _mobileService;
        private Mock<WebContentService> _webService;

        private  Mock<ILogger<WebContentController>> _logger;
        private  Mock<IService<WebContent>> _service;

        private Mock<DbSet<WebContent>> _contentDbSet;

        public ContentTest()
        {
            _mobileController = new Mock<MobileContentController>();
            _webController = new Mock<WebContentController>();

            _mobileService = new Mock<MobileContentService>();
            _webService = new Mock<WebContentService>();

            _logger = new Mock<ILogger<WebContentController>>();
            _service = new Mock<IService<WebContent>>();
        }

        public WebContent webContentData = new WebContent
        {
            Name = "Test",
            Title ="Test",
            Description ="Test",
            ImageUrl ="Test",
            Link ="Test",
            Order = 1
       
    };

        public WebContentController GetController()
        {
            var controller = new WebContentController(_service.Object,_logger.Object);
            return controller;
        }

        [TestMethod]
        public async Task Content_Test()
        {
            var controller = GetController();
            var result = controller.FindAsync().Result;
            var code = result as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, code.StatusCode);
        }
    }
}
