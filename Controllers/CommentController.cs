using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.DTO.comment;
using ApiProject.Intefaces;
using ApiProject.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var comments = await _commentRepository.GetAllComments();
            var commentdto = comments.Select(x => x.toCommentDto());
            return Ok(commentdto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCommentByID(int id)
        {
            var comment = await _commentRepository.FindCommentAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.toCommentDto());
        }
 
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id , [FromBody] UpdateCommentDto dto){

            var comment=CommentMapper.ToCommentFromUpdatedDto(dto);
             await _commentRepository.UpdateCommentAsync( comment,id);
             return Ok(comment.toCommentDto()
             );
        }  

           [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id){
            var commentModel= await _commentRepository.DeleteCommentAsync(id);
            if(commentModel==null){
                return NotFound();
            }
            return Ok(commentModel);
        }
    }
}