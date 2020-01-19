using ExpertalSystem.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Requests
{
    public class GetQuestionRequest
    {
        public Question PreviousQuestion { get; set; }
        [Required]
        public bool IsFirstQuestion { get; set; }
    }
}
