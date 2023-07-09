using Library.BikeApplication;
using Library.BikeApplication.Interface;
using Library.BikeApplication.Model;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Runtime.InteropServices.JavaScript;

namespace XUnitTest.BikeApplication
{
    /// <summary>
    /// Unit tests for the BikeService class.
    /// </summary>
    public class BikeServiceTest
    {
        private readonly Mock<IBikeService> _mockBikeService;
        public BikeServiceTest()
        {
            _mockBikeService = new Mock<IBikeService>();
        }
        /// <summary>
        /// Test for getting all bikes successfully.
        /// </summary>
        [Fact]
        public void GetAllBikes_Successful()
        {
            _mockBikeService.Setup(bikeService => bikeService.GetBikes()).Returns(
                new Result<List<Bike>>
                {
                    Success = true
                });

            var bikeService = _mockBikeService.Object;

            Result<List<Bike>> result = bikeService.GetBikes();

            Assert.True(result.Success);
        }
        /// <summary>
        /// Test for getting all bikes successfully when is not null.
        /// </summary>
        [Fact]
        public void GetAllBikes_NotNull()
        {
            var bikes = new List<Bike>()
            {
                new Bike
                {
                    Model = "Lectric eBikes",
                    Make = "Lectric Xp 3.0",
                    Color = "Yellow",
                    Type = "City",
                    Size = "Small"
                },
                new Bike
                {
                    Model = "Lectric eBikes",
                    Make = "Lectric Xp 3.0",
                    Color = "Yellow",
                    Type = "City",
                    Size = "Small"
                }
            };


            _mockBikeService.Setup(bikeService => bikeService.GetBikes()).Returns(
                new Result<List<Bike>>
                {
                    Success = true,
                    Data = bikes
                });

            var bikeService = _mockBikeService.Object;

            Result<List<Bike>> result = bikeService.GetBikes();

            Assert.NotNull(result.Data);
        }
        /// <summary>
        /// Test to add bike successfully.
        /// </summary>
        [Fact]
        public void AddBike_Successful()
        {
            var newBike = new Bike
            {
                Model = "Lectric eBikes",
                Make = "Lectric Xp 3.0",
                Color = "Yellow",
                Type = "City",
                Size = "Small"
            };
            _mockBikeService.Setup(bikeService => bikeService.AddBike(newBike)).Returns(
                new Result<Bike>() { Success = true, Data = newBike });

            var bikeService = _mockBikeService.Object;

            Result<Bike> result = bikeService.AddBike(newBike);

            Assert.True(result.Success);
        }
        /// <summary>
        /// Test to verify added Bike data.
        /// </summary>
        [Fact]
        public void AddBike_SameDataInserted()
        {
            var newBike = new Bike
            {
                Model = "Lectric eBikes",
                Make = "Lectric Xp 3.0",
                Color = "Yellow",
                Type = "City",
                Size = "Small"
            };
            _mockBikeService.Setup(bikeService => bikeService.AddBike(It.IsAny<Bike>())).Returns(
                new Result<Bike>() { Success = false , Data = newBike});

            var bikeService = _mockBikeService.Object;

            Result<Bike> result = bikeService.AddBike(newBike);

            Assert.Equal(newBike, result.Data);
        }
        /// <summary>
        /// Test in DeleteBike when bike doesn't exist.
        /// </summary>
        [Fact]
        public void DeleteBike_NotExisting()
        {
            int id = 5;
           
            _mockBikeService.Setup(bikeService => bikeService.DeleteBikeById(id)).Returns(
                new Result<Bike>() { Success = false , ErrorMessage = "Bike was not found"});

            var bikeService = _mockBikeService.Object;

            Result<Bike> result = bikeService.DeleteBikeById(id);

            Assert.Equal("Bike was not found", result.ErrorMessage);
        }
        /// <summary>
        /// Test in DeleteBike when bike doesn't have same id.
        /// </summary>
        [Fact]
        public void DeleteBike_NotEqualId()
        {
            int id = 5;
            var expectedBike = new Bike
            {
                Id = 3,
                Model = "Test",
                Make = "Lectric Xp 3.0",
                Color = "Yellow",
                Type = "City",
                Size = "Small"
            };

            _mockBikeService.Setup(bikeService => bikeService.DeleteBikeById(id)).Returns(
                new Result<Bike>() { Success = true, Data = expectedBike });

            var bikeService = _mockBikeService.Object;

            Result<Bike> result = bikeService.DeleteBikeById(id);

            Assert.NotEqual(id, result.Data.Id);
        }
        /// <summary>
        /// Test in EditBike when expectedBike and resultBike have same ID.
        /// </summary>
        [Fact]
        public void EditBike_EqualId()
        {
            int id = 3;
            var editedBike = new Bike
            {
                Id = 3,
                Model = "Test",
                Make = "Lectric Xp 3.0",
                Color = "Yellow",
                Type = "City",
                Size = "Small"
            };

            _mockBikeService.Setup(bikeService => bikeService.EditBikeById(id,editedBike)).Returns(
                new Result<Bike>() { Success = false, Data = editedBike});

            var bikeService = _mockBikeService.Object;

            Result<Bike> result = bikeService.EditBikeById(id,editedBike);

            Assert.Equal(id, result.Data.Id);
        }
        /// <summary>
        /// Test in EditBike when Model value in resultBike is empty.
        /// </summary>
        [Fact]
        public void EditBike_EmptyModelValue()
        {
            int id = 3;
            var editedBike = new Bike
            {
                Id = 3,
                Model = "",
                Make = "Lectric Xp 3.0",
                Color = "Yellow",
                Type = "City",
                Size = "Small"
            };

            _mockBikeService.Setup(bikeService => bikeService.EditBikeById(id, editedBike)).Returns(
                new Result<Bike>() { Success = true, Data = editedBike });

            var bikeService = _mockBikeService.Object;

            Result<Bike> result = bikeService.EditBikeById(id, editedBike);
            Assert.Empty(result.Data.Model);
        }
        ///<summary>
        /// Test for getting a bike by ID and checking if the Make value starts with "T".
        /// </summary>
        [Fact]
        public void GetBikeById_EqualData()
        {
            int id = 5;
            Bike expectedBike = new Bike
            {
                Id = id, 
                Make = "Test Make", 
                Model = "Test Model",
                Color = "Test Color", 
                Type = "Test Type", 
                Size = "Test Size"
            };


            _mockBikeService.Setup(bikeService => bikeService.GetBikeById(id)).Returns(
                new Result<Bike>() { Success = false,Data = expectedBike});

            var bikeService = _mockBikeService.Object;

            Result<Bike> result = bikeService.GetBikeById(id);

            Assert.Equal(expectedBike, result.Data);
        }
        ///<summary>
        /// Test for getting a bike by ID and checking if expected data is same as result data.
        /// </summary>
        [Fact]
        public void GetBikeById_MakeValueStartsWith()
        {
            int id = 5;
            Bike expectedBike = new Bike
            {
                Id = id,
                Make = "Test Make",
                Model = "Test Model",
                Color = "Test Color",
                Type = "Test Type",
                Size = "Test Size"
            };
            _mockBikeService.Setup(bikeService => bikeService.GetBikeById(id)).Returns(
                new Result<Bike>() { Success = false, Data = expectedBike });

            var bikeService = _mockBikeService.Object;

            Result<Bike> result = bikeService.GetBikeById(id);

            Assert.StartsWith("T",result.Data.Make);
        }
    }
}