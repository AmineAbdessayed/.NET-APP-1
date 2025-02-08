using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.DTO.stock;
using ApiProject.Helpers;
using ApiProject.Mapper;
using ApiProject.Models;

namespace ApiProject.Intefaces
{
    public interface IStockRepository
    {

        Task<List<Stock>> GetAllsAsync(QueryObject queryObject);
        Task<Stock?> GetStockAsync(int id);
        Task<Stock> CreateStockAsync(Stock stock);
        string DeleteStock(int id);

        Task<Stock?> UpdateStockAsync(UpdateStockDto stock ,int id);
        Task<bool> isStockExists(int id);
        
    }
}