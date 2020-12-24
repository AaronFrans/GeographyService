using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using RestLayer.Controllers;
using RestLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RestLayerTests
{
    public class GeographyControllerTests
    {
        private readonly Mock<IManager> mockManager;
        private readonly Mock<ILogger<GeographyController>> mockLogger;
        private readonly Mock<IConfiguration> mockConfig;
        private readonly GeographyController geographyController;

        public GeographyControllerTests()
        {
            mockManager = new Mock<IManager>();
            mockLogger = new Mock<ILogger<GeographyController>>();

            mockConfig = new Mock<IConfiguration>();

            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value).Returns("http://localhost:5051");

            mockConfig.Setup(a => a.GetSection("profiles:RestLayer:applicationUrl")).Returns(configurationSection.Object);

            geographyController = new GeographyController(mockManager.Object, mockConfig.Object, mockLogger.Object);
        }

        #region Continent

        #region GET

        [Fact]
        public void GetCountry_ReturnsRequestType()
        {
            mockManager.Setup(repo => repo.GetContinent(2))
                .Returns(new Continent("Continent") { Id = 2 });


            var result = geographyController.GetContinent(2);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetContinent_ReturnsContinentBody()
        {
            Continent c = new Continent("Continent") { Id = 2 };
            mockManager.Setup(repo => repo.GetContinent(2))
                .Returns(c);

            var result = geographyController.GetContinent(2).Result as OkObjectResult;

            Assert.IsType<RContinentOutput>(result.Value);
            Assert.Equal("http://localhost:5051" + "/api/continent/2", (result.Value as RContinentOutput).Id);
            Assert.Equal(c.Population, (result.Value as RContinentOutput).Population);
            Assert.Equal(c.Name, (result.Value as RContinentOutput).Name);
            Assert.Empty((result.Value as RContinentOutput).Countries);
        }

        [Fact]
        public void GetContinent_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.GetContinent(2))
                .Throws(new Exception());
            var result = geographyController.GetContinent(2);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetContinent_ReturnsNotFound()
        {
            mockManager.Setup(repo => repo.GetContinent(2))
                .Throws(new DomainException("Er is geen continent met het gegeven id."));
            var result = geographyController.GetContinent(2);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        #endregion

        #region POST
        [Fact]
        public void PostContinent_ReturnsRequestType()
        {
            Continent c = new Continent("bla");
            mockManager.Setup(repo => repo.AddContinent(c)).Returns(1);
            mockManager.Setup(repo => repo.GetContinent(1)).Returns(c);
            var result = geographyController.PostContinent(new RContinentInput() { Name = "bla" });
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void PostContinent_ReturnsContinentBody()
        {
            Continent c = new Continent("bla");
            mockManager.Setup(repo => repo.AddContinent(c)).Returns(1);
            c.Id = 1;
            mockManager.Setup(repo => repo.GetContinent(1)).Returns(c);

            var result = geographyController.PostContinent(new RContinentInput() { Name = "bla" }).Result as CreatedAtActionResult;

            Assert.IsType<RContinentOutput>(result.Value);
            Assert.Equal("http://localhost:5051" + "/api/continent/1", (result.Value as RContinentOutput).Id);
            Assert.Equal(c.Population, (result.Value as RContinentOutput).Population);
            Assert.Equal(c.Name, (result.Value as RContinentOutput).Name);
            Assert.Empty((result.Value as RContinentOutput).Countries);
        }

        [Fact]
        public void PostContinent_ReturnsBadRequest()
        {
            Continent c = new Continent("bla");
            mockManager.Setup(repo => repo.AddContinent(c)).Throws(new Exception());

            var result = geographyController.PostContinent(new RContinentInput() { Name = "bla" });

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        #endregion

        #region PUT
        [Fact]
        public void PutContinent_ReturnsRequestType()
        {
            Continent c = new Continent("bla");
            mockManager.Setup(repo => repo.UpdateContinent(1, c));
            c.Id = 1;
            mockManager.Setup(repo => repo.GetContinent(1))
                .Returns(c);
            var result = geographyController.PutContinent(1, new RContinentInput() { Name = "bla", Id = 1 });
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void PutContinent_ReturnsContinentBody()
        {
            Continent c = new Continent("bla");
            mockManager.Setup(repo => repo.UpdateContinent(1, c));
            c.Id = 1;
            mockManager.Setup(repo => repo.GetContinent(1))
                .Returns(c);

            var result = geographyController.PutContinent(1, new RContinentInput() { Name = "bla", Id = 1 }).Result as OkObjectResult;


            Assert.IsType<RContinentOutput>(result.Value);
            Assert.Equal("http://localhost:5051" + "/api/continent/1", (result.Value as RContinentOutput).Id);
            Assert.Equal(c.Population, (result.Value as RContinentOutput).Population);
            Assert.Equal(c.Name, (result.Value as RContinentOutput).Name);
            Assert.Empty((result.Value as RContinentOutput).Countries);
        }

        [Fact]
        public void PutContinent_ReturnsBadRequest()
        {
            Continent c = new Continent("bla");
            mockManager.Setup(repo => repo.UpdateContinent(1, c)).Throws(new Exception());

            var result = geographyController.PutContinent(1, new RContinentInput() { Name = "bla" });

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void PutContinent_ReturnsNotFound()
        {
            Continent c = new Continent("bla");
            mockManager.Setup(repo => repo.UpdateContinent(1, c)).Throws(new DomainException("Er is geen continent met het gegeven id."));

            var result = geographyController.PutContinent(1, new RContinentInput() { Name = "bla", Id = 1 });

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        #endregion

        #region DELETE
        [Fact]
        public void DeleteContinent_ReturnsRequestType()
        {
            mockManager.Setup(repo => repo.DeleteContinent(1));
            var result = geographyController.DeleteContinent(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteContinent_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.DeleteContinent(1)).Throws(new Exception());

            var result = geographyController.DeleteContinent(1);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void DeleteContinent_ReturnsNotFound()
        {
            mockManager.Setup(repo => repo.DeleteContinent(1))
                .Throws(new Exception("Er is geen continent met het gegeven id."));

            var result = geographyController.DeleteContinent(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion

        #endregion

        #region Country

        #region GET
        [Fact]
        public void GetOrder_ReturnsRequestType()
        {
            Country o = new Country(5, "Country", 5.0f, new Continent("Arr") { Id = 1 }) { Id = 2 };
            mockManager.Setup(repo => repo.GetCountry(1, 2))
                .Returns(o);

            var result = geographyController.GetCountry(1, 2);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void GetOrder_ReturnsClientBody()
        {
            Country o = new Country(5, "Country", 5.0f, new Continent("Arr") { Id = 1 }) { Id = 2 };

            mockManager.Setup(repo => repo.GetCountry(1, 2))
                .Returns(o);

            var result = geographyController.GetCountry(1, 2).Result as OkObjectResult;

            Assert.IsType<RCountryOutput>(result.Value);
            Assert.Equal("http://localhost:5051" + "/api/continent/1/country/2", (result.Value as RCountryOutput).Id);
            Assert.Equal(o.Name, (result.Value as RCountryOutput).Name);
            Assert.Equal("http://localhost:5051" + "/api/continent/1", (result.Value as RCountryOutput).Continent);
            Assert.Equal(o.Population, (result.Value as RCountryOutput).Population);
            Assert.Equal(o.Surface, (result.Value as RCountryOutput).Surface);
        }
        [Fact]
        public void GetOrder_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.GetCountry(2, 1))
                .Throws(new Exception());
            var result = geographyController.GetCountry(2, 1);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public void GetOrder_ReturnsNotFoundContinent()
        {
            mockManager.Setup(repo => repo.GetCountry(2, 1))
                .Throws(new DomainException("Er is geen continent met het gegeven id."));
            var result = geographyController.GetCountry(2, 1);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public void GetOrder_ReturnsNotFoundCountry()
        {
            mockManager.Setup(repo => repo.GetCountry(2, 1))
                .Throws(new DomainException("Er is geen land met het gegeven id."));
            var result = geographyController.GetCountry(2, 1);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        #endregion
        #region POST
        [Fact]
        public void PostOrder_ReturnsRequestType()
        {
            Country o = new Country(5, "Country", 5.0f, new Continent("Arr") { Id = 1 }) { Id = 2 };
            mockManager.Setup(repo => repo.AddCountry(2, "Country", 5, 5.0f))
                .Returns(1);

            mockManager.Setup(repo => repo.GetCountry(2, 1))
                .Returns(o);

            var result = geographyController.PostCountry(new RCountryInput() { ContinentId = 2, Name = "Country", Population = 5, Surface = 5.0f }, 2);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void PostOrder_ReturnsClientBody()
        {
            Country o = new Country(5, "Country", 5.0f, new Continent("Arr") { Id = 1 }) { Id = 2 };

            mockManager.Setup(repo => repo.AddCountry(2, "Country", 5, 5.0f))
                .Returns(1);

            mockManager.Setup(repo => repo.GetCountry(2, 1))
                .Returns(o);

            var result = geographyController.PostCountry(new RCountryInput() { ContinentId = 2, Name = "Country", Population = 5, Surface = 5.0f }, 2).Result as CreatedAtActionResult;

            Assert.IsType<RCountryOutput>(result.Value);
            Assert.Equal("http://localhost:5051" + "/api/continent/1/country/2", (result.Value as RCountryOutput).Id);
            Assert.Equal(o.Name, (result.Value as RCountryOutput).Name);
            Assert.Equal("http://localhost:5051" + "/api/continent/1", (result.Value as RCountryOutput).Continent);
            Assert.Equal(o.Population, (result.Value as RCountryOutput).Population);
            Assert.Equal(o.Surface, (result.Value as RCountryOutput).Surface);
        }

        [Fact]
        public void PostOrder_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.AddCountry(2, "Country", 5, 5.0f))
                .Throws(new Exception());
            var result = geographyController.PostCountry(new RCountryInput() { ContinentId = 2, Name = "Country", Population = 5, Surface = 5.0f }, 2);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion

        #region PUT
        [Fact]
        public void PutOrder_ReturnsRequestType()
        {
            Country o = new Country(5, "Country", 5.0f, new Continent("Arr") { Id = 1 }) { Id = 2 };

            mockManager.Setup(repo => repo.UpdateCountry(1, 2, "Country", 5, 5.0f));

            mockManager.Setup(repo => repo.GetCountry(2, 1))
                .Returns(o);

            var result = geographyController.PutCountry(2, 1, new RCountryInput() { ContinentId = 2, Name = "Country", Population = 5, Surface = 5.0f, Id = 1 });
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void PutOrder_ReturnsClientBody()
        {
            Country o = new Country(5, "Country", 5.0f, new Continent("Arr") { Id = 1 }) { Id = 2 };

            mockManager.Setup(repo => repo.UpdateCountry(1, 2, "Country", 5, 5.0f));

            mockManager.Setup(repo => repo.GetCountry(2, 1))
                .Returns(o);

            var result = geographyController.PutCountry(2, 1, new RCountryInput() { ContinentId = 2, Name = "Country", Population = 5, Surface = 5.0f, Id = 1 }).Result as OkObjectResult;

            Assert.IsType<RCountryOutput>(result.Value);
            Assert.Equal("http://localhost:5051" + "/api/continent/1/country/2", (result.Value as RCountryOutput).Id);
            Assert.Equal(o.Name, (result.Value as RCountryOutput).Name);
            Assert.Equal("http://localhost:5051" + "/api/continent/1", (result.Value as RCountryOutput).Continent);
            Assert.Equal(o.Population, (result.Value as RCountryOutput).Population);
            Assert.Equal(o.Surface, (result.Value as RCountryOutput).Surface);
        }

        [Fact]
        public void PutOrder_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.UpdateCountry(1, 2, "Country", 5, 5.0f))
                .Throws(new Exception());
            var result = geographyController.PutCountry(2, 1, new RCountryInput() { ContinentId = 2, Name = "Country", Population = 5, Surface = 5.0f });
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void PutOrder_ReturnsNotFoundContinent()
        {
            mockManager.Setup(repo => repo.UpdateCountry(1, 2, "Country", 5, 5.0f))
                .Throws(new DomainException("Er is geen continent met het gegeven id."));
            var result = geographyController.PutCountry(2, 1, new RCountryInput() { ContinentId = 2, Name = "Country", Population = 5, Surface = 5.0f });
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void PutOrder_ReturnsNotFoundCountry()
        {
            mockManager.Setup(repo => repo.UpdateCountry(1, 2, "Country", 5, 5.0f))
                .Throws(new DomainException("Er is geen land met het gegeven id."));
            var result = geographyController.PutCountry(2, 1, new RCountryInput() { ContinentId = 2, Name = "Country", Population = 5, Surface = 5.0f });
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion

        #region DELETE
        [Fact]
        public void DeleteOrder_ReturnsRequestType()
        {
            mockManager.Setup(repo => repo.DeleteCountry(1, 1));

            var result = geographyController.DeleteCountry(1, 1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteOrder_ReturnsNotFoundContinent()
        {
            mockManager.Setup(repo => repo.DeleteCountry(1, 1))
                .Throws(new DomainException("Er is geen continent met het gegeven id."));
            var result = geographyController.DeleteCountry(1, 1);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void DeleteOrder_ReturnsNotFoundCountry()
        {
            mockManager.Setup(repo => repo.DeleteCountry(1, 1))
                .Throws(new DomainException("Er is geen land met het gegeven id."));
            var result = geographyController.DeleteCountry(1, 1);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void DeleteOrder_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.DeleteCountry(1, 1))
                .Throws(new Exception());
            var result = geographyController.DeleteCountry(1, 1);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #endregion


    }
}
