using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using TodoAppAPI.Models;

namespace TodoAppAPI.DTOs
{
    public class TodoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a todo item")]
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        [Required(ErrorMessage = "Please select a category")]
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "Please select a status")]
        public string StatusId { get; set; }
        [Required(ErrorMessage = "Please select a priority level")]
        public string PriorityId { get; set; }
        public bool Overdue => StatusId == "open" || StatusId == "progress" && DueDate < DateTime.Today;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
