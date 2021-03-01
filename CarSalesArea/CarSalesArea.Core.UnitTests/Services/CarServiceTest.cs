using AutoMapper;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services;
using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace CarSalesArea.Core.UnitTests.Services
{
    [TestClass]
    public class CarServiceTest
    {
        private readonly ICarService _carService;
        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CarServiceTest()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _mapperMock = new Mock<IMapper>();
            _carService = new CarService(
                _carRepositoryMock.Object,
                _mapperMock.Object);
        }

        [TestMethod]
        public void GetAllCars_ReturnCollectionOfCars_IfAnyExist()
        {
            //Arrange
            var collection = new List<CarEntity>()
            {
                new CarEntity()
                {
                    Brand = "brand",
                    Model = "model",
                    Body = "BODY",
                    Color = "color",
                    Description = "description",
                    EngineVolume = 3.0,
                    Id = 1,
                    Mileage = 100000,
                    Price = 10000,
                    Year = DateTime.MinValue,
                    VinCode = "vin",
                    SalesArea = new SalesArea(),
                    FuelType = new FuelTypeEntity(),
                    Photos = new List<PhotoEntity>()
                }
            };

            var collectionModel = new List<CarModel>()
            {
                new CarModel()
                {
                    Brand = "brand",
                    Model = "model",
                    Body = "BODY",
                    Color = "color",
                    Description = "description",
                    EngineVolume = 3.0,
                    Id = 1,
                    Mileage = 100000,
                    Price = 10000,
                    Year = DateTime.MinValue,
                    VinCode = "vin",
                    SalesArea = new SalesAreaModel(),
                    FuelType = new FuelTypeModel(),
                    Photos = new List<PhotoModel>()
                }
            };

            var pagingOptions = new Core.Models.PagingOptions()
            {
                Limit = 10,
                Offset = 0
            };

            _carRepositoryMock.Setup(r => r.GetAllCarsCollectionAsync(It.IsAny<Data.Models.PagingOptions>()))
                .ReturnsAsync(collection);
            _mapperMock.Setup(r => r.Map<IEnumerable<CarModel>>(
                It.IsAny<IEnumerable<CarEntity>>())).Returns(collectionModel);

            //Act
            var result = _carService.GetAllCarsAsync(pagingOptions);

            //Assert
            Assert.AreEqual(collectionModel, result.Result);
        }

        [TestMethod]
        public void GetCarByIdAsync_ReturnsCar_IfItExist()
        {
            //Arrange
            var car = new CarEntity()
            {
                Brand = "brand",
                Model = "model",
                Body = "BODY",
                Color = "color",
                Description = "description",
                EngineVolume = 3.0,
                Id = 1,
                Mileage = 100000,
                Price = 10000,
                Year = DateTime.MinValue,
                VinCode = "vin",
                SalesArea = new SalesArea(),
                FuelType = new FuelTypeEntity(),
                Photos = new List<PhotoEntity>()
            };

            var carModel = new CarModel()
            {
                Brand = "brand",
                Model = "model",
                Body = "BODY",
                Color = "color",
                Description = "description",
                EngineVolume = 3.0,
                Id = 1,
                Mileage = 100000,
                Price = 10000,
                Year = DateTime.MinValue,
                VinCode = "vin",
                SalesArea = new SalesAreaModel(),
                FuelType = new FuelTypeModel(),
                Photos = new List<PhotoModel>()
            };

            _carRepositoryMock.Setup(r => r.GetCarByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(car);
            _mapperMock.Setup(r => r.Map<CarModel>(
                It.IsAny<CarEntity>())).Returns(carModel);

            //Act
            var result = _carService.GetCarByIdAsync(1);

            //Assert
            Assert.AreEqual(carModel, result.Result);
        }

        [TestMethod]
        public void GetCarByIdAsync_ThrowsNullReferenceException_IfNotExist()
        {
            //Arrange
            CarEntity carEntity = null;

            _carRepositoryMock
                .Setup(r => r.GetCarByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(carEntity);

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _carService.GetCarByIdAsync(It.IsAny<long>()));
        }

        [TestMethod]
        public void CreateCarAsync_CreatesCar_IfModelIsValid()
        {
            //Arrange
            var carModel = new CarModel()
            {
                Brand = "brand",
                Model = "model",
                Body = "BODY",
                Color = "color",
                Description = "description",
                EngineVolume = 3.0,
                Id = 1,
                Mileage = 100000,
                Price = 10000,
                Year = DateTime.MinValue,
                VinCode = "vin",
                SalesArea = new SalesAreaModel(),
                FuelType = new FuelTypeModel(),
                Photos = new List<PhotoModel>()
            };

            _carRepositoryMock
                .Setup(r => r.CreateManagerAsync(It.IsAny<CarEntity>()));

            //Act
            _carService.CreateCarAsync(carModel);

            //Assert
            _carRepositoryMock.Verify(x=>x.CreateManagerAsync(It.IsAny<CarEntity>()), Times.Once);
        }

        [TestMethod]
        public void CreateCarAsync_ThrowsArgumentNullException_IfModelIsNull()
        {
            //Arrange
            CarModel car = null;

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _carService.CreateCarAsync(car));
        }

        [TestMethod]
        public void UpdateCarAsync_UpdatesCar_IfItExists()
        {
            //Arrange
            var carModel = new CarModel()
            {
                Brand = "brand",
                Model = "model",
                Body = "BODY",
                Color = "color",
                Description = "description",
                EngineVolume = 3.0,
                Id = 1,
                Mileage = 100000,
                Price = 10000,
                Year = DateTime.MinValue,
                VinCode = "vin",
                SalesArea = new SalesAreaModel(),
                FuelType = new FuelTypeModel(),
                Photos = new List<PhotoModel>()
            };

            _carRepositoryMock
                .Setup(r => r.UpdateCarAsync(It.IsAny<CarEntity>()));

            //Act
            _carService.UpdateCarAsync(carModel);

            //Assert
            _carRepositoryMock.Verify(x => x.UpdateCarAsync(It.IsAny<CarEntity>()), Times.Once);
        }

        [TestMethod]
        public void UpdateCarAsync_ThrowsArgumentNullException_IfModelIsNull()
        {
            //Arrange
            CarModel car = null;

            //Assert
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _carService.UpdateCarAsync(car));
        }

        [TestMethod]
        public void UpdateCarAsync_ThrowsNullReferenceException_IfNotExist()
        {
            //Arrange
            var carModel = new CarModel()
            {
                Brand = "brand",
                Model = "model",
                Body = "BODY",
                Color = "color",
                Description = "description",
                EngineVolume = 3.0,
                Id = 1,
                Mileage = 100000,
                Price = 10000,
                Year = DateTime.MinValue,
                VinCode = "vin",
                SalesArea = new SalesAreaModel(),
                FuelType = new FuelTypeModel(),
                Photos = new List<PhotoModel>()
            };

            _carRepositoryMock.Setup(x => x.UpdateCarAsync(It.IsAny<CarEntity>()))
                .Throws<NullReferenceException>();

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _carService.UpdateCarAsync(carModel));
        }

        [TestMethod]
        public void RemoveCarAsync_RemoveEntity_IfItExists()
        {
            //Arrange
            long id = 1;
            _carRepositoryMock.Setup(x => x.DeleteCarAsync(It.IsAny<long>()));

            //Act
            _carService.RemoveCarAsync(id);

            //Assert
            _carRepositoryMock.Verify(x => x.DeleteCarAsync(It.IsAny<long>()), Times.Once);
        }

        [TestMethod]
        public void RemoveManagerAsync_ThrowsNullReferenceException_IfNotExists()
        {
            //Arrange
            long id = 1;
            _carRepositoryMock.Setup(x => x.DeleteCarAsync(It.IsAny<long>()))
                .Throws<NullReferenceException>();

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _carService.RemoveCarAsync(id));
        }
    }
}
