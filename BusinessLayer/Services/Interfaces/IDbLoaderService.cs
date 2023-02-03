using BusinessLayer.Dto.DBLoad;

//to delete
namespace BusinessLayer.Services.Interfaces
{
    public interface IDbLoaderService
    {
        Task<string> LoadDb(DbLoadModel model);
    }
}
