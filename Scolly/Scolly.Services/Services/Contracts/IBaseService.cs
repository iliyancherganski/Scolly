﻿namespace Scolly.Services.Services.Contracts
{
    public interface IBaseService<T1,T2>
    {
        Task<List<T1>> GetAll();
        Task<List<T1>> GetAllByName(string name);
        Task<T1?> GetById(int id);
        abstract Task Add(T1 model);
        Task EditById(int id, T1 model);
        Task DeleteById(int id);
        T1 MapData(T2 model);
    }
}
