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
    public class MediaServiceTest
    {
        private readonly IMediaService _mediaService;
        private readonly Mock<IMediaRepository> _mediaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public MediaServiceTest()
        {
            _mediaRepositoryMock = new Mock<IMediaRepository>();
            _mapperMock = new Mock<IMapper>();
            _mediaService = new MediaService(
                _mediaRepositoryMock.Object,
                _mapperMock.Object);
        }

        [TestMethod]
        public void GetAllPhotoAsync_ReturnsCollectionOfPhoto_IfAnyExist()
        {
            //Arrange
            var collection = new List<PhotoEntity>()
            {
                new PhotoEntity()
                {
                    Id = 1,
                    PhotoPath = "path",
                    Car = new CarEntity()
                }
            };

            var collectionModel = new List<PhotoModel>()
            {
                new PhotoModel()
                {
                    Id = 1,
                    PhotoPath = "path",
                    Car = new CarModel()
                }
            };

            _mediaRepositoryMock
                .Setup(r => r.GetAllPhotosCollectionAsync())
                .ReturnsAsync(collection);
            _mapperMock
                .Setup(r => r.Map<IEnumerable<PhotoModel>>(
                    It.IsAny<IEnumerable<PhotoEntity>>())).Returns(collectionModel);

            //Act
            var result = _mediaService.GetAllPhotosCollectionAsync();
            
            //Assert
            Assert.AreEqual(collectionModel, result.Result);
        }

        [TestMethod]
        public void GetPhotoCollectionByCarId_ReturnsCollecitonOfPhoto_IfAnyExist()
        {
            //Arrange
            var collection = new List<PhotoEntity>()
            {
                new PhotoEntity()
                {
                    Id = 1,
                    PhotoPath = "path",
                    Car = new CarEntity()
                }
            };

            var collectionModel = new List<PhotoModel>()
            {
                new PhotoModel()
                {
                    Id = 1,
                    PhotoPath = "path",
                    Car = new CarModel()
                }
            };

            _mediaRepositoryMock
                .Setup(r => r.GetPhotoCollectionByCarIdAsync(It.IsAny<long>()))
                .ReturnsAsync(collection);
            _mapperMock
                .Setup(r => r.Map<IEnumerable<PhotoModel>>(
                    It.IsAny<IEnumerable<PhotoEntity>>())).Returns(collectionModel);

            //Act
            var result = _mediaService.GetPhotoCollectionByCarIdAsync(1);

            //Assert
            Assert.AreEqual(collectionModel, result.Result);
        }

        [TestMethod]
        public void GetPhotoCollectionByCarId_ThrowsNullReferenceException_IfAnyExist()
        {
            //Arrange
            IEnumerable<PhotoEntity> photoEntity = null;

            _mediaRepositoryMock
                .Setup(r => r.GetPhotoCollectionByCarIdAsync(It.IsAny<long>()))
                .ReturnsAsync(photoEntity);

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _mediaService.GetPhotoCollectionByCarIdAsync(It.IsAny<long>()));
        }

        [TestMethod]
        public void CreatePhotoAsync_CreatePhoto_IfModelIsValid()
        {
            //Arrange
            var photoModel = new PhotoModel()
            {
                Id = 1,
                PhotoPath = "path",
                Car = new CarModel()
            };

            _mediaRepositoryMock
                .Setup(r => r.CreatePhotoAsync(It.IsAny<PhotoEntity>()));

            //Act
            _mediaService.CreatePhotoAsync(photoModel);

            //Assert
            _mediaRepositoryMock.Verify(x=>x.CreatePhotoAsync(It.IsAny<PhotoEntity>()));
        }

        [TestMethod]
        public void CreatePhotoAsync_ThrowsArgumentNullException_IfModelIsNull()
        {
            //Arrange
            PhotoModel photo = null;

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _mediaService.CreatePhotoAsync(photo));
        }

        [TestMethod]
        public void UpdatePhotoAsync_UpdatesPhoto_IfItExists()
        {
            //Arrange
            var photoModel = new PhotoModel()
            {
                Id = 1,
                PhotoPath = "path",
                Car = new CarModel()
            };

            _mediaRepositoryMock
                .Setup(r => r.UpdatePhotoAsync(It.IsAny<PhotoEntity>()));

            //Act
            _mediaService.UpdatePhotoAsync(photoModel);

            //Assert
            _mediaRepositoryMock.Verify(x=>x.UpdatePhotoAsync(It.IsAny<PhotoEntity>()), Times.Once);
        }

        [TestMethod]
        public void UpdatePhotoAsyn_ThrowsArgumentNullException_IfModelIsNull()
        {
            //Arrange
            PhotoModel photo = null;

            //Assert
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _mediaService.UpdatePhotoAsync(photo));
        }

        [TestMethod]
        public void UpdatePhotoAsync_ThrowsNullReferenceException_IfNotExist()
        {
            //Arrange
            var photoModel = new PhotoModel()
            {
                Id = 1,
                PhotoPath = "path",
                Car = new CarModel()
            };

            _mediaRepositoryMock.Setup(x => x.UpdatePhotoAsync(It.IsAny<PhotoEntity>()))
                .Throws<NullReferenceException>();

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _mediaService.UpdatePhotoAsync(photoModel));
        }

        [TestMethod]
        public void DeletePhotoAsync_RemoveEntity_IfItExist()
        {
            //Arrange
            long id = 1;
            _mediaRepositoryMock
                .Setup(x => x.DeletePhotoAsync(It.IsAny<long>()));

            //Act
            _mediaService.DeletePhotoAsync(id);

            //Assert
            _mediaRepositoryMock.Verify(x=>x.DeletePhotoAsync(It.IsAny<long>()),Times.Once);
        }

        [TestMethod]
        public void DeletePhotoAsync_ThrowsNullReferenceException_IfNotExists()
        {
            //Arrange
            long id = 1;
            _mediaRepositoryMock
                .Setup(x => x.DeletePhotoAsync(It.IsAny<long>()))
                .Throws<NullReferenceException>();

            //Act

            //Assert
            Assert.ThrowsExceptionAsync<NullReferenceException>(() => _mediaService.DeletePhotoAsync(id));
        }
    }
}
