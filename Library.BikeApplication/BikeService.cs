using Library.BikeApplication.Model;
using Library.BikeApplication.Interface;
using System.Text.Json;
using System.IO;
using System.Linq.Expressions;
using System.Xml.Serialization;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Reflection;
using log4net;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Library.BikeApplication
{
    /// <summary>
    /// Service class for managing bike operations.
    /// </summary>
    public class BikeService : IBikeService
    {
        private List<Bike> bikeList;
        private readonly ILogger _logger;
        private readonly IBikeRepository _bikeRepository;

        public BikeService()
        {
        }
        public BikeService(ILogger logger, IBikeRepository bikeRepository)
        {
            _logger = logger;
            _bikeRepository = bikeRepository;
            bikeList = _bikeRepository.LoadBikesFromFile();
        }

        #region CRUD

        /// <summary>
        /// Gets all Bikes in alphabetic order by Model.
        /// </summary>
        /// <returns>A list of bikes and a successful result in case they exist ,
        /// or an error message if they doesn't exist.</returns>
        public Result<List<Bike>> GetBikes()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation(String.Format("START METHOD - {0}", methodName));
                var bikes = _bikeRepository.LoadBikesFromFile();
                if (bikes.Count > 0)
                {
                    return new Result<List<Bike>>
                    {
                        Success = true,
                        Data = bikes.OrderBy(b => b.Model).ToList()
                    };
                }
                else
                {
                    return new Result<List<Bike>>
                    {
                        Success = false,
                        ErrorMessage = "There are no bike products."
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("METHOD - {0} : ERROR MESSAGE - {1}", methodName, ex.Message));
                return new Result<List<Bike>>
                {
                    Success = false,
                    ErrorMessage = ex.Message

                };
            }

        }
        /// <summary>
        ///The service that gets one Bike by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A bike and a successful result in case it exists ,
        /// or an error message if it doesn't exist.</returns>
        public Result<Bike> GetBikeById(int id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation(String.Format("START METHOD - {0}", methodName));
                if (id > 0)
                {
                    var bike = bikeList.FirstOrDefault(b => b.Id == id);
                    if (bike != null)
                    {
                        return new Result<Bike>
                        {
                            Success = true,
                            Data = bike
                        };
                    }
                    else
                    {
                        return new Result<Bike>
                        {
                            Success = false,
                            ErrorMessage = "Bike was not found"
                        };
                    }
                }
                return new Result<Bike>();
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("METHOD - {0} : ERROR MESSAGE - {1} : FOR BIKE WITH ID - {2}", methodName, ex.Message, id));
                return new Result<Bike>
                {
                    Success = false,
                    ErrorMessage = ex.Message

                };
            }
        }
        /// <summary>
        /// The service to delete a bike by id.
        /// </summary>
        /// <param name="id">The id of the bike to be deleted.</param>
        /// /// <returns>A successful result in case it exists and it was deleted ,
        /// or an error message if it doesn't exist.</returns>
        public Result<Bike> DeleteBikeById(int id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation(String.Format("START METHOD - {0}", methodName));
                if (id > 0)
                {
                    var bike = bikeList.FirstOrDefault(b => b.Id == id);
                    if (bike != null)
                    {
                        bikeList.Remove(bike);
                        _bikeRepository.SaveBikesToFile(bikeList);
                        return new Result<Bike>
                        {
                            Success = true
                        };
                    }
                    else
                    {
                        return new Result<Bike>
                        {
                            Success = false,
                            ErrorMessage = "Bike was not found"
                        };
                    }
                }

                return new Result<Bike>();
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("METHOD - {0} : ERROR MESSAGE - {1} : FOR BIKE WITH ID - {2}", methodName, ex.Message, id));

                return new Result<Bike>
                {
                    Success = false,
                    ErrorMessage = ex.Message

                };
            }
        }
        /// <summary>
        /// Service to add a new Bike.
        /// </summary>
        /// <param name="newBike">The new Bike to be added.</param>
        /// <returns>A successful result in case it was created,
        /// or an error message if it couldn't be created.</returns>
        public Result<Bike> AddBike(Bike newBike)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation(String.Format("START METHOD - {0}", methodName));

                if (newBike != null)
                {
                    newBike.Id = _bikeRepository.GetNextId(bikeList);
                    bikeList.Add(newBike);

                    _bikeRepository.SaveBikesToFile(bikeList);

                    return new Result<Bike>()
                    {
                        Success = true
                    };
                }
                else
                {
                    return new Result<Bike>()
                    {
                        Success = false,
                        ErrorMessage = "Couldn't be added the new bike"
                    };
                }


                return new Result<Bike>();
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("METHOD - {0} : ERROR MESSAGE - {1}", methodName, ex.Message));
                return new Result<Bike>()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
        /// <summary>
        /// The Service to edit an existing Bike.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editedBike"></param>
        /// <returns>A successful result in case it exists and it was edited ,
        /// or an error message if it doesn't exist.</returns>
        public Result<Bike> EditBikeById(int id, Bike editedBike)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation(String.Format("START METHOD - {0}", methodName));
                if (id > 0 && editedBike != null)
                {
                    var bike = bikeList.FirstOrDefault(b => b.Id == id);

                    if (bike != null)
                    {
                        bike.Model = editedBike.Model;
                        bike.Make = editedBike.Make;
                        bike.Color = editedBike.Color;
                        bike.Type = editedBike.Type;
                        bike.Size = editedBike.Size;
                        _bikeRepository.SaveBikesToFile(bikeList);
                        return new Result<Bike>
                        {
                            Success = true
                        };
                    }
                    else
                    {
                        return new Result<Bike>
                        {
                            Success = false,
                            ErrorMessage = "Bike was not found"
                        };
                    }
                }

                return new Result<Bike>();
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("METHOD - {0} : ERROR MESSAGE - {1} : FOR BIKE WITH ID - {2}", methodName, ex.Message, id));
                return new Result<Bike>
                {
                    Success = false,
                    ErrorMessage = ex.Message

                };
            }
        }
        #endregion

    }
}