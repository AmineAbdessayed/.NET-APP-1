using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Extentions;
using ApiProject.Helpers;
using ApiProject.Intefaces;
using ApiProject.Models;
using ApiProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiProject.Controllers
{
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly ILogger<PortfolioController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IStockRepository _stockRepository;

        public PortfolioController(UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository, IStockRepository stockRepository)
        {
            _userManager = userManager;
            _portfolioRepository = portfolioRepository;
            _stockRepository = stockRepository;

        }


        [HttpGet]
        [Authorize]  // lel claims
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username); // lazem taaml extention l claims bech tgeetii l username or email ..
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();

            var appUser = await _userManager.Users
                .Include(u => u.portfolios)
                .ThenInclude(p => p.Stock)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (appUser == null) return BadRequest("User Not Found");

            var stock = await _stockRepository.GetStockBySymbol(symbol);
            if (stock == null) return BadRequest("Stock Not Found");

            if (appUser.portfolios.Any(p => p.Stock.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Already got the stock");
            }

            var portfolio = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id,
            };

            await _portfolioRepository.CreatePortfolio(portfolio);

            return StatusCode(201, "Added Successfully");
        }



      [HttpDelete]
      [Authorize]
            public async Task<IActionResult> DeleteStockUser(string symbol){

                var username= User.GetUsername();
                var user= await _userManager.FindByNameAsync(username);


                 var userPortfolio= await _portfolioRepository.GetUserPortfolio(user);
                 
                 var filteredStock= userPortfolio.Where(p=>p.Symbol==symbol).ToList();
                 if(filteredStock.Count()==1){

                    await _portfolioRepository.DeleteSTOCK(user,symbol);

                    return StatusCode(201, "Deleted Succefullt");
                 }

                 return StatusCode(500);

                 }



            }
    }
