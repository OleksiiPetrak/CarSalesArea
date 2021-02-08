using CarSalesArea.Core.Services;
using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using CarSalesArea.Core.Models;

namespace CarSalesArea.Core.UnitTests.Services
{
    [TestClass]
    public class ManagerServiceTest
    {
        private readonly IManagerService _managerService;
        private readonly Mock<IManagerRepository> _managerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public ManagerServiceTest()
        {
            _managerRepositoryMock = new Mock<IManagerRepository>();
            _mapperMock = new Mock<IMapper>();
            _managerService = new ManagerService(
                _managerRepositoryMock.Object,
                _mapperMock.Object);
        }

        [TestMethod]
        public void GetAllManagersAsync_ReturnsCollectionOfManager_IfAnyExist()
        {
            //Arrange
            var collection = new List<Manager>
            {
                new Manager
                {
                    Id = 1,
                    ManagerName = "Name",
                    Surname = "Surname",
                    SalesArea = new SalesArea()
                }
            };

            var collectionModel = new List<ManagerModel>
            {
                new ManagerModel()
                {
                    Id = 1,
                    ManagerName = "Name",
                    Surname = "Surname",
                    SalesArea = new SalesAreaModel()
                }
            };

            _managerRepositoryMock
                .Setup(r => r.GetAllManagersCollectionAsync())
                .ReturnsAsync(collection);
            _mapperMock.Setup(r => r.Map<IEnumerable<ManagerModel>>(
                It.IsAny<IEnumerable<Manager>>())).Returns(collectionModel);

            //Act
            var result = _managerService.GetAllManagersAsync();

            //Assert
            Assert.AreEqual(collectionModel, result.Result);
        }

        [TestMethod]
        public void GetManagerByIdAsync_ReturnsManager_IfExist()
        {
            //Arrange
            var manager = new Manager
            {
                Id = 1,
                ManagerName = "Name",
                Surname = "Surname",
                SalesArea = new SalesArea()
            };

            var managerModel = new ManagerModel()
            {
                Id = 1,
                ManagerName = "Name",
                Surname = "Surname",
                SalesArea = new SalesAreaModel()
            };

            _managerRepositoryMock
                .Setup(r => r.GetManagerByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(manager);
            _mapperMock.Setup(r => r.Map<ManagerModel>(
                It.IsAny<Manager>())).Returns(managerModel);

            //Act
            var result = _managerService.GetManagerByIdAsync(1);

            //Assert
            Assert.AreEqual(managerModel, result.Result);
        }

        [TestMethod]
        public void GetManagerByIdAsync_ThrowsNullReferenceException_IfNotExist()
        {
            //Arrange
            Manager manager = null;

            _managerRepositoryMock
                .Setup(r => r.GetManagerByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(manager);

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _managerService.GetManagerByIdAsync(It.IsAny<long>()));
        }

        [TestMethod]
        public void CreateManagerAsync_CreatesManager_IfModelIsValid()
        {
            //Arrange
            var manager = new ManagerModel
            {
                Id = 1,
                ManagerName = "Name",
                Surname = "Surname",
                SalesArea = new SalesAreaModel()
            };

            _managerRepositoryMock
                .Setup(r => r.CreateManagerAsync(It.IsAny<Manager>()));

            //Act
            _managerService.CreateManagerAsync(manager);

            //Assert
            _managerRepositoryMock.Verify(x=>x.CreateManagerAsync(It.IsAny<Manager>()),Times.Once);
        }

        [TestMethod]
        public void CreateManagerAsync_ThrowsArgumentNullException_IfModelIsNull()
        {
            //Arrange
            ManagerModel manager = null;

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _managerService.CreateManagerAsync(manager));
        }

        [TestMethod]
        public void UpdateManagerAsync_UpdatesManager_ItExists()
        {
            //Arrange
            var manager = new ManagerModel
            {
                Id = 1,
                ManagerName = "Name",
                Surname = "Surname",
                SalesArea = new SalesAreaModel()
            };

            _managerRepositoryMock
                .Setup(r => r.UpdateManagerAsync(It.IsAny<Manager>()));

            //Act
            _managerService.UpdateManagerAsync(manager);

            //Assert
            _managerRepositoryMock.Verify(x => x.UpdateManagerAsync(It.IsAny<Manager>()), Times.Once);
        }

        [TestMethod]
        public void UpdateManagerAsync_ThrowsArgumentNullException_IfModelIsNull()
        {
            //Arrange
            ManagerModel manager = null;

            //Assert
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _managerService.UpdateManagerAsync(manager));
        }
        
        [TestMethod]
        public void UpdateManagerAsync_ThrowsNullReferenceException_IfNotExist()
        {
            //Arrange
            var manager = new ManagerModel
            {
                Id = 1,
                ManagerName = "Name",
                Surname = "Surname",
                SalesArea = new SalesAreaModel()
            };

            _managerRepositoryMock.Setup(x => x.UpdateManagerAsync(It.IsAny<Manager>()))
                .Throws<NullReferenceException>();

            //Act
            
            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _managerService.UpdateManagerAsync(manager));
        }

        [TestMethod]
        public void RemoveManagerAsync_RemoveEntity_IfItExists()
        {
            //Arrange
            long id = 1;
            _managerRepositoryMock.Setup(x => x.DeleteManagerAsync(It.IsAny<long>()));

            //Act
            _managerService.RemoveManagerAsync(id);

            //Assert
            _managerRepositoryMock.Verify(x=>x.DeleteManagerAsync(It.IsAny<long>()),Times.Once);
        }

        [TestMethod]
        public void RemoveManagerAsync_ThrowsNullReferenceException_IfNotExists()
        {
            //Arrange
            long id = 1;
            _managerRepositoryMock.Setup(x => x.DeleteManagerAsync(It.IsAny<long>()))
                .Throws<NullReferenceException>();

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _managerService.RemoveManagerAsync(id));
        }
    }
}
