using Library.BikeApplication.Model;

namespace Library.BikeApplication.Interface;

public interface IBikeService
{
    /// <summary>
    /// Function to get all Bikes
    /// </summary>
    /// <returns></returns>
    public Result<List<Bike>> GetBikes();
    /// <summary>
    /// Function to get a Bike
    /// </summary>
    /// <returns>
    /// Bikes
    /// </returns>
    public Result<Bike> GetBikeById(int id);

    /// <summary>
    /// Adds in the File the new Bike
    /// </summary>
    /// <param name="bikes"></param>
    /// <param name="bike"></param>
    public Result<Bike> AddBike(Bike bikes);
    /// <summary>
    /// Deletes one Bike by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Result<Bike> DeleteBikeById(int id);
    /// <summary>
    /// Edites one Bike by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Result<Bike> EditBikeById(int id , Bike bike);
}