using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BikeApplication.Model
{
    public class Bike
    {
        /// <summary>
        /// Gets or sets the Id of the Bike.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Make of the Bike.
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Gets or sets the Model of the Bike.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the Color of the Bike.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the Type of the Bike.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Size of the Bike.
        /// </summary>
        public string Size { get; set; }


    }

}
