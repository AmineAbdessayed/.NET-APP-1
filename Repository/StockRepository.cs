using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Data;
using ApiProject.DTO.stock;
using ApiProject.Helpers;
using ApiProject.Intefaces;
using ApiProject.Mapper;
using ApiProject.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;

        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock ;

        }

        public string DeleteStock(int id)
        {
            var stock = _context.Stock.FirstOrDefault(x => x.Id == id);
            if (stock == null)
            {
                return "Stock not found !!";
            }
            _context.Stock.Remove(stock);
            return "Stock Deleted Succefullt";
        }

        public async Task<List<Stock>> GetAllsAsync(QueryObject queryObject)
        {
          //  return await _context.Stock.Include(x=>x.Comments).ToListAsync();
          var stocks=  _context.Stock.Include(x=>x.Comments).AsQueryable();
          if(!string.IsNullOrWhiteSpace(queryObject.CompanyName)){
            stocks=stocks.Where(x=>x.CompanyName.Contains(queryObject.CompanyName));
          }
          if(!string.IsNullOrWhiteSpace(queryObject.Symbol)){
            stocks=stocks.Where(x=>x.Symbol.Contains(queryObject.Symbol));
          }
          if(!string.IsNullOrWhiteSpace(queryObject.SortBy)){
            if(queryObject.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase)){
                stocks=queryObject.IsDecsending? stocks.OrderByDescending(s=>s.Symbol): stocks.OrderBy(s=>s.Symbol);
            }
          }
          var skipNumber=(queryObject.PageNumber-1)*queryObject.PageSize;
          return  await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();

        }

        public async Task<Stock?> GetStockAsync(int id)
        {
            var stock = await _context.Stock.Include(x=>x.Comments).FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            return stock;
        }

        public async Task<Stock?> GetStockBySymbol(string symbol)
        {
           var stock = await _context.Stock.FirstOrDefaultAsync(c=>c.Symbol==symbol);
           return stock;
        }

        public async Task<bool> isStockExists(int id)
        {
           return await _context.Stock.AnyAsync(x=>x.Id == id);
         

        }

        public async Task<Stock?> UpdateStockAsync(UpdateStockDto stockDto, int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;

            // Save the changes to the database
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}