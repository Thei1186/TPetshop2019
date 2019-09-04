namespace TPetshop2019.Core.ApplicationServices
{
    public interface IValidateIdService
    {
        /// <summary>
        /// Checks if an id is legal, i.e. above 0
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if legal or false if illegal</returns>
        bool ValidateId(int id);
    }
}