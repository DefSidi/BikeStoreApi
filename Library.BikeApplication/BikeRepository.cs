using Library.BikeApplication.Interface;
using Library.BikeApplication.Model;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using log4net;
using System.Reflection;
using log4net.Repository.Hierarchy;

namespace Library.BikeApplication
{
    /// <summary>
    /// Implementation of the JSON persistence for the Bike repository.
    /// </summary>
    public class BikeRepository : IBikeRepository
    {
        private readonly string directoryPath;
        private readonly string filePath;
        private List<Bike> bikes;
        private readonly ILogger _logger;

        public BikeRepository()
        {
        }

        public BikeRepository(ILogger logger)
        {
            string folderPath = "FileStorage\\";
            string directoryProjPath = Directory.GetCurrentDirectory();
            directoryPath = Path.Combine(directoryProjPath, folderPath);
            CreateDirectory(directoryPath);
            filePath = Path.Combine(directoryPath, "data.json");
            CreateFile(filePath);
            bikes = LoadBikesFromFile();
            _logger = logger;
        }

        /// <summary>
        /// Saves the bike into JSON file.
        /// </summary>
        /// <param name="bikeList">The list of  bikes to be saved.</param>
        public void SaveBikesToFile(List<Bike> bikeList)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                if (File.Exists(filePath))
                {
                    string json =
                        JsonSerializer.Serialize(bikeList, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(filePath, json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("METHOD - {0} : ERROR MESSAGE - {1} ", methodName, ex.Message));
                throw;
            }
        }

        /// <summary>
        /// Method to retrieve all Bikes from the file.
        /// </summary>
        /// <returns> A List of Bikes from file JSON or empty list.</returns>
        public List<Bike> LoadBikesFromFile()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    if (!string.IsNullOrEmpty(json))
                        return JsonSerializer.Deserialize<List<Bike>>(json);

                }
                else
                {
                    return new List<Bike>();
                }

                return new List<Bike>();
            }

            catch (Exception ex)
            {
                _logger.LogError(String.Format("METHOD - {0} : ERROR MESSAGE - {1} ", methodName, ex.Message));
                throw;
            }
        }
       
        /// <summary>
        /// Generates an ID for the new bike.
        /// </summary>
        /// <param name="bikes">The list of existing bikes.</param>
        /// <returns>The generated int ID.</returns>
        public int GetNextId(List<Bike> bikes)
        {
            if (bikes.Count > 0)
            {
                return bikes[bikes.Count - 1].Id + 1;
            }
            else
            {
                return 1;
            }
        }
       
        /// <summary>
        /// Create  a Directory if it doesnt Exist.
        /// </summary>
        /// <param name="directoryPath">The path where this directory should be created.</param>
        public void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        
        /// <summary>
        /// Create  a File if it doesnt Exist.
        /// </summary>
        /// <param name="filePath">The path where this file should be created.</param>
        public void CreateFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                {
                }
            }
        }


    }
}
