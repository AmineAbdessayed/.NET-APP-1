using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.DTO.comment
{
    public class CreateCommentDto
    {
         [Required]
         [MinLength(5, ErrorMessage = "Name must be 5 characters")]
         [MaxLength(12, ErrorMessage = "Name must be 12 characters max")]
         
        public string Name { get; set; } = string.Empty;
          [Required]
         [MinLength(5, ErrorMessage = "Content must be 5 characters")]
         [MaxLength(42, ErrorMessage = "Content must be 12 characters max")]
        public string Content { get; set; } = string.Empty;
    }
}