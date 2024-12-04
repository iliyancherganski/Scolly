namespace Scolly.Services.Services.Contracts
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAllByName(string name);
        Task<T?> GetById(int id);
        Task Add(T model);
        Task EditById(int id, T model);
        Task DeleteById(int id);
        Task<T?> MapData(int modelId);
    }
}
