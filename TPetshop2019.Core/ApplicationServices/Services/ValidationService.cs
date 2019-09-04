namespace TPetshop2019.Core.ApplicationServices.Services
{
    public class ValidationService: IValidationService
    {
        public bool ValidateId(int id)
        {
            if (id > 0)
            {
                return true;
            }

            return false;
        }
    }
}