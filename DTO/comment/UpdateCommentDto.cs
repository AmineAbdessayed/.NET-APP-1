using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.DTO.comment
{
    public class UpdateCommentDto
    {

        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

    }
}