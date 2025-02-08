using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Extentions;
using ApiProject.Intefaces;
using ApiProject.Models;
using ApiProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiProject.Controllers
{
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly ILogger<PortfolioController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> userManager,IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _portfolioRepository = portfolioRepository;

        }


    [HttpGet]
    [Authorize]  // lel claims
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username= User.GetUsername();
            var appUser= await _userManager.FindByNameAsync(username); // lazem taaml extention l claims bech tgeetii l username or email ..
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);




        }

     
    }
}