namespace TPetShop2019.Infrastructure.SQL
{
    public interface IDbInitializer
    {
        void SeedDb(PetShopContext context);
    }
}