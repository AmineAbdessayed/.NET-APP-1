using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Data;
using ApiProject.Intefaces;
using ApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context){
            _context = context;
        }
        
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment=await _context.Comments.FirstOrDefaultAsync(c=>c.Id == id);
            if(comment==null){
                return null;
            }
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return comment;

        }

        public async Task<Comment?> FindCommentAsync(int id)
        {
            var comment=await _context.Comments.FirstOrDefaultAsync(c=>c.Id==id);
            return comment;
        }

        public async Task<List<Comment>> GetAllComments()
        {
           return await _context.Comments.ToListAsync() ;
        }

        public async Task<Comment?> UpdateCommentAsync(Comment comment, int id)
        {
            var extingComment = await _context.Comments.FirstOrDefaultAsync(c=>c.Id == id);
            if(extingComment==null){
                return null;
            }
            extingComment.Content = comment.Content;
            extingComment.Name = comment.Name;
            await _context.SaveChangesAsync();
            return extingComment;

        }
    }
}