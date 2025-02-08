using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Data;
using ApiProject.Intefaces;
using ApiProject.Models;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public PortfolioRepository(ApplicationDBContext context)
        {
            _dbContext = context;
            
        }
        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _dbContext.portfolios.Where(p=>p.AppUserId==user.Id)

            .Select(stock=> new Stock {

                Id= stock.StockId,
                Symbol= stock.Stock.Symbol,
                CompanyName= stock.Stock.CompanyName,
                Purchase= stock.Stock.Purchase,
                LastDiv= stock.Stock.LastDiv,
                Industry=stock.Stock.Industry,
                MarketCap=stock.Stock.MarketCap
            }).ToListAsync();
        }
    }
}