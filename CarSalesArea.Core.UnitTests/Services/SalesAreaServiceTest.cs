using System;
using AutoMapper;
using CarSalesArea.Core.Services;
using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using CarSalesArea.Core.Models;

namespace CarSalesArea.Core.UnitTests.Services
{
    [TestClass]
    public class SalesAreaServiceTest
    {
        private readonly ISalesAreaService _salesAreaService;
        private readonly Mock<ISalesAreaRepository> _salesAreaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public SalesAreaServiceTest()
        {
            _salesAreaRepositoryMock = new Mock<ISalesAreaRepository>();
            _mapperMock = new Mock<IMapper>();
            _salesAreaService = new SalesAreaService(
                _salesAreaRepositoryMock.Object,
                _mapperMock.Object);
        }

        [TestMethod]
        public void GetAllSalesAreaAsync_ReturnsCollectionOfSalesAreas_IfAnyExist()
        {
            //Arrange
            var collection = new List<SalesArea>
            {
                new SalesArea()
                {
                    Id = 1,
                    AreaLocation = "location",
                    Capacity = 300
                }
            };

            var collectionModel = new List<SalesAreaModel>
            {
                new SalesAreaModel()
                {
                    Id = 1,
                    AreaLocation = "location",
                    Capacity = 300
                }
            };

            _salesAreaRepositoryMock
                .Setup(r => r.GetAllSalesAreasCollectionAsync())
                .ReturnsAsync(collection);
            _mapperMock
                .Setup(r => r.Map<IEnumerable<SalesAreaModel>>(
                    It.IsAny<IEnumerable<SalesArea>>()))
                .Returns(collectionModel);

            //Act
            var result = _salesAreaService.GetAllSalesAreasAsync();

            //Assert
            Assert.AreEqual(collectionModel, result.Result);
        }

        [TestMethod]
        public void GetSalesAreaByIdAsync_ReturnSalesArea_IfItExist()
        {
            //Arrange
            var salesArea = new SalesArea()
            {
                Id = 1,
                AreaLocation = "location",
                Capacity = 300
            };

            var salesAreaModel = new SalesAreaModel()
            {
                Id = 1,
                AreaLocation = "location",
                Capacity = 300
            };

            _salesAreaRepositoryMock
                .Setup(r => r.GetSalesAreaByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(salesArea);
            _mapperMock
                .Setup(r => r.Map<SalesAreaModel>(
                    It.IsAny<SalesArea>()))
                .Returns(salesAreaModel);

            //Act
            var result = _salesAreaService.GetSalesAreaByIdAsync(1);

            //Assert
            Assert.AreEqual(salesAreaModel, result.Result);
        }

        [TestMethod]
        public void GetManagerByIdAsync_ThrowsNullReferenceException_IfNotExist()
        {
            //Arrange
            SalesArea area = null;
            _salesAreaRepositoryMock
                .Setup(r => r.GetSalesAreaByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(area);

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _salesAreaService.GetSalesAreaByIdAsync(It.IsAny<long>()));
        }

        [TestMethod]
        public void CreateManagerAsync_CreatesManager_IfModelIsValid()
        {
            //Arrange
            var salesAreaModel = new SalesAreaModel()
            {
                Id = 1,
                AreaLocation = "location",
                Capacity = 300
            };

            _salesAreaRepositoryMock
                .Setup(r => r.CreateSalesAreaAsync(It.IsAny<SalesArea>()));

            //Act
            _salesAreaService.CreateSalesAreaAsync(salesAreaModel);

            //Assert
            _salesAreaRepositoryMock.Verify(x => x.CreateSalesAreaAsync(It.IsAny<SalesArea>()), Times.Once);
        }

        [TestMethod]
        public void CreateManagerAsync_ThrowsArgumentNullException_IfModelIsNull()
        {
            //Arrange
            SalesAreaModel salesArea = null;

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _salesAreaService.CreateSalesAreaAsync(salesArea));
        }

        [TestMethod]
        public void UpdateManagerAsync_UpdatesManager_ItExists()
        {
            //Arrange
            var salesAreaModel = new SalesAreaModel()
            {
                Id = 1,
                AreaLocation = "location",
                Capacity = 300
            };

            _salesAreaRepositoryMock
                .Setup(r => r.UpdateSalesAreaAsync(It.IsAny<SalesArea>()));

            //Act
            _salesAreaService.UpdateSalesAreaAsync(salesAreaModel);

            //Assert
            _salesAreaRepositoryMock.Verify(x => x.UpdateSalesAreaAsync(It.IsAny<SalesArea>()), Times.Once);
        }

        [TestMethod]
        public void UpdateManagerAsync_ThrowsArgumentNullException_IfModelIsNull()
        {
            //Arrange
            SalesAreaModel areaModel = null;

            //Assert
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _salesAreaService.UpdateSalesAreaAsync(areaModel));
        }

        [TestMethod]
        public void UpdateManagerAsync_ThrowsNullReferenceException_IfNotExist()
        {
            //Arrange
            var salesAreaModel = new SalesAreaModel()
            {
                Id = 1,
                AreaLocation = "location",
                Capacity = 300
            };

            _salesAreaRepositoryMock.Setup(x => x.UpdateSalesAreaAsync(It.IsAny<SalesArea>()))
                .Throws<NullReferenceException>();

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _salesAreaService.UpdateSalesAreaAsync(salesAreaModel));
        }
    }
}
