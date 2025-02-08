using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Data;
using Microsoft.AspNetCore.Mvc;
using ApiProject.Mapper;
using ApiProject.DTO.stock;
using Microsoft.EntityFrameworkCore;
using ApiProject.Intefaces;
using ApiProject.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace ApiProject.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context , IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var stocks = await _stockRepository.GetAllsAsync(queryObject);
            var stockDto=stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
              var stock = await _stockRepository.GetStockAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
        {

            var stockmodel = stockDto.ToStockFromCreateDto();
             await _stockRepository.CreateStockAsync(stockmodel);
            return CreatedAtAction(nameof(GetById), new { id = stockmodel.Id }, stockmodel.ToStockDto());

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto stockDto)
        {

          var stock= await _stockRepository.UpdateStockAsync(stockDto,id);
            return Ok(stock.ToStockDto());
        }


     [HttpDelete]
     [Route("{id}")]
     public async Task<IActionResult> DeleteStock([FromRoute] int id){
         _stockRepository.DeleteStock(id);
        _context.SaveChanges();
        return Ok("Stock deleted Succefully");

     }


    }
}