using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Data;
using ApiProject.Intefaces;
using ApiProject.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            await _dbContext.portfolios.AddAsync(portfolio);
            await _dbContext.SaveChangesAsync();
           return portfolio;
        }

        public async Task<Portfolio> DeleteSTOCK(AppUser appUser, string Symbol)
        {
            var portfolio = await _dbContext.portfolios.FirstOrDefaultAsync(p=>p.AppUserId==appUser.Id&& p.Stock.Symbol==Symbol);
             _dbContext.portfolios.Remove(portfolio);
             await _dbContext.SaveChangesAsync();
             return portfolio;
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