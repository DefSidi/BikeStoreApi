using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BikeApplication
{
    public class Result<T>
    {
        /// <summary>
        /// Gets or sets ErrorMessage of the Result
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        ///  Gets or sets the Success of the Result.
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Gets or sets the Data of the Result.
        /// </summary>
        public T Data { get; set; }


    }
}
