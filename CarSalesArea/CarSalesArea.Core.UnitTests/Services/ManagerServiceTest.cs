using System;
using CarSalesArea.Core.Services;
using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using NuGet.Frameworks;

namespace CarSalesArea.Core.UnitTests.Services
{
    [TestClass]
    public class ManagerServiceTest
    {
        private readonly IManagerService _managerService;
        private readonly Mock<IManagerRepository> _managerRepositoryMock;

        public ManagerServiceTest()
        {
            _managerRepositoryMock = new Mock<IManagerRepository>();
            _managerService = new ManagerService(_managerRepositoryMock.Object);
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

            _managerRepositoryMock
                .Setup(r => r.GetAllManagersCollectionAsync())
                .ReturnsAsync(collection);

            //Act
            var result = _managerService.GetAllManagersAsync();

            //Assert
            Assert.AreEqual(collection, result.Result);
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

            _managerRepositoryMock
                .Setup(r => r.GetManagerByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(manager);

            //Act
            var result = _managerService.GetManagerByIdAsync(1);

            //Assert
            Assert.AreEqual(manager, result);
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
            Assert.ThrowsException<NullReferenceException>(() => _managerService.GetManagerByIdAsync(1));
        }

        [TestMethod]
        public void CreateManagerAsync_CreatesManager_IfModelIsValid()
        {
            //Arrange
            var manager = new Manager
            {
                Id = 1,
                ManagerName = "Name",
                Surname = "Surname",
                SalesArea = new SalesArea()
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
            Manager manager = null;

            //Act

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => _managerService.CreateManagerAsync(manager));
        }

        [TestMethod]
        public void UpdateManagerAsync_UpdatesManager_ItExists()
        {
            //Arrange
            var manager = new Manager
            {
                Id = 1,
                ManagerName = "Name",
                Surname = "Surname",
                SalesArea = new SalesArea()
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
            Manager manager = null;

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => _managerService.UpdateManagerAsync(manager));
        }
        
        [TestMethod]
        public void UpdateManagerAsync_ThrowsNullReferenceException_IfNotExist()
        {
            //Arrange
            var manager = new Manager
            {
                Id = 1,
                ManagerName = "Name",
                Surname = "Surname",
                SalesArea = new SalesArea()
            };

            _managerRepositoryMock.Setup(x => x.UpdateManagerAsync(It.IsAny<Manager>()))
                .Throws<NullReferenceException>();

            //Act
            
            //Assert
            Assert.ThrowsException<NullReferenceException>(() => _managerService.CreateManagerAsync(manager));
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
            Assert.ThrowsException<NullReferenceException>(() => _managerService.RemoveManagerAsync(id));
        }
    }
}
