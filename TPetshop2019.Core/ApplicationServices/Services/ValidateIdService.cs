using System.IO;

namespace TPetshop2019.Core.ApplicationServices.Services
{
    public class ValidateIdService: IValidateIdService
    {
        public bool ValidateId(int id)
        {
            if (id > 0)
            {
                return true;
            }

            throw new InvalidDataException($"The id: {id} is not valid, please use an id greater than 0");
        }
    }
}