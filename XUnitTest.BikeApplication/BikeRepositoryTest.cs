using Library.BikeApplication;
using Library.BikeApplication.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Library.BikeApplication.Model;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace XUnitTest.BikeApplication
{
    /// <summary>
    /// Test units to check class BikeRepository.
    /// </summary>
    public class BikeRepositoryTest
    {
        private readonly Mock<IBikeRepository> _mockBikeRepository;
        private string filePath;
        private string directoryPath;

        public BikeRepositoryTest()
        {
            string folderPath = "FileStorage\\";
            string directoryProjPath = Directory.GetCurrentDirectory();
            directoryPath = Path.Combine(directoryProjPath, folderPath);
            filePath = Path.Combine(directoryPath, "bike.json");
            _mockBikeRepository = new Mock<IBikeRepository>();
        }

        /// <summary>
        /// Test to check if Directory Exists.
        /// </summary>
        [Fact]
        public void CreateDirectory_Exist()
        {
            string directoryPath =
                @"C:\Users\ndous\source\repos\Web.BikeApplication\Library.BikeApplication\FileStorage\";

            var bikeRepository = _mockBikeRepository.Object;

            bikeRepository.CreateDirectory(directoryPath);

            Assert.True(Directory.Exists(directoryPath));
        }

        /// <summary>
        /// Test to check if File Exists.
        /// </summary>
        [Fact]
        public void CreateFile_Exist()
        {
            var bikeRepository = _mockBikeRepository.Object;

            bikeRepository.CreateFile(filePath);

            Assert.True(File.Exists(filePath));
        }

        /// <summary>
        /// Test to check if next id is the one expected.
        /// </summary>
        [Fact]
        public void GetNextId_NonEqualValue_ReturnsNextId()
        {
            var bikes = new List<Bike>
            {
                new Bike
                {
                    Id = 1,
                    Make = "Test",
                    Model = "TestModel",
                    Color = "Red",
                    Type = "Mountain",
                    Size = "L"
                },
                new Bike
                {
                    Id = 2,
                    Make = "Test",
                    Model = "TestModel",
                    Color = "Red",
                    Type = "Mountain",
                    Size = "L"
                },
                new Bike
                {
                    Id = 3,
                    Make = "Test",
                    Model = "TestModel",
                    Color = "Red",
                    Type = "Mountain",
                    Size = "L"
                }
            };
            _mockBikeRepository.Setup(repo => repo.GetNextId(bikes)).Returns(9);
            var bikeRepository = _mockBikeRepository.Object;

            int result = bikeRepository.GetNextId(bikes);

            Assert.NotEqual(4, result);
        }

        /// <summary>
        /// Unit test for the SaveBikesToFile method that verifies the bikes are saved to the file in JSON format.
        /// </summary>
        [Fact]
        public void SaveBikesToFile_SavesBikesToFile()
        {
            var bikes = new List<Bike>
            {
                new Bike
                {
                    Id = 1,
                    Make = "Test",
                    Model = "TestModel",
                    Color = "Red",
                    Type = "Mountain",
                    Size = "L"
                },
                new Bike
                {
                    Id = 2,
                    Make = "Test",
                    Model = "TestModel",
                    Color = "Red",
                    Type = "Mountain",
                    Size = "L"
                },
                new Bike
                {
                    Id = 3,
                    Make = "Test",
                    Model = "TestModel",
                    Color = "Red",
                    Type = "Mountain",
                    Size = "L"
                }
            };

            _mockBikeRepository.Setup(repo => repo.SaveBikesToFile(bikes));

            var bikeRepository = _mockBikeRepository.Object;

            bikeRepository.SaveBikesToFile(bikes);

            Assert.True(File.Exists(filePath));

            string json = File.ReadAllText(filePath);

            var savedBikes = System.Text.Json.JsonSerializer.Deserialize<List<Bike>>(json);

            Assert.Equal(bikes.Count, savedBikes.Count);
        }

        /// <summary>
        /// Unit test for the SaveBikesToFile method that verifies the number of files retrieved from JSON format, but before check if they are not.
        /// </summary>
        [Fact]
        public void LoadBikesFromFile_ReturnsBikesFromFile()
        {
            var bikes = new List<Bike>
            {
                new Bike
                {
                    Id = 1,
                    Make = "Test",
                    Model = "TestModel",
                    Color = "Red",
                    Type = "Mountain",
                    Size = "L"
                },
                new Bike
                {
                    Id = 2,
                    Make = "Test",
                    Model = "TestModel",
                    Color = "Red",
                    Type = "Mountain",
                    Size = "L"
                },
                new Bike
                {
                    Id = 3,
                    Make = "Test",
                    Model = "TestModel",
                    Color = "Red",
                    Type = "Mountain",
                    Size = "L"
                }
            };

            _mockBikeRepository.Setup(repo => repo.LoadBikesFromFile()).Returns(
                bikes);

            var bikeRepository = _mockBikeRepository.Object;

            string json = System.Text.Json.JsonSerializer.Serialize(bikes);

            File.WriteAllText(filePath, json);


            var result = bikeRepository.LoadBikesFromFile();

            Assert.NotNull(result);

            Assert.Equal(bikes.Count, result.Count);

        }

    }
}
