using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TodoAppAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter a todo item")]
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        [Required(ErrorMessage = "Please select a category")]
        public bool IsCompleted { get; set; }
        public string CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [Required(ErrorMessage = "Please select a status")]
        public string StatusId { get; set; }
        [ValidateNever]
        public Status Status { get; set; }
        [Required(ErrorMessage = "Please select a priority level")]
        public string PriorityId { get; set; }
        [ValidateNever]
        public Priority Priority { get; set; }
        public bool Overdue => StatusId == "open" || StatusId == "progress" && DueDate < DateTime.Today;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
