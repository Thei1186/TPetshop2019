namespace TPetshop2019.Core.ApplicationServices.Services
{
    public class MiscService: IMiscService
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