using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.DTO.comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime createdOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
    }
}