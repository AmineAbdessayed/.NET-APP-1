using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.DTO.comment;
using ApiProject.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ApiProject.Mapper
{
    public static class CommentMapper
    {

        public static CommentDto toCommentDto(this Comment comment){

            return new CommentDto
            {
             Id=comment.Id,
             Name=comment.Name,
             Content=comment.Content,
             createdOn=comment.createdOn,
             StockId=comment.StockId


            };
                
            
        }

        public static Comment ToCommentFromDto(CreateCommentDto createCommentDto,int stockId){
            return new Comment {
                Name=createCommentDto.Name,
                Content=createCommentDto.Content,
                StockId=stockId
            };
        }
         public static Comment ToCommentFromUpdatedDto(UpdateCommentDto dto){
            return new Comment {
                Name=dto.Name,
                Content=dto.Content,
            };
        }



        
    }
}