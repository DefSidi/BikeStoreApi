using Library.BikeApplication.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.BikeApplication;
using Library.BikeApplication.Model;
using Microsoft.AspNetCore.Mvc;
using Web.BikeApplication.Controllers;

namespace XUnitTest.BikeApplication
{
    /// <summary>
    /// Unit tests for the BikeController class.
    /// </summary>
    public class BikeControllerTest
    {
       
        private readonly Mock<IBikeService> _mockBikeService;
        public BikeControllerTest()
        {
            _mockBikeService = new Mock<IBikeService>();
        }
        /// <summary>
        /// Unit Test To check OkResult in GetBikes.
        /// </summary>
        [Fact]
        public void GetBikes_ReturnsOkResult_WhenSuccess()
        {
            var expectedResult = new Result<List<Bike>>
            {
                Success = true,
                Data = new List<Bike>()
            };
            _mockBikeService.Setup(service => service.GetBikes())
                .Returns(expectedResult);

            var controller = new BikeController(_mockBikeService.Object);

            var actionResult = controller.GetBikes();

            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsType<Result<List<Bike>>>(okResult.Value);
            Assert.Equal(result.Success , true);
        }
        /// <summary>
        /// Unit Test To check Empty Data in OkResult in GetBikes.
        /// </summary>
        [Fact]
        public void GetBikes_ReturnsOkResult_NotEmptyData()
        {
            var expectedResult = new Result<List<Bike>>
            {
                Success = true,
                Data = new List<Bike>()
                {
                    new Bike
                    {
                        Id = 3,
                        Make = "Test",
                        Model = "TestModel",
                        Color = "Red",
                        Type = "Mountain",
                        Size = "L"
                    },
                    new Bike
                    {
                        Id = 4,
                        Make = "Test",
                        Model = "TestModel",
                        Color = "Red",
                        Type = "Mountain",
                        Size = "L"
                    }
                }
            };
            _mockBikeService.Setup(service => service.GetBikes())
                .Returns(expectedResult);

            var controller = new BikeController(_mockBikeService.Object);

            var actionResult = controller.GetBikes();

            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsType<Result<List<Bike>>>(okResult.Value);
            Assert.NotEmpty(result.Data);
        }
        /// <summary>
        /// Unit Test To check BadRequest in GetBike.
        /// </summary>
        [Fact]
        public void GetBikeById_ReturnsBadRequest()
        {
            int id = 10;
            _mockBikeService.Setup(service => service.GetBikeById(id))
                .Throws(new Exception("An error occurred"));

            var controller = new BikeController(_mockBikeService.Object);

            var actionResult = controller.GetBikeById(id);

            var badResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal("An error occurred", badResult.Value);
        }
        /// <summary>
        ///  Unit Test To check Equal Make in GetBike.
        /// </summary>
        [Fact]
        public void GetBikeById_EqualMake()
        {
            int id = 1;
            var expectedBike = new Bike
            {
                Id = 1,
                Make = "Test",
                Model = "TestModel",
                Color = "Red",
                Type = "Mountain",
                Size = "L"
            };
            _mockBikeService.Setup(service => service.GetBikeById(id))
                .Returns(new Result<Bike>()
                {
                    Success = true,
                    Data = expectedBike
                });

            var controller = new BikeController(_mockBikeService.Object);

            var actionResult = controller.GetBikeById(id);

            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsType<Result<Bike>>(okResult.Value);
            Assert.Equal("Test", result.Data.Make);
        }
        /// <summary>
        ///  Unit Test To check BadRequest when invalid input in SaveBike.
        /// </summary>
        [Fact]
        public void SaveBike_ReturnsBadRequestResponseCode_WhenInvalidInput()
        {
            var bike = new Bike
            {
                Id = 1,
                Make = "Test",
                Model = "",
                Color = "Red",
                Type = "Mountain",
                Size = "L"
            };
            var controller = new BikeController(_mockBikeService.Object);
            controller.ModelState.AddModelError("Model", "The Model field is required.");

            var actionResult = controller.AddBike(bike);
            
            var badResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal(400, badResult.StatusCode);
        }
        /// <summary>
        /// Unit Test To check Not adding bike in SaveBike.
        /// </summary>
        [Fact]
        public void SaveBike_NotAddingBike()
        {
            var bike = new Bike
            {
                Id = 1,
                Make = "Test",
                Model = "TestModel",
                Color = "Red",
                Type = "Mountain",
                Size = "L"
            };

            _mockBikeService.Setup(service => service.AddBike(bike))
                .Returns(new Result<Bike>()
                {
                    ErrorMessage = "Couldn't be added the new bike"
                });

            var controller = new BikeController(_mockBikeService.Object);
            

            var actionResult = controller.AddBike(bike);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            
            Assert.Equal("Couldn't be added the new bike" , notFoundResult.Value);
        }
        /// <summary>
        /// To check not found result in DeleteBike.
        /// </summary>
        [Fact]
        public void DeleteBike_NotFoundNullResult()
        {
            int id = 9;
            _mockBikeService.Setup(service => service.DeleteBikeById(id))
                .Returns(new Result<Bike>());

            var controller = new BikeController(_mockBikeService.Object);

            var actionResult = controller.DeleteBike(id);

            
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Null(notFoundResult.Value);
        }
        /// <summary>
        /// Unit Test to check SuccessResult in DeleteBike.
        /// </summary>
        [Fact]
        public void DeleteBike_SuccessResult()
        {
            int id = 9;
            _mockBikeService.Setup(service => service.DeleteBikeById(id))
                .Returns(new Result<Bike>()
                {
                    Success = true
                });

            var controller = new BikeController(_mockBikeService.Object);

            var actionResult = controller.DeleteBike(id);

             Assert.IsType<OkObjectResult>(actionResult);
        }
        /// <summary>
        /// Unit Test to Check EditedSuccessfully in  EditBike.
        /// </summary>
        [Fact]
        public void EditBike_BikeEditedSuccessfully()
        {
            _mockBikeService.Setup(service => service.EditBikeById(It.IsAny<int>(), It.IsAny<Bike>()))
                .Returns(new Result<Bike> { Success = true });
            var controller = new BikeController(_mockBikeService.Object);

            var result = controller.EditBike(1, new Bike());

            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// Unit Test to check bad Request in EditBike
        /// </summary>
        [Fact]
        public void EditBike_BadRequestEditedBike()
        {

            var bike = new Bike
            {
                Id = 1,
                Make = "Test",
                Model = "TestModel",
                Color = "Red",
                Type = "Mountain",
                Size = "L"
            };
            _mockBikeService.Setup(service => service.EditBikeById(It.IsAny<int>(),bike))
                .Returns(new Result<Bike> { Success = true , Data = bike});
            var controller = new BikeController(_mockBikeService.Object);

            var result = controller.EditBike(1, bike);

            Assert.IsNotType<BadRequestObjectResult>(result);
        }
    }

}
