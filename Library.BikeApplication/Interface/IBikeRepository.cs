using Library.BikeApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BikeApplication.Interface
{
    public interface IBikeRepository
    {

        /// <summary>
        /// Saves the list of bikes to the file.
        /// </summary>
        /// <param name="bikes">The list of bikes to save.</param>
        public void SaveBikesToFile(List<Bike> bikes);
        /// <summary>
        /// Loads the list of bikes from the file.
        /// </summary>
        /// <returns>The list of bikes loaded from the file.</returns>
        public List<Bike> LoadBikesFromFile();
        /// <summary>
        /// Generates an ID for the new bike.
        /// </summary>
        /// <param name="bikes">The list of existing bikes.</param>
        /// <returns>The generated int ID.</returns>
        public int GetNextId(List<Bike> bikes);
        /// <summary>
        /// Create  a Directory if it doesnt Exist.
        /// </summary>
        /// <param name="directoryPath">The path where this directory should be created.</param>
        public void CreateDirectory(string directoryPath);

        /// <summary>
        /// Create  a File if it doesnt Exist.
        /// </summary>
        /// <param name="filePath">The path where this file should be created.</param>
        public void CreateFile(string filePath);
        
    }
}
