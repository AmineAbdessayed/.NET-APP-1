using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Models;

namespace ApiProject.Intefaces
{
    public interface ICommentRepository
    {
        Task<Comment?> FindCommentAsync(int id);
        Task<List<Comment>> GetAllComments();
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment?> UpdateCommentAsync(Comment comment, int id);
        Task<Comment?> DeleteCommentAsync(int id);

    }
}